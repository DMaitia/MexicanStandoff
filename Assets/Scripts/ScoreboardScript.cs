using System.Collections;
using System.Collections.Generic;
using Model;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

public class ScoreboardScript : MonoBehaviour
{
    public Text ScoreboardInfo;
    
    public void ShowPlayers(List<Player> players)
    {
        ScoreboardInfo.text = "";
        for (int i = 0; i < players.Count; i++)
        {
            ScoreboardInfo.text += "Player " + i + " -------- " + players[i].Hp + "\n\n";
        }
    }
}
