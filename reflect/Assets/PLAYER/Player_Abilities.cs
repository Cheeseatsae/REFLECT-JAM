using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Player_Abilities : MonoBehaviour
{
    [HideInInspector] private Player_Movement pMove;
    [HideInInspector] public Player_View pView;
    
    public string InputXButton = "X";
    public string InputYButton = "Y";
    public string InputBButton = "B";
    public string InputAButton = "A";
    
    private void Awake()
    {
        pMove = GetComponent<Player_Movement>();
        pView = GetComponentInChildren<Player_View>();
        
        InputXButton += pMove.playerNum;
        InputYButton += pMove.playerNum;
        InputBButton += pMove.playerNum;
        InputAButton += pMove.playerNum;
    }

    void Update()
    {
         if (Input.GetButtonDown(InputXButton)) XButton();
         if (Input.GetButtonDown(InputBButton)) BButton();
         if (Input.GetButtonDown(InputAButton)) AButton();
         if (Input.GetButtonDown(InputYButton)) YButton();
         
         Debug.DrawLine(transform.position, pView.gameObject.transform.position + (pView.gameObject.transform.forward * 4), Color.blue, 0.1f);
         
         if (CheckBall()) Debug.DrawLine(transform.position, Ball.GameBall.transform.position, Color.red, 0.2f); 
         else Debug.DrawLine(transform.position, Ball.GameBall.transform.position, Color.yellow, 0.2f);
    }

//    private void OnDrawGizmos()
//    {
//        Gizmos.color = Color.red;
//        Gizmos.DrawSphere(transform.position + pView.transform.forward, 3);
//    }

    bool CheckBall(float radius = 3)
    {
        Collider[] col = Physics.OverlapSphere(transform.position + pView.transform.forward, radius);

        foreach (Collider c in col)
        {
            if (c.gameObject == Ball.GameBall) return true;
        }
        
        return false;
    }

    public bool cooldown = false;
    public bool ballHeld = false;
    
    void AButton()
    {
        Debug.Log("Pressing A");
        if (ballHeld) return;
        if (cooldown) return;
        if (!CheckBall(1.5f)) return;
        
        Debug.Log("hitball");
        StartCoroutine(HoldBall());
        
    }

    IEnumerator HoldBall()
    {
        ballHeld = true;
        float duration = 1 + (Ball.instance.speed * 0.004f);
        StartCoroutine(Cooldown(duration + 0.2f));
        float time = 0;
        Ball.instance.body.velocity = Vector3.zero;

        while (time < duration)
        {
            time += Time.deltaTime;
            Ball.instance.body.velocity = Vector3.zero;
            Ball.GameBall.transform.position = transform.position + (pView.transform.forward * 2);
            
            yield return null;
        }
        Ball.instance.Pie(pView.transform.forward.x, pView.transform.forward.z, this.gameObject);
        ballHeld = false;
    }
    
    void BButton()
    {
        Debug.Log("Pressing B");
        if (cooldown) return;
        if (!CheckBall(2.5f)) return;
        StartCoroutine(Cooldown());
        Debug.Log("hitball");
        //Vector3 force = Vector3.Normalize(Ball.GameBall.transform.position - transform.position);
        Ball.instance.Watermelon(pView.transform.forward.x, pView.transform.forward.z, this.gameObject);
        pMove.Knockback(9000, 0.5f);
    }

    void XButton()
    {
        Debug.Log("Pressing X");
        if (cooldown) return;
        if (!CheckBall(2.5f)) return;
        StartCoroutine(Cooldown());
        Debug.Log("hitball");
        Ball.instance.Jam(pView.transform.forward.x, pView.transform.forward.z, this.gameObject);
    }
    
    void YButton()
    {
        Debug.Log("Pressing Y");
        if (cooldown) return;
        if (!CheckBall(2.5f)) return;
        StartCoroutine(Cooldown());
        Debug.Log("hitball");
        Ball.instance.Chilli(pView.transform.forward.x, pView.transform.forward.z, this.gameObject);
    }

    IEnumerator Cooldown(float duration = 0.7f)
    {
        cooldown = true;
        yield return new WaitForSecondsRealtime(duration);
        cooldown = false;
    }
}
