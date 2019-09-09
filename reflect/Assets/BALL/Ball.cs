using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TestTools;

public class Ball : MonoBehaviour
{

    public static GameObject GameBall;

    private void Awake()
    {
        GameBall = this.gameObject;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
