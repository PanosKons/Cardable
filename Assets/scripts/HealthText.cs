using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthText : MonoBehaviour
{
    public bool IsInventory;
    public bool IsPlayer;
    private TMPro.TextMeshProUGUI Text;
    void Start()
    {
        Text = GetComponent<TMPro.TextMeshProUGUI>();
    }

    void Update()
    {
        uint value;
        if (IsInventory == false)
        {
            if (IsPlayer == true)
            {
                value = GameManager.info.Player.HealthPoints;
            }
            else
            {
                value = GameManager.info.Enemy.HealthPoints;
            }
        }
        else
        {
            if (IsPlayer == true)
            {
                value = LevelManager.Instance.Levels[LevelManager.CurrentLevel].Player.HealthPoints;
            }
            else
            {
                value = LevelManager.Instance.Levels[LevelManager.CurrentLevel].Enemy.HealthPoints;
            }
        }
        Text.text = value.ToString();
    }
}
