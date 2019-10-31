using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Doll : MonoBehaviour
{
    private int _secondsToAct;
    private int _hp;

    public GameObject infoDisplay;
    public Animator animator;
    
    public void GetHurt(int newHp)
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
        StartCoroutine(OnStrikeStart(targetDoll));
        gameObject.transform.LookAt(targetDoll.transform.position);
        animator.CrossFade("Strike", 0.1f);
        StartCoroutine(OnStrikeEnd(targetDoll));
    }

    private IEnumerator OnStrikeStart(GameObject targetDoll)
    {
        //TODO: make code so that the rotation is done smoothly
        gameObject.transform.LookAt(targetDoll.transform.position);
        yield return null;
    }
    
    private IEnumerator OnStrikeEnd(GameObject targetDoll)
    {
        //TODO: fix that and make code so that the rotation is done smoothly
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("Standing"))
        {
            gameObject.transform.LookAt(Vector3.zero);
        }
        yield return null;
    }

    
    // Start is called before the first frame update
    void Start()
    {
        animator = gameObject.GetComponent<Animator>();
    }

}
