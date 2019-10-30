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
        animator.CrossFade("GettingHit", 0.1f);
    }

    public void GetKilled()
    {
        animator.CrossFade("GettingKilled", 0.1f);
    }

    public void Heal(uint newHp)
    {
        
    }

    public void Strike(GameObject targetDoll)
    {
        animator.CrossFade("Strike", 0.1f);
    }

    // Start is called before the first frame update
    void Start()
    {
        animator = gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
