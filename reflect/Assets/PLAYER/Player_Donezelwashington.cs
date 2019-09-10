using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Donezelwashington : MonoBehaviour
{
    public GameObject Player1;
    public GameObject Player2;

    private Vector3 player1Start;
    private Transform player1View;
    private Quaternion player1StartRot;
    private Vector3 player2Start;
    private Transform player2View;
    private Quaternion player2StartRot;

    private float ballStartY;
    
    public Counter counter1;
    public Counter counter2;

    private void OnDestroy()
    {
        Ball.instance.OnPlayerHit -= ResetPlay;
    }

    // Start is called before the first frame update
    void Start()
    {
        Ball.instance.OnPlayerHit += ResetPlay;
        ballStartY = Ball.GameBall.transform.position.y;
        player1Start = Player1.transform.position;
        player1View = Player1.GetComponentInChildren<Player_View>().transform;
        player1StartRot = player1View.rotation;
        player2Start = Player2.transform.position;
        player2View = Player2.GetComponentInChildren<Player_View>().transform;
        player2StartRot = player2View.rotation;
    }

    public void ResetPlay(GameObject player)
    {
        int playerLost = 0;
        if (player == Player1)
        {
            counter2.FoodHit();
            playerLost = 1;
        } else if (player == Player2)
        {
            counter1.FoodHit();
            playerLost = 2;
        }
        
        ResetPlayers(playerLost);
    }

    void ResetPlayers(int playerLost)
    {
        Player1.transform.position = player1Start;
        player1View.rotation = player1StartRot;
        Player2.transform.position = player2Start;
        player2View.rotation = player2StartRot;

        Player_Abilities p1 = Player1.GetComponent<Player_Abilities>();
        p1.StopAllCoroutines();
        p1.ballHeld = false;
        p1.cooldown = false;
        Player_Abilities p2 = Player2.GetComponent<Player_Abilities>();
        p2.StopAllCoroutines();
        p2.ballHeld = false;
        p2.cooldown = false;
        
        if (playerLost == 1)
        {
            Ball.GameBall.transform.position = Player1.transform.position + (player1View.forward * 5);
            Ball.GameBall.transform.position = new Vector3(Ball.GameBall.transform.position.x, ballStartY, Ball.GameBall.transform.position.z);
            Ball.instance.lastPlayer = Player1;
        }
        else
        {
            Ball.GameBall.transform.position = Player2.transform.position + (player2View.forward * 5);
            Ball.GameBall.transform.position = new Vector3(Ball.GameBall.transform.position.x, ballStartY, Ball.GameBall.transform.position.z);
            Ball.instance.lastPlayer = Player2;
        }

        Ball.instance.speed = Ball.instance.baseSpeed;
        Ball.instance.body.velocity = Vector3.zero;
        Ball.GameBall.transform.localScale = Vector3.one;
    }
}
