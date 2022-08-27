using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoodIndicator : MonoBehaviour
{
    public bool IsPlayer;
    public bool IsInventory;
    private SpriteRenderer sp;
    void Start()
    {
        sp = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        Mood value;
        if(IsPlayer == true)
        {
            value = GameManager.info.Player.CurrentMood;
        }
        else
        {
            value = GameManager.info.Enemy.CurrentMood;
        }
        switch (value)
        {
            case Mood.None:
                sp.color = new Color(1, 1, 1, 1);
                break;
            case Mood.Healing:
                sp.color = new Color(1, 0.4f, 0.4f, 1);
                break;
            case Mood.Thorns:
                sp.color = new Color(0.4f, 1, 0.4f, 1);
                break;
        }

    }
}
