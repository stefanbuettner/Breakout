using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class MyRigidbody : MonoBehaviour
{
    /** Need this rigid body to receive trigger events with static colliders. */
	private Rigidbody rb;

    private Vector3 m_Velocity = Vector3.zero;

	public Vector3 velocity
	{
		get { return m_Velocity; }
		set { m_Velocity = value; }
	}

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        rb.useGravity = false;
        rb.isKinematic = true;
    }

    void Update()
    {
        //Debug.Log("v = " + velocity);
        transform.position += Time.deltaTime * velocity;
    }

    void OnTriggerEnter(Collider col)
    {
        // Prepare for some horrible code!
        // BRUTEFORCE 4tw!
        Bounds own = GetComponent<Collider>().bounds;
        Bounds other = col.bounds;
        Vector3 newVelocity = velocity;

        float leftPenetration = own.max.x - other.min.x;
        float rightPenetration = other.max.x - own.min.x;
        float topPenetration = other.max.y - own.min.y;
        float bottomPenetration = own.max.y - other.min.y;

        if ((leftPenetration < rightPenetration) &&
            (leftPenetration < topPenetration) &&
            (leftPenetration < bottomPenetration))
        {
            newVelocity.x = -newVelocity.x;
        }
        else if ((rightPenetration < leftPenetration) &&
                 (rightPenetration < topPenetration) && 
                 (rightPenetration < bottomPenetration))
        {
            newVelocity.x = -newVelocity.x;
        }
        else if ((topPenetration < leftPenetration) &&
                 (topPenetration < rightPenetration) &&
                 (topPenetration < bottomPenetration))
        {
            newVelocity.y = -newVelocity.y;
        }
        else if ((bottomPenetration < leftPenetration) &&
                 (bottomPenetration < rightPenetration) &&
                 (bottomPenetration < topPenetration))
        {
            newVelocity.y = -newVelocity.y;
        }
        velocity = newVelocity;
    }
}
