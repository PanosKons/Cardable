using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelButton : MonoBehaviour
{
    public uint Level;
    public Image first,second;
    private void Start()
    {
        if (Level > LevelManager.UnlockedLevel)
        {
            first.color = new Color(0.6f, 0.6f, 0.6f, 1);
            second.color = new Color(0.6f, 0.6f, 0.6f, 0.5f);
        }
        else if (Level == LevelManager.UnlockedLevel)
        {
            first.color = new Color(1,0.6f,0,1);
            second.color = new Color(1, 0.6f, 0, 0.5f);
        }
        else
        {
            first.color = Color.yellow;
            second.color = new Color(1, 1, 0, 0.5f);
        }
    }
}
