using UnityEngine;

[RequireComponent(typeof(ConfigurableJoint))]
public class HumanPlayer : Player
{
    private Paddle paddle;

    void Awake()
    {
        /* In case it was not set in the editor. */
        if (paddle == null)
            paddle = FindObjectOfType<Paddle>();
        GetComponent<ConfigurableJoint>().connectedBody = paddle.GetComponent<Rigidbody>();
    }

    public override void LevelReset()
    {
        paddle.LevelReset();
    }

    public override void TurnReset()
    {
        paddle.TurnReset();
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
            paddle.ShootBall(new Vector3(Random.Range(-1f, 1f), 1.0f, 0.0f));
        }
    }
}
