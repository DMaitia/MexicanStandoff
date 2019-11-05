using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using Control;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class SettingsBoard : MonoBehaviour
{
    public Text playersAmountText;
    public Text minDamageText;
    public Text maxDamageText;
    public Text initialHpText;
    public Text meanSecondsBetweenActionsText;
    public Text attackHealRateText;

    public Text playersAmountPlaceholder;
    public Text minDamagePlaceholder;
    public Text maxDamagePlaceholder;
    public Text initialHpPlaceholder;
    public Text meanSecondsBetweenActionsPlaceholder;
    public Text attackHealRatePlaceholder;
    
    void Awake()
    {
        LoadSettings();
    }

    public void LoadSettings()
    {
        playersAmountPlaceholder.text = Settings.PlayersAmount.ToString();
        minDamagePlaceholder.text = Settings.MinimumDamage.ToString();
        maxDamagePlaceholder.text = Settings.MaximumDamage.ToString();
        initialHpPlaceholder.text = Settings.InitialHp.ToString();
        meanSecondsBetweenActionsPlaceholder.text = Settings.SecondsBetweenActions.ToString();
        attackHealRatePlaceholder.text = Settings.BotAttackHealRate.ToString();
    }

    public void UpdateSettings()
    {
        if (initialHpText.text != "")
        {
            Settings.InitialHp = int.Parse(initialHpText.text);            
        }

        if (minDamageText.text != "")
        {
            Settings.MinimumDamage = int.Parse(minDamageText.text);
        }
        
        if (maxDamageText.text != "")
        {
            Settings.MaximumDamage = int.Parse(maxDamageText.text);
        }

        if (playersAmountText.text != "")
        {
            Settings.PlayersAmount = int.Parse(playersAmountText.text);
        }
        
        if (meanSecondsBetweenActionsText.text != "")
        {
            Settings.SecondsBetweenActions = int.Parse(meanSecondsBetweenActionsText.text);
        }

        if (attackHealRateText.text != "")
        {
            Settings.BotAttackHealRate = int.Parse(attackHealRateText.text);
        }
    }
}
