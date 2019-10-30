using System;
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
            _dolls.Add(doll);
        }
    }

    public void SetEnemyDollSelected(uint idDoll)
    {
        
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
    
    public void PerformStrikeAnimation(uint idDoll1, uint idDoll2)
    {
        
    }

    public void PerformKillAnimation(uint idDoll1, uint idDoll2)
    {
        
    }

    public void PerformHealingAnimation(uint idDoll)
    {
        
    }
    
}
