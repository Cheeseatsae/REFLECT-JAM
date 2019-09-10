using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Build.Player;
using UnityEngine;
using UnityEngine.Experimental.PlayerLoop;
using UnityEngine.UIElements;

public class Player_Movement : MonoBehaviour
{
    [HideInInspector] public Rigidbody body;
    
    public float acceleration;
    public float maxSpeed;
    public float breaking;
    public float breakingThreshold;
    public float jumpHeight;

    public string playerNum;
    
    public string InputX;
    public string InputY;
    public string InputJump;
    
    private bool airborne;
    
    [HideInInspector] public float _inputX;
    [HideInInspector] public float _inputY;
    private float _inputJump;

    private Player_View pView;

    public PhysicMaterial slidePhysMat;
    public PhysicMaterial defaultPhysMat;
    [HideInInspector] public bool stunned = false;

    private void Awake()
    {
        body = GetComponent<Rigidbody>();
        pView = GetComponentInChildren<Player_View>();
        GetComponent<Collider>().material = defaultPhysMat;
        
        InputX += playerNum;
        InputY += playerNum;
        InputJump += playerNum;
    }

    private void FixedUpdate()
    {
        Vector3 force = Time.deltaTime * acceleration * new Vector3(_inputX, 0, _inputY);

        body.AddRelativeForce(force);

        Vector2 vel = new Vector2(body.velocity.x, body.velocity.z);
        vel = Vector2.ClampMagnitude(vel, maxSpeed);

        body.velocity = new Vector3(vel.x, body.velocity.y, vel.y);

        if (stunned) return;
        if (Math.Abs(_inputX) < breakingThreshold && Math.Abs(_inputY) < breakingThreshold && Math.Abs(body.velocity.y) < 0.1 )
        {
            body.velocity = Vector3.Lerp(body.velocity, Vector3.zero, breaking);
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        if (stunned)
        {
            _inputX = 0;
            _inputY = 0;
            return;
        }
        
        _inputX = Input.GetAxis(InputX);
        _inputY = Input.GetAxis(InputY);
        if (Input.GetButtonDown(InputJump)) Jump();
    }

    public void Knockback(float force, float duration)
    {
        StartCoroutine(KnockbackRoutine(force, duration));
    }

    IEnumerator KnockbackRoutine(float force, float duration)
    {
        Collider col = GetComponent<Collider>();
        col.material = slidePhysMat;
        body.AddRelativeForce(-pView.transform.forward * force);
        stunned = true;
        
        yield return new WaitForSecondsRealtime(duration);
        stunned = false;
        col.material = defaultPhysMat;
    }
    
    public delegate void HitGround();
    public event HitGround OnHitGround;
    
    private void OnCollisionEnter(Collision other)
    {
        foreach (ContactPoint c in other.contacts)
        {
            if (c.normal.y > 0.5f)
            {
                OnHitGround?.Invoke();
                airborne = false;
            }
        }
    }

    void Jump()
    {
        if (!airborne)
        {
            body.velocity = body.velocity + (Vector3.up * jumpHeight);
            airborne = true;
        }
    }
}
