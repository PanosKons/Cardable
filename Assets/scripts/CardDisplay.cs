using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardDisplay : MonoBehaviour
{
    public uint CardId;
    public TMPro.TextMeshProUGUI Text;
    void Update()
    {
        Card card = GameManager.Instance.DrawnCards[CardId];
        Text.text = "\n" + card.name + "\n" + "--------" +"\n";
        if(card.effects != null)
            foreach (Effect effect in card.effects)
            {
                Text.text += effect.GetString() + "\n";
            }
        Text.text += "--------" + "\n" + "Cost: " + card.StaminaCost;
    }
    public uint Multiplier = 1;
    public void OnCardPressed()
    {
        if (GameManager.Instance.Rerolling == true)
        {
            if (GameManager.info.Stamina < 2)
                return;
            GameManager.info.Stamina -= 2;
            GameManager.Instance.DrawNewCard(CardId);
            GameManager.Instance.Rerolling = false;
        }
        else
        {

            Card card = GameManager.Instance.DrawnCards[CardId];
            if (GameManager.info.Stamina < card.StaminaCost)
                return;
            Entity player = GameManager.info.Player;
            Entity enemy = GameManager.info.Enemy;
            foreach (Effect effect in card.effects)
            {
                switch (effect.effectType)
                {
                    case EffectType.AttackDamageIncement:
                        player.AttackDamage += effect.effectMagnitude * Multiplier;
                        break;
                    case EffectType.AttackDamageDecrement:
                        if (player.AttackDamage > effect.effectMagnitude * Multiplier)
                            player.AttackDamage -= effect.effectMagnitude * Multiplier;
                        else player.AttackDamage = 0;
                        break;
                    case EffectType.HealthPointsIncrement:
                        player.HealthPoints += effect.effectMagnitude * Multiplier;
                        if (player.HealthPoints > player.MaxHealth)
                            player.HealthPoints = player.MaxHealth;
                        break;
                    case EffectType.HealthPointsDecrement:
                        if (player.HealthPoints > effect.effectMagnitude * Multiplier)
                            player.HealthPoints -= effect.effectMagnitude * Multiplier;
                        else player.HealthPoints = 0;
                        break;
                    case EffectType.Attack:
                        if (enemy.CurrentMood == Mood.Thorns)
                        {
                            if (player.HealthPoints > enemy.AttackDamage )
                                player.HealthPoints -= enemy.AttackDamage;
                            else
                            {
                                player.HealthPoints = 0;
                                GameManager.Instance.WonGame();
                            }
                        }
                        if (player.CurrentMood == Mood.Rage)
                        {
                            Multiplier *= 3;
                            Multiplier /= 2;
                        }
                        if (enemy.HealthPoints > player.AttackDamage * Multiplier)
                            enemy.HealthPoints -= player.AttackDamage * Multiplier;
                        else
                        {
                            enemy.HealthPoints = 0;
                            GameManager.Instance.WonGame();
                        }
                        break;
                    case EffectType.MaxStaminaIncrement:
                        GameManager.info.MaxStamina += effect.effectMagnitude * Multiplier;
                        break;
                    case EffectType.MaxStaminaDecrement:
                        if (GameManager.info.MaxStamina > effect.effectMagnitude * Multiplier)
                            GameManager.info.MaxStamina -= effect.effectMagnitude * Multiplier;
                        else
                            GameManager.info.MaxStamina = 0;
                        break;
                    case EffectType.CardIncrement:
                        {
                            if (GameManager.info.CardCount + effect.effectMagnitude * Multiplier <= GameManager.Instance.MaxCardCount)
                            {
                                GameManager.info.CardCount += effect.effectMagnitude * Multiplier;
                                for (uint i = GameManager.info.CardCount - effect.effectMagnitude * Multiplier; i < GameManager.info.CardCount; i++)
                                {
                                    GameManager.Instance.DrawNewCard(i);
                                }
                                GameManager.Instance.MakeCardDisplays();
                            }
                        }
                        break;
                    case EffectType.SetMood:
                        player.CurrentMood = (Mood)effect.effectMagnitude;
                        break;
                    case EffectType.CardDecrement:
                        {
                            if (GameManager.info.CardCount > effect.effectMagnitude * Multiplier)
                                GameManager.info.CardCount -= effect.effectMagnitude * Multiplier;
                            else
                                GameManager.info.CardCount = 0;
                            GameManager.Instance.MakeCardDisplays();
                        }
                        break;
                    case EffectType.DoubleEffect:
                        Multiplier = effect.effectMagnitude;
                        break;
                }
                if (effect.effectType != EffectType.DoubleEffect) Multiplier = 1;
            }
            GameManager.info.Stamina -= card.StaminaCost;
            GameManager.info.Player = player;
            GameManager.info.Enemy = enemy;
            GameManager.Instance.DrawNewCard(CardId);
        }
    }
}
