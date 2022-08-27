using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoodIndicator : MonoBehaviour
{
    public bool IsPlayer;
    public bool IsInventory;
    private SpriteRenderer sp;
    private Image image;
    void Start()
    {
        image = GetComponent<Image>();
        sp = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        Mood value;
        Expression expression;
        if (IsInventory == true)
        {
            if (IsPlayer == true)
            {
                value = LevelManager.Instance.Levels[LevelManager.CurrentLevel].Player.CurrentMood;
                expression = LevelManager.Instance.Levels[LevelManager.CurrentLevel].Player.CurrentExpression;
            }
            else
            {
                value = LevelManager.Instance.Levels[LevelManager.CurrentLevel].Enemy.CurrentMood;
                expression = LevelManager.Instance.Levels[LevelManager.CurrentLevel].Enemy.CurrentExpression;
            }
            switch (value)
            {
                case Mood.None:
                    image.color = new Color(1, 1, 1, 1);
                    break;
                case Mood.Healing:
                    image.color = new Color(1, 0.4f, 0.4f, 1);
                    break;
                case Mood.Thorns:
                    image.color = new Color(0.4f, 1, 0.4f, 1);
                    break;
            }
            image.sprite = LevelManager.Instance.Expressions[(int)expression];
        }
        else
        {
            if (IsPlayer == true)
            {
                value = GameManager.info.Player.CurrentMood;
                expression = GameManager.info.Player.CurrentExpression;
            }
            else
            {
                value = GameManager.info.Enemy.CurrentMood;
                expression = GameManager.info.Enemy.CurrentExpression;
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
            sp.sprite = GameManager.Instance.Expressions[(int)expression];
        }
        

    }
}
