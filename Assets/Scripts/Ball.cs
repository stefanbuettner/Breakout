using UnityEngine;

public class Ball : MonoBehaviour
{
    private Rigidbody rb;

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
            //if we collide with a player, we become faster
            //rb.velocity *= 1.1f;
            //we also gain a little speed away from his center
            float addSpeed = (this.gameObject.transform.position.x - collision.gameObject.transform.position.x);
            rb.velocity += new Vector3(addSpeed, 0, 0);
        }
    }
}
