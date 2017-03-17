using UnityEngine;
using System.Collections;

public class Ball : MonoBehaviour {

    Vector3 startingPosition = Vector3.zero;
    Vector3 startingSpeed = new Vector3(0f,8f,0f);
    Rigidbody rb;

	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody>();
        reset();
    }

    public void reset() {
        rb.velocity = Vector3.zero;
        this.gameObject.transform.position = startingPosition;
        Invoke("resetStartingSpeed", 1);
    }

    void resetStartingSpeed() {
        rb.velocity = startingSpeed;
    }

	
	// Update is called once per frame
	void Update () {
	
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
        if (other.gameObject.CompareTag("Border"))
        {
            Invoke("reset", 1);
        }
    }
}
