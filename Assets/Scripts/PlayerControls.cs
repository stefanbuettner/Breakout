using UnityEngine;

public class PlayerControls : MonoBehaviour
{
    public GameObject ballPrefab;

    public Vector3 ballPosition = new Vector3(0f, 1.5f, 0f);
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
        newBall.transform.localPosition = ballPosition;
        ballToShoot = newBall.GetComponent<Ball>();
    }

    // Update is called once per frame
    void Update()
    {
        float movement = Input.GetAxis("Mouse X");
        this.gameObject.transform.Translate(movement, 0, 0);

        if (Input.GetButtonUp("Fire1"))
        {
            if (ballToShoot != null)
            {
                ballToShoot.Shoot();
                ballToShoot = null;
            }
        }
    }
}
