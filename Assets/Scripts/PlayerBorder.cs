using System.Collections.Generic;
using UnityEngine;

public class PlayerBorder : Hittable
{
    [Tooltip("Speed a ball gains when it first hit's the wall.")]
    public float speedGain = 4f;

    private List<Ball> ballsWhichHitAlready = new List<Ball>();

    public void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Ball"))
        {
            Ball ball = other.gameObject.GetComponent<Ball>();
            if (!ballsWhichHitAlready.Contains(ball))
            {
                ballsWhichHitAlready.Add(ball);
                RaiseBallHit(ball, this);
            }
        }
    }
}
