using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaminaText : MonoBehaviour
{
    private TMPro.TextMeshProUGUI Text;
    public bool IsInventory;
    void Start()
    {
        Text = GetComponent<TMPro.TextMeshProUGUI>();
    }

    void Update()
    {
        if (IsInventory == true)
            Text.text = LevelManager.Instance.Levels[LevelManager.CurrentLevel].MaxStamina.ToString();
        else
            Text.text = GameManager.info.Stamina.ToString() + "/" + GameManager.info.MaxStamina.ToString();
    }
}
