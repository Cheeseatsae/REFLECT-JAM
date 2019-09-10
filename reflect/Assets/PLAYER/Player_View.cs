using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_View : MonoBehaviour
{
    private Player_Movement pMove;

    private void Awake()
    {
        pMove = GetComponentInParent<Player_Movement>();
        pMove.OnHitGround += ResetView;
    }

    private void OnDestroy()
    {
        pMove.OnHitGround -= ResetView;
    }

    private void Update()
    {
        if (pMove.stunned) return;
        
        if (Math.Abs(pMove._inputX) > 0.1f || Mathf.Abs(pMove._inputY) > 0.1f)
        {
            transform.LookAt(transform.position + new Vector3(pMove._inputX * 40, pMove.body.velocity.y,pMove._inputY * 40));
        } 
        else if (Math.Abs(pMove.body.velocity.x) > 0.3f || Math.Abs(pMove.body.velocity.z) > 0.3f)
        {
            transform.LookAt(transform.position + new Vector3(pMove.body.velocity.x * 40, pMove.body.velocity.y,pMove.body.velocity.z * 40));
        }
    }

    void ResetView()
    {
        Vector3 look = new Vector3(transform.forward.x, 0, transform.forward.z);
        transform.LookAt(transform.position + look);
    }
}
