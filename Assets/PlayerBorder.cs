using UnityEngine;
using System.Collections;

public class PlayerBorder : MonoBehaviour {

    public GameControl gc;
    public int timesHit;

	// Use this for initialization
	void Start () {
        timesHit = 0;
    }
	
	// Update is called once per frame
	void Update () {
	    
	}


    void OnTriggerEnter(Collider other) {
        if (other.gameObject.CompareTag("Ball")) {
            timesHit++;
            gc.rewrite();
        }
    }
}
