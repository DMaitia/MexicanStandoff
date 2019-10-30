using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Doll : MonoBehaviour
{
    private int _secondsToAct;
    private uint _hp;

    public GameObject infoDisplay;
    public Animator animator;
    
    public void GetHurt(uint newHp)
    {
        animator.Play("GettingHit");
    }

    public void GetKilled()
    {
        animator.Play("GettingKilled");
    }

    public void Heal(uint newHp)
    {
        
    }

    public void Strike(GameObject targetDoll)
    {
        animator.Play("Strike");
    }
    
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
