using UnityEngine;

public class PlayerBorder : Hittable
{
    private bool wasHit = false;

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Ball"))
        {
            if (!wasHit)
            {
                wasHit = true;
                RaiseBallHit(other.gameObject.GetComponent<Ball>(), this);
            }
        }
    }
}
