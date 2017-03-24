using UnityEngine;

[RequireComponent(typeof(ConfigurableJoint))]
public class PlayerControls : MonoBehaviour
{
    public GameObject ballPrefab;
    private GameObject paddle;

    public Vector3 initialBallPosition = new Vector3(0f, 1.5f, 0f);
    [SerializeField]
    private float defaultShotSpeed = 8f;
    [HideInInspector]
    public float shotSpeed;
    private Ball ballToShoot;

    public float paddleSpeed = 2f;

    void Awake()
    {
        paddle = GameObject.FindGameObjectWithTag("Paddle");
        GetComponent<ConfigurableJoint>().connectedBody = paddle.GetComponent<Rigidbody>();
        Reset();
    }

    public void Reset()
    {
        SpawnNewBall();
        shotSpeed = defaultShotSpeed;
    }

    public void TurnReset()
    {
        SpawnNewBall();
    }

    public void SpawnNewBall()
    {
        if (ballToShoot != null)
            Destroy(ballToShoot.gameObject);

        GameObject newBall = GameObject.Instantiate(ballPrefab, paddle.transform) as GameObject;
        newBall.transform.localPosition = initialBallPosition;
        ballToShoot = newBall.GetComponent<Ball>();
    }

    // Update is called once per frame
    void Update()
    {

        // Vector3 mousePos = CrossPlatformInputManager.mousePosition;
        Vector3 mousePos = Input.mousePosition;
        Ray r = Camera.main.ScreenPointToRay(mousePos);
        mousePos = r.origin;
        mousePos.y = transform.position.y;
        mousePos.z = transform.position.z;
        transform.position = Vector3.MoveTowards(transform.position, mousePos, 2.0f);
        //transform.position = mousePos;

        //float movement = CrossPlatformInputManager.GetAxis("Mouse X");
        //float movement = Input.GetAxis("Mouse X");
        //this.gameObject.transform.Translate(movement, 0, 0);

        //if (CrossPlatformInputManager.GetButtonUp("Fire1"))
        if (Input.GetButtonUp("Fire1"))
        {
            if (ballToShoot != null)
            {
                ballToShoot.Shoot(Vector3.up * shotSpeed);
                ballToShoot = null;
            }
        }
    }
}
