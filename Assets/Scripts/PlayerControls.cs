using UnityEngine;

public class PlayerControls : MonoBehaviour
{
    public Ball ballToShoot;

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
