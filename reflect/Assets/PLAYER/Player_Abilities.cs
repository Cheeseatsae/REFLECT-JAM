﻿using System;
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
    
    void AButton()
    {
        Debug.Log("Pressing A");
        if (!CheckBall(2.5f)) return;
        
        Debug.Log("hitball");
        Ball.instance.Pie(pView.transform.forward.x, pView.transform.forward.z);
    }
    
    void BButton()
    {
        Debug.Log("Pressing B");
        if (!CheckBall(2.5f)) return;
        
        Debug.Log("hitball");
        //Vector3 force = Vector3.Normalize(Ball.GameBall.transform.position - transform.position);
        Ball.instance.Watermelon(pView.transform.forward.x, pView.transform.forward.z);
        pMove.Knockback(9000, 0.5f);
    }

    void XButton()
    {
        Debug.Log("Pressing X");
        if (!CheckBall(2.5f)) return;
        
        Debug.Log("hitball");
        Ball.instance.Jam(pView.transform.forward.x, pView.transform.forward.z);
    }
    
    void YButton()
    {
        Debug.Log("Pressing Y");
        if (!CheckBall(2.5f)) return;
        
        Debug.Log("hitball");
        Ball.instance.Chilli(pView.transform.forward.x, pView.transform.forward.z);
    }
}
