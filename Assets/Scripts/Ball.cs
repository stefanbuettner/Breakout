using UnityEngine;

public class Ball : MonoBehaviour
{
    private MyRigidbody rb;
    public float horizontalSpeedGain = 1.5f;

    // Use this for initialization
    void Start()
    {
        rb = GetComponent<MyRigidbody>();
    }

    public void Shoot(Vector3 initialSpeed)
    {
        rb.velocity = initialSpeed;
    }

    void OnTriggerEnter(Collider collision)
    {
        if (collision.CompareTag("Paddle"))
        {
            //we also gain a little speed away from his center
            float addSpeed = (this.gameObject.transform.position.x - collision.gameObject.transform.position.x);
            float speed = rb.velocity.magnitude;
            rb.velocity += new Vector3(addSpeed * horizontalSpeedGain, 0, 0);
            rb.velocity = rb.velocity.normalized * speed;
        }
    }

    public delegate void BallDestroyedFunction(Ball ball);
    public event BallDestroyedFunction OnDestroyEvent;
    void OnDestroy()
    {
        BallDestroyedFunction handler = OnDestroyEvent;
        if (handler != null)
            handler(this);
    }
}
