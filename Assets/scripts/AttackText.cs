using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class AttackText : MonoBehaviour
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
                value = GameManager.info.Player.AttackDamage;
            }
            else
            {
                value = GameManager.info.Enemy.AttackDamage;
            }
        }
        else
        {
            if (IsPlayer == true)
            {
                value = LevelManager.Instance.Levels[LevelManager.CurrentLevel].Player.AttackDamage;
            }
            else
            {
                value = LevelManager.Instance.Levels[LevelManager.CurrentLevel].Enemy.AttackDamage;
            }
        }
        Text.text = value.ToString();
    }
}
