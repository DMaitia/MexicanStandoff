using System;
using System.Collections;
using System.Collections.Generic;
using Bots;
using Control;
using Logging;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;
using UserInterface;
using Logger = Logging.Logger;

public class GameView : MonoBehaviour
{
    private const int MainPlayerId = 0;
    
    private const int DefaultSelectedDoll = 1;
    
    private Controller _controller;
    
    public GameObject dollPrefab;

    public GameObject scoreBoard;

    public GameObject settingsBoard;

    public GameObject logBoard;

    public Text matchCountDown;
    
    public float radius;

    private List<GameObject> _dolls;

    private int _selectedDollId; //Todo: desmockupear

    
    void Start()
    {
        _dolls = new List<GameObject>();
        _controller = new Controller(this);
        SpawnDolls();
        AddBotBehaviours();
        SelectTarget(DefaultSelectedDoll);
    }

    void Update()
    {
        ListenForSelection();
        PrintRemainingTime();
    }

    private void PrintRemainingTime()
    {
        matchCountDown.text = "Time remaining: " + ((int) (_controller.StopDateTime - DateTime.Now).TotalSeconds).ToString();
    }
    
    private void SpawnDolls()
    {
        Vector3 arenaPosition = gameObject.transform.position;
        double angleBetweenDolls = (2*Math.PI) / Settings.PlayersAmount;
        double theta = 0;
        for (int i = 0; i < Settings.PlayersAmount; i++)
        {
            Vector3 dollPosition = new Vector3((float) (radius * Math.Cos(theta)),
                arenaPosition.y, (float) (radius * Math.Sin(theta))
            );
            theta += angleBetweenDolls;
            
            GameObject doll = Instantiate(dollPrefab, dollPosition, Quaternion.identity) as GameObject;
            doll.transform.LookAt(Vector3.zero);
            doll.AddComponent<Doll>().Id = i;
            _dolls.Add(doll);
        }
    }

    private void AddBotBehaviours()
    {
        for (var id = 1; id < Settings.PlayersAmount; id++)
        {
            Bot.CreateBot(_dolls[id], id, Settings.BotAttackHealRate, _controller);
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
        settingsBoard.SetActive(true);
    }

    public void DisplayScoreboard()
    {
        scoreBoard.SetActive(true);
        ScoreBoard sb = scoreBoard.GetComponent<ScoreBoard>();
        sb.ClearRecords();
        foreach (var player in _controller.GetPlayers())
        {
            sb.AddRecord("", "Player " + player.Id, player.Hp.ToString());
        }
    }
    public void DisplayLogboard()
    {
        logBoard.SetActive(true);
        LogBoard lb = logBoard.GetComponent<LogBoard>();
        lb.ClearRecords();
        List<ActionEventInfo> events = Logger.GetLoggedEvents();
        for (var i = events.Count - 1; i >= 0; i--)
        {
            lb.AddRecord(events[i]);
        }
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
        GameObject target = _dolls[idDoll];
        target.GetComponent<Doll>().Heal(100);//TODO? hace falta?
    }

    public void FinishGame()
    {
        foreach (var doll in _dolls)
        {
            Destroy(doll);
        }

        DisplayScoreboard();
    }

    public void PauseGame()
    {
        _controller.SetPause(true);
    }

    public void ResumeGame()
    {
        _controller.SetPause(false);
    }

    public void PauseBots(bool pause)
    {
        for(var i = 1; i < _dolls.Count; i++)
        {
            _dolls[i].GetComponent<Bot>().SetPause(pause);
        }
    }
    
    /*
     * ListenForSelection
     * Method to listen if the user has selected an enemy doll.
     */
    private void ListenForSelection()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hitInfo = new RaycastHit();
            bool hit = Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hitInfo);
            
            if (hit) 
            {
                if (hitInfo.transform.gameObject.CompareTag("Enemy"))
                {
                    GameObject target = hitInfo.transform.gameObject;
                    SelectTarget(target.GetComponent<Doll>().Id);
                } 
            } 
        }
    }

    private void SelectTarget(int id)
    {
        if (id != 0 && id != _selectedDollId)
        {
            _dolls[_selectedDollId].GetComponent<Doll>().DeselectDoll();
            _dolls[id].GetComponent<Doll>().SelectDoll();
            _selectedDollId = id;
        }
    }
}
