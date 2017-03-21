using UnityEngine;

public class PlayerControls : MonoBehaviour
{
    public GameObject ballPrefab;
    private Ball ballToShoot;

    public void Reset()
    {
        SpawnNewBall();
    }

    public void SpawnNewBall()
    {
        if (ballToShoot != null)
            Destroy(ballToShoot.gameObject);

        GameObject newBall = Instantiate(ballPrefab, transform) as GameObject;
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
