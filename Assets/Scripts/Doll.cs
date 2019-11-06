using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Doll : MonoBehaviour
{
    private int _secondsToAct;
    private int _hp;
    private int _id;
    
    public GameObject infoDisplay;
    public Animator animator;
    public int Id
    {
        get => _id;
        set => _id = value;
    }
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
        animator.CrossFade("Healing", 0.1f);
        //TODO: increment health bar
    }

    public void Strike(GameObject targetDoll)
    {
        OnStrikeStart(targetDoll);
        animator.CrossFade("Strike", 0.1f);
    }

    private void OnStrikeStart(GameObject targetDoll)
    {
        StartCoroutine(LookAtSmoothly(targetDoll));
    }

    private IEnumerator LookAtSmoothly(GameObject targetDoll)
    {
        float elapsedTime = 0f;
        float time = 0.5f;
        var dollTransform = transform;
        var startingRotation = dollTransform.rotation;
        Vector3 orientation = targetDoll.transform.position - gameObject.transform.position;
        while (elapsedTime < time)
        {
            elapsedTime += Time.deltaTime;
            Quaternion rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(orientation), Time.time * 5f);
            transform.rotation = Quaternion.Slerp(startingRotation, rotation, (elapsedTime / time));
            yield return new WaitForEndOfFrame();
        }
    }

    public void SelectDoll()
    {
        gameObject.transform.GetChild(1).gameObject.SetActive(true);
    }

    public void DeselectDoll()
    {
        gameObject.transform.GetChild(1).gameObject.SetActive(false);
    }
    // Start is called before the first frame update
    void Start()
    {
        animator = gameObject.GetComponent<Animator>();
    }

}
