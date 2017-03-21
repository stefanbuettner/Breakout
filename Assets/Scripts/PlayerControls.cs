using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class PlayerControls : MonoBehaviour
{
    public GameObject ballPrefab;
    public Camera playerCam;

    public Vector3 initialBallPosition = new Vector3(0f, 1.5f, 0f);
    public Vector3 initialBallSpeed = new Vector3(0f, 8f, 0f);
    private Ball ballToShoot;


    public void Reset()
    {
        SpawnNewBall();
    }

    public void SpawnNewBall()
    {
        if (ballToShoot != null)
            Destroy(ballToShoot.gameObject);

        GameObject newBall = GameObject.Instantiate(ballPrefab, transform) as GameObject;
        newBall.transform.localPosition = initialBallPosition;
        ballToShoot = newBall.GetComponent<Ball>();
    }

    // Update is called once per frame
    void Update()
    {

        // Vector3 mousePos = CrossPlatformInputManager.mousePosition;
        // Ray r = playerCam.ScreenPointToRay(mousePos);
        // mousePos = r.origin;
        // mousePos.y = transform.position.y;
        // mousePos.z = transform.position.z;
        // transform.position = Vector3.MoveTowards(transform.position, mousePos, 1.0f);

        float movement = CrossPlatformInputManager.GetAxis("Mouse X");
        this.gameObject.transform.Translate(movement, 0, 0);


        if (CrossPlatformInputManager.GetButtonUp("Fire1"))
        {
            if (ballToShoot != null)
            {
                ballToShoot.Shoot(initialBallSpeed);
                ballToShoot = null;
            }
        }
    }
}
