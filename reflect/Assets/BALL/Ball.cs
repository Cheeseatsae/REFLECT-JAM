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

    public float speed = 10;

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

    void ResetSpeed()
    {
        body.velocity = body.velocity.normalized * speed;
    }
    
    public void ThrowBall(float x, float z, float strength)
    {
        speed += strength;
        Vector3 force = new Vector3(x, 0 ,z) * speed;
        body.velocity = force;
    }
    
    public void Chilli(float x, float z)
    {
        transform.localScale = Vector3.one * 0.7f;
        ThrowBall(x, z, 5);
    }

    public void Jam(float x, float z)
    {
        transform.localScale = Vector3.one * 1f;
        ThrowBall(x, z, 2.5f);
    }

    public void Pie(float x, float z)
    {
        transform.localScale = Vector3.one * 0.9f;
        ThrowBall(x, z, 2f);
    }

    public void Watermelon(float x, float z)
    {
        transform.localScale = Vector3.one * 1.5f;
        ThrowBall(x, z, 1);
    }
    
}
