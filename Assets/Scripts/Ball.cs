using UnityEngine;

public class Ball : MonoBehaviour
{
    Vector3 startingPosition = new Vector3(0f, 1.5f, 0f);
    Vector3 startingSpeed = new Vector3(0f, 8f, 0f);
    Rigidbody rb;

    // Use this for initialization
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        reset();
    }

    public void reset()
    {
        rb.velocity = Vector3.zero;
        this.gameObject.transform.localPosition = startingPosition;
        gameObject.transform.parent = GameObject.FindGameObjectWithTag("Player").transform;
    }

    public void Shoot()
    {
        rb.velocity = startingSpeed;
        transform.parent = null;
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            //if we collide with a player, we become faster
            rb.velocity *= 1.1f;
            //we also gain a little speed away from his center
            float addSpeed = (this.gameObject.transform.position.x - collision.gameObject.transform.position.x);
            rb.velocity += new Vector3(addSpeed, 0, 0);
        }

    }
    void OnTriggerEnter(Collider other)
    {
        // if (other.gameObject.CompareTag("Border"))
        // {
        //     Invoke("reset", 1);
        // }
    }
}
