﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class GameView : MonoBehaviour
{
    private Controller _controller;

    public uint playersAmount;

    public uint minDamage;

    public uint maxDamage;

    public uint initialHp;
    
    public GameObject dollPrefab;
    
    public float radius;

    private List<GameObject> _dolls;

    private uint _selectedDollId = 2; //Todo: desmockupear
    
    void Start()
    {
        _dolls = new List<GameObject>();
//        _controller = new Controller(playersAmount, minDamage, maxDamage, initialHp);    
        SpawnDolls();
    }

    private void SpawnDolls()
    {
        Vector3 arenaPosition = gameObject.transform.position;
        double angleBetweenDolls = (2*Math.PI) / playersAmount;
        double theta = 0;
        for (int i = 0; i < playersAmount; i++)
        {
            Vector3 dollPosition = new Vector3((float) (radius*Math.Cos(theta)),
                                       arenaPosition.y, (float) (radius*Math.Sin(theta))
                                       ) - arenaPosition;
            theta += angleBetweenDolls;
            
            GameObject doll = Instantiate(dollPrefab, dollPosition, Quaternion.identity) as GameObject;
            doll.transform.LookAt(Vector3.zero);
            doll.AddComponent<Doll>();
            _dolls.Add(doll);
        }
    }

    public void SetEnemyDollSelected(uint idDoll)
    {
        _selectedDollId = idDoll;
    }

    public void OnShotButtonClick()
    {
        PerformStrikeAnimation(0, (int)_selectedDollId, 0);
    }
    
    public void UpdateDollTimer(uint idDoll, uint time)
    {
           
    }

    public void DisplayConfiguration()
    {
        
    }

    public void DisplayLog()
    {
        
    }
    
    public void PerformStrikeAnimation(int idDoll1, int idDoll2, uint newHp)
    {
        GameObject striker = _dolls[idDoll1];
        GameObject target = _dolls[idDoll2];
        striker.GetComponent<Doll>().Strike(target);
        target.GetComponent<Doll>().GetHurt(newHp);
    }

    public void PerformKillAnimation(int idDoll1, int idDoll2)
    {
        GameObject striker = _dolls[idDoll1];
        GameObject target = _dolls[idDoll2];
        striker.GetComponent<Doll>().Strike(target);
        target.GetComponent<Doll>().GetKilled();
    }

    public void PerformHealingAnimation(uint idDoll)
    {
        
    }
    
}
