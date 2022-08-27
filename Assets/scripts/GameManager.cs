using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

[System.Serializable]
public struct LevelInfo
{
    public Entity Player;
    public Entity Enemy;
    public uint MaxStamina;
    public uint Stamina;
    public uint CardCount;
}
public enum EffectType
{
    AttackDamageIncement,HealthPointsIncrement, AttackDamageDecrement, HealthPointsDecrement,Attack,MaxStaminaIncrement,MaxStaminaDecrement,CardIncrement,CardDecrement,DoubleEffect,SetMood
}
[System.Serializable]
public class Effect
{
    public EffectType effectType;
    public uint effectMagnitude;
    public string GetString()
    {
        switch(effectType)
        {
            case EffectType.AttackDamageIncement:
                return "+" + effectMagnitude + " Damage";
            case EffectType.HealthPointsIncrement:
                return "+" + effectMagnitude + " Health";
            case EffectType.AttackDamageDecrement:
                return "-" + effectMagnitude + " Damage";
            case EffectType.HealthPointsDecrement:
                return "-" + effectMagnitude + " Health";
            case EffectType.Attack:
                return "Attack!";
            case EffectType.MaxStaminaIncrement:
                return "+" + effectMagnitude + " MaxStamina";
            case EffectType.MaxStaminaDecrement:
                return "-" + effectMagnitude + " MaxStamina";
            case EffectType.CardIncrement:
                return "+" + effectMagnitude + " Card"; 
            case EffectType.CardDecrement:
                return "-" + effectMagnitude + " Card";
            case EffectType.DoubleEffect:
                return "Next card x" + effectMagnitude;
            case EffectType.SetMood:
                return "Apply " + ((Mood)(effectMagnitude)).ToString();
            default: throw new System.Exception();
        }
    }
}
[System.Serializable]
public struct Card : IComparable<Card>
{
    public string name;
    public Effect[] effects;
    public uint StaminaCost;

    public int CompareTo(Card card)
    {
        if (StaminaCost > card.StaminaCost) return -1;
        if (StaminaCost == card.StaminaCost) return 0;
        return 1;
    }
}
public enum Mood
{
    None,Healing,Thorns,Rage
}
[System.Serializable]
public struct Entity
{
    public uint HealthPoints;
    public uint AttackDamage;
    public uint MaxHealth;
    public Mood CurrentMood;
}
public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public static LevelInfo info;
    public static Card?[] Deck = new Card?[10];
    public static Card?[] Inventory = new Card?[48];
    public Card[] DrawnCards;
    public uint MaxCardCount;
    public GameObject CardDisplayPrefab;
    public Transform Canvas;
    public bool Rerolling;
    private void Start()
    {
        Instance = this;
        for (uint i = 0; i < info.CardCount; i++)
        {
            DrawNewCard(i);
        }
        MakeCardDisplays();
    }
    private void Update()
    {
        
    }
    public void MakeCardDisplays()
    {
        int OldCardCount = Canvas.transform.childCount;
        for (int i = 0; i < OldCardCount; i++)
        {
            Destroy(Canvas.GetChild(i).gameObject);
        }
        for (uint i = 0; i < info.CardCount; i++)
        {
            float step = 900 / ((float)info.CardCount + 1);
            GameObject cardDisplay = Instantiate(CardDisplayPrefab, Canvas, true);
            cardDisplay.GetComponent<CardDisplay>().CardId = i;
            RectTransform rect = cardDisplay.GetComponent<RectTransform>();
            rect.transform.localPosition = new Vector3((step * (i + 1)) - 450, 120 - 225.0235f, 0);
            rect.transform.localScale = new Vector3(0.95f, 0.95f, 1);
        }
    }
    public void DrawNewCard(uint Index)
    {
        if(Index >= DrawnCards.Length)
        {
            Array.Resize(ref DrawnCards, (int)Index + 1);
        }
        if(GetFirstAvaiableSlot(Deck) != 0)
            DrawnCards[Index] = Deck[UnityEngine.Random.Range(0, GetFirstAvaiableSlot(Deck))].Value;
    }
    public void EndTurn()
    {
        switch(info.Enemy.CurrentMood)
        {
            case Mood.Healing:
                info.Enemy.HealthPoints += info.Enemy.MaxHealth / 10;
                if (info.Enemy.HealthPoints > info.Enemy.MaxHealth)
                    info.Enemy.HealthPoints = info.Enemy.MaxHealth;
                break;
        }
        info.Stamina = info.MaxStamina;
        if(info.Player.CurrentMood == Mood.Thorns)
        {
            if (info.Enemy.HealthPoints > info.Player.AttackDamage)
            {
                info.Enemy.HealthPoints -= info.Player.AttackDamage;
            }
            else
            {
                info.Enemy.HealthPoints = 0;
                LostGame();
            }
        }
        if (info.Player.HealthPoints > info.Enemy.AttackDamage)
        {
            info.Player.HealthPoints -= info.Enemy.AttackDamage;
            switch (info.Player.CurrentMood)
            {
                case Mood.Healing:
                    info.Player.HealthPoints += info.Player.MaxHealth / 10;
                    if (info.Player.HealthPoints > info.Player.MaxHealth)
                        info.Player.HealthPoints = info.Player.MaxHealth;
                    break;
            }
        }
        else
        {
            info.Player.HealthPoints = 0;
            LostGame();
        }
    }
    public void ToggleReRoll()
    {
        Rerolling = !Rerolling;
    }
    public static int GetFirstAvaiableSlot(Card?[] inv)
    {
        for (int i = 0; i < inv.Length; i++)
        {
            if (inv[i] == null)
            {
                return i;
            }
        }
        return -1;
    }
    public static void MoveToDeck(int Index)
    {
        int slot = GetFirstAvaiableSlot(Deck);
        if (slot == -1) return;
        Deck[slot] = Inventory[Index];
        Inventory[Index] = null;
        Array.Sort(Deck);
        Array.Sort(Inventory);
        Array.Reverse(Deck);
        Array.Reverse(Inventory);
    }
    public static void MoveToInventory(int Index)
    {
        int slot = GetFirstAvaiableSlot(Inventory);
        if (slot == -1) return;
        Inventory[slot] = Deck[Index];
        Deck[Index] = null;
        Array.Sort(Deck);
        Array.Sort(Inventory);
        Array.Reverse(Deck);
        Array.Reverse(Inventory);
    }
    public void WonGame()
    {
        if(LevelManager.UnlockedLevel == LevelManager.CurrentLevel)
        {
            LevelManager.UnlockedLevel++;
            LevelManager.Instance.NewCards();
            Saving.Instance.Save();
        }
        SceneManager.LoadScene(1);
    }
    public void LostGame()
    {
        SceneManager.LoadScene(1);
    }
}
