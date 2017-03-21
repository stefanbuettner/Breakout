using UnityEngine;

[RequireComponent(typeof(Collider))]
public class BottomArea : MonoBehaviour
{
	private GameControl gameControl;

    // Use this for initialization
    void Start()
    {
		gameControl = FindObjectOfType<GameControl>();
    }

	void OnTriggerExit(Collider col)
	{
		if (col.CompareTag("Ball"))
		{
			gameControl.EndTurn();
			Destroy(col.gameObject);
		}
	}
}
