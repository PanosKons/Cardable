using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardDisplayInventory : MonoBehaviour
{
    public int Index;
    public bool IsInventory;
    public Image image;
    public TMPro.TextMeshProUGUI Text;
    public void Update()
    {
        if(IsInventory == true)
        {
            if (GameManager.Inventory[Index] != null)
            {
                image.enabled = true;
                Text.text = GameManager.Inventory[Index].Value.name;
                return;
            }
        }
        else
        {
            if (GameManager.Deck[Index] != null)
            {
                image.enabled = true;
                Text.text = GameManager.Deck[Index].Value.name;
                return;
            }
        }
        image.enabled = false;
        Text.text = "";
    }
    public void Clicked()
    {
        if (IsInventory == true)
            GameManager.MoveToDeck(Index);
        else
            GameManager.MoveToInventory(Index);
    }
}
