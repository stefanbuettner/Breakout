using UnityEngine;

public class PlayerControls : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        float movement = Input.GetAxis("Mouse X");
        this.gameObject.transform.Translate(movement, 0, 0);
    }
}
