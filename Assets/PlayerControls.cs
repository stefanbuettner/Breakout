using UnityEngine;
using System.Collections;

public class PlayerControls : MonoBehaviour {

    Vector3 position;

    // Use this for initialization
    void Start () {
        position = this.gameObject.transform.position;
	}
	
	// Update is called once per frame
	void Update () {

        float movement = Input.GetAxis("Mouse X");
        this.gameObject.transform.Translate(movement,0,0);
    }

}
