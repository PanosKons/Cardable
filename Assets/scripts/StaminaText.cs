using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaminaText : MonoBehaviour
{
    private TMPro.TextMeshProUGUI Text;
    void Start()
    {
        Text = GetComponent<TMPro.TextMeshProUGUI>();
    }

    void Update()
    {
        Text.text = GameManager.info.Stamina.ToString() + "/" + GameManager.info.MaxStamina.ToString();
    }
}
