using UnityEngine;

public class Ball : MonoBehaviour
{
    private Rigidbody rb;
    public float horizontalSpeedGain = 1.5f;

    // Use this for initialization
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    public void Shoot(Vector3 initialSpeed)
    {
        rb.velocity = initialSpeed;
        transform.parent = null;
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            //we also gain a little speed away from his center
            float addSpeed = (this.gameObject.transform.position.x - collision.gameObject.transform.position.x);
            float speed = rb.velocity.magnitude;
            rb.velocity += new Vector3(addSpeed * horizontalSpeedGain, 0, 0);
            rb.velocity = rb.velocity.normalized * speed;
        }
    }
}
