using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class AttackText : MonoBehaviour
{
    public bool IsPlayer;
    private TMPro.TextMeshProUGUI Text;
    void Start()
    {
        Text = GetComponent<TMPro.TextMeshProUGUI>();
    }

    void Update()
    {
        uint value;
        if(IsPlayer == true)
        {
            value = GameManager.info.Player.AttackDamage;
        }
        else
        {
            value = GameManager.info.Enemy.AttackDamage;
        }
        Text.text = value.ToString();
    }
}
