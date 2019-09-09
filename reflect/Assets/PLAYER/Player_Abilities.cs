using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Abilities : MonoBehaviour
{
    [HideInInspector] public Player_Movement pMove;
    
    public string InputXButton;
    public string InputYButton;
    public string InputBButton;
    
    private void Awake()
    {
        pMove = GetComponent<Player_Movement>();

        InputXButton += pMove.playerNum;
        InputYButton += pMove.playerNum;
        InputBButton += pMove.playerNum;
    }

    void Update()
    {
         
    }
}
