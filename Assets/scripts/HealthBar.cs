using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public bool IsPlayer;
    void Update()
    {
        float Ratio;
        if(IsPlayer == true)
            Ratio = GameManager.info.Player.HealthPoints / (float)GameManager.info.Player.MaxHealth;
        else
            Ratio = GameManager.info.Enemy.HealthPoints / (float)GameManager.info.Enemy.MaxHealth;

        transform.localScale = new Vector3(Ratio, 1, 1);
        transform.localPosition = new Vector3(((Ratio)*1.5f)-1.5f, 0, 0);
    }
}
