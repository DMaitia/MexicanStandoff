using System;
using System.Collections;
using System.Collections.Generic;
using Bots;
using Control;
using UnityEngine;
using UnityEngine.Serialization;

public class GameView : MonoBehaviour
{
    private const int MainPlayerId = 0;
    
    private Controller _controller;
    
    public GameObject dollPrefab;
    
    public float radius;

    private List<GameObject> _dolls;

    private int _selectedDollId = 2; //Todo: desmockupear

    private Settings _settings;
    
    void Start()
    {
        _dolls = new List<GameObject>();

        _settings = new Settings(5, 10, 100, 1000, 2, 0.5f);
        
        _controller = new Controller(this, _settings);    
        
        SpawnDolls();
        
        AddBotBehaviours();
    }

    private void SpawnDolls()
    {
        Vector3 arenaPosition = gameObject.transform.position;
        double angleBetweenDolls = (2*Math.PI) / _settings.PlayersAmount;
        double theta = 0;
        for (int i = 0; i < _settings.PlayersAmount; i++)
        {
            Vector3 dollPosition = new Vector3((float) (radius * Math.Cos(theta)),
                arenaPosition.y, (float) (radius * Math.Sin(theta))
            );
            theta += angleBetweenDolls;
            
            GameObject doll = Instantiate(dollPrefab, dollPosition, Quaternion.identity) as GameObject;
            doll.transform.LookAt(Vector3.zero);
            doll.AddComponent<Doll>();
            _dolls.Add(doll);
        }
    }

    private void AddBotBehaviours()
    {
        for (var id = 1; id < _settings.PlayersAmount; id++)
        {
            Bot.CreateBot(_dolls[id], id, _settings.BotAttackHealRate, _controller);
        }
    }

    public void SetEnemyDollSelected(int idDoll)
    {
        _selectedDollId = idDoll;
    }

    public void OnShotButtonClick()
    {
        _controller.Strike(MainPlayerId, _selectedDollId);
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
    
    public void PerformStrikeAnimation(int idDoll1, int idDoll2, int newHp)
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

    public void HideKilledPlayer(int idDoll)
    {
        GameObject doll = _dolls[idDoll];
        StartCoroutine(HideAfterEndOfKillingAnimation(doll));
    }

    private IEnumerator HideAfterEndOfKillingAnimation(GameObject doll)
    {
        yield return new WaitForSeconds(5);;
        doll.SetActive(false);
    }

    public void PerformHealingAnimation(int idDoll)
    {
        
    }
}
