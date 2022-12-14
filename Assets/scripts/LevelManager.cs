using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
[System.Serializable]
public struct CardUnlocks
{
    public uint Level;
    public uint Index;
}
public class LevelManager : MonoBehaviour
{
    public LevelInfo[] Levels;
    public Card[] Cards;
    public CardUnlocks[] Progression;
    public static uint UnlockedLevel;
    public GameObject Inventory;
    public GameObject PrepareScreen;
    public static LevelManager Instance;
    public static bool FirstTime = true;
    public static int CurrentLevel = 0;
    public Sprite[] Expressions;
    private void Start()
    {
        if (FirstTime == true)
        {
            FirstTime = false;
            PopulateInventory();
        }
        Instance = this;
    }
    public void NewCards()
    {
        foreach (CardUnlocks item in Progression)
        {
            if (item.Level == UnlockedLevel)
            {
                GameManager.Inventory[GameManager.GetFirstAvaiableSlot(GameManager.Inventory)] = Cards[item.Index];
            }
        }
    }
    public void PopulateInventory()
    {
        foreach (CardUnlocks item in Progression)
        {
            if(item.Level <= UnlockedLevel)
            {
                GameManager.Inventory[GameManager.GetFirstAvaiableSlot(GameManager.Inventory)] = Cards[item.Index];
            }
        }
    }
    public void PlayLevel(int Index)
    {
        if(UnlockedLevel >= Index && GameManager.GetFirstAvaiableSlot(GameManager.Deck) != 0)
        {
            GameManager.info = Levels[Index];
            CurrentLevel = Index;
            PrepareScreen.SetActive(true);
        }
    }
    public void OpenInventory()
    {
        Inventory.SetActive(true);
    }
    public void CloseInventory()
    {
        Inventory.SetActive(false);
    }
    public void GoToMainMenu()
    {
        SceneManager.LoadScene(0);
    }
    public void ClosePrepareScreen()
    {
        PrepareScreen.SetActive(false);
    }
    public void GenerateLevel()
    {
        SceneManager.LoadScene(2);
    }
}
