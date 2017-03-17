﻿using UnityEngine;
using System.Collections;


public class GameController : MonoBehaviour
{

    public PlayerBorder borderTop, borderBottom;
    private TextMesh tm;

    // Use this for initialization
    void Start()
    {
        tm = GetComponent<TextMesh>();
        rewrite();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            reset();
        }
    }

    void reset()
    {
        borderTop.timesHit = 0;
        borderBottom.timesHit = 0;
        rewrite();
    }

    public void rewrite()
    {
        tm.text = borderBottom.timesHit + "\n" + borderTop.timesHit;
    }
}