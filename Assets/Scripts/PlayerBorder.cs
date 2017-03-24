using UnityEngine;

public class PlayerBorder : Hittable
{
    private bool wasHit = false;

    public void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Ball"))
        {
            if (!wasHit)
            {
                wasHit = true;
                RaiseBallHit(other.gameObject.GetComponent<Ball>(), this);
            }
        }
    }
}
