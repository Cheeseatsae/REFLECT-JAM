using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TestTools;

public class Ball : MonoBehaviour
{

    public static GameObject GameBall;
    public static Ball instance;
    [HideInInspector] public Rigidbody body;
    private float previousMagnitude;
    private Vector3 previousVelocity;

    public float baseSpeed = 14;
    public float speed = 14;

    private void Awake()
    {
        GameBall = this.gameObject;
        instance = this;
        body = GetComponent<Rigidbody>();
    }

    private void LateUpdate()
    {
        if (body.velocity.magnitude < previousMagnitude) ResetSpeed();
        
        previousVelocity = body.velocity;
        previousMagnitude = previousVelocity.magnitude;
       
        Debug.Log(body.velocity.magnitude);
    }

    public delegate void PlayerHit(GameObject player);
    public event PlayerHit OnPlayerHit;
    
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.GetComponent<Player_Movement>())
        {
            if (other.gameObject == lastPlayer) return;
            OnPlayerHit?.Invoke(other.gameObject);
        } 
    }

    void ResetSpeed()
    {
        body.velocity = body.velocity.normalized * speed;
    }

    public GameObject lastPlayer;
    
    public void ThrowBall(float x, float z, float strength, GameObject p)
    {
        lastPlayer = p;
        speed += strength;
        Vector3 force = new Vector3(x, 0 ,z) * speed;
        body.velocity = force;
    }
    
    public void Chilli(float x, float z, GameObject p)
    {
        transform.localScale = Vector3.one * 0.7f;
        ThrowBall(x, z, 5, p);
    }

    public void Jam(float x, float z, GameObject p)
    {
        transform.localScale = Vector3.one * 1f;
        ThrowBall(x, z, 2.5f, p);
    }

    public void Pie(float x, float z, GameObject p)
    {
        transform.localScale = Vector3.one * 0.9f;
        ThrowBall(x, z, 2f, p);
    }

    public void Watermelon(float x, float z, GameObject p)
    {
        transform.localScale = Vector3.one * 1.5f;
        ThrowBall(x, z, 1, p);
    }
    
}
