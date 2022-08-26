using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardDisplay : MonoBehaviour
{
    public uint CardId;
    public TMPro.TextMeshProUGUI Text;
    void Update()
    {
        Card card = GameManager.Instance.DrawnCards[CardId];
        Text.text = "\n" + card.name + "\n" + "--------" +"\n";
        foreach (Effect effect in card.effects)
        {
            Text.text += effect.GetString() + "\n";
        }
        Text.text += "--------" + "\n" + "Cost: " + card.StaminaCost;
    }
    public void OnCardPressed()
    {
        if (GameManager.Instance.Rerolling == true)
        {
            if (GameManager.info.Stamina < 2)
                return;
            GameManager.info.Stamina -= 2;
            GameManager.Instance.DrawNewCard(CardId);
            GameManager.Instance.Rerolling = false;
        }
        else
        {

            Card card = GameManager.Instance.DrawnCards[CardId];
            if (GameManager.info.Stamina < card.StaminaCost)
                return;
            Entity player = GameManager.info.Player;
            Entity enemy = GameManager.info.Enemy;
            foreach (Effect effect in card.effects)
            {
                switch (effect.effectType)
                {
                    case EffectType.AttackDamageIncement:
                        player.AttackDamage += effect.effectMagnitude;
                        break;
                    case EffectType.AttackDamageDecrement:
                        if (player.AttackDamage > effect.effectMagnitude)
                            player.AttackDamage -= effect.effectMagnitude;
                        else player.AttackDamage = 0;
                        break;
                    case EffectType.HealthPointsIncrement:
                        player.HealthPoints += effect.effectMagnitude;
                        if (player.HealthPoints > player.MaxHealth)
                            player.HealthPoints = player.MaxHealth;
                        break;
                    case EffectType.HealthPointsDecrement:
                        if (player.HealthPoints > effect.effectMagnitude)
                            player.HealthPoints -= effect.effectMagnitude;
                        else player.HealthPoints = 0;
                        break;
                    case EffectType.Attack:
                        if (enemy.HealthPoints > player.AttackDamage)
                            enemy.HealthPoints -= player.AttackDamage;
                        else
                        {
                            enemy.HealthPoints = 0;
                            GameManager.Instance.WonGame();
                        }
                        break;
                    case EffectType.MaxStaminaIncrement:
                        GameManager.info.MaxStamina += effect.effectMagnitude;
                        break;
                    case EffectType.MaxStaminaDecrement:
                        if (GameManager.info.MaxStamina > effect.effectMagnitude)
                            GameManager.info.MaxStamina -= effect.effectMagnitude;
                        else
                            GameManager.info.MaxStamina = 0;
                        break;
                    case EffectType.CardIncrement:
                        {
                            if (GameManager.info.CardCount + effect.effectMagnitude <= GameManager.Instance.MaxCardCount)
                            {
                                GameManager.info.CardCount += effect.effectMagnitude;
                                for (uint i = GameManager.info.CardCount - effect.effectMagnitude; i < GameManager.info.CardCount; i++)
                                {
                                    GameManager.Instance.DrawNewCard(i);
                                }
                                GameManager.Instance.MakeCardDisplays();
                            }
                        }
                        break;
                    case EffectType.CardDecrement:
                        {
                            if (GameManager.info.CardCount > effect.effectMagnitude)
                                GameManager.info.CardCount -= effect.effectMagnitude;
                            else
                                GameManager.info.CardCount = 0;
                            GameManager.Instance.MakeCardDisplays();
                        }
                        break;
                }
            }
            GameManager.info.Stamina -= card.StaminaCost;
            GameManager.info.Player = player;
            GameManager.info.Enemy = enemy;
            GameManager.Instance.DrawNewCard(CardId);
        }
    }
}
