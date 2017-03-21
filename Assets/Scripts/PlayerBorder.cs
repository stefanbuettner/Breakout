using System.Collections.Generic;
using UnityEngine;

public class PlayerBorder : MonoBehaviour
{
    private PlayerControls player;

    [Tooltip("Speed a ball gains when it first hit's the wall.")]
    public float speedGain = 4f;

    private List<Ball> ballsWhichHitAlready = new List<Ball>();

    // Use this for initialization
    void Start()
    {
        player = FindObjectOfType<PlayerControls>();
    }

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Ball"))
        {
            Ball ball = other.gameObject.GetComponent<Ball>();
            if (!ballsWhichHitAlready.Contains(ball))
            {
                ballsWhichHitAlready.Add(ball);
                Rigidbody ballRB = ball.GetComponent<Rigidbody>();
                ballRB.velocity += ballRB.velocity.normalized * speedGain;
                player.initialBallSpeed += player.initialBallSpeed.normalized * speedGain;
                Debug.Log("Speed gained by hitting " + name + ": " + speedGain);
            }
        }
    }
}
