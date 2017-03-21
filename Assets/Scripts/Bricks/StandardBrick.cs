using UnityEngine;

[RequireComponent(typeof(Collider))]
public class StandardBrick : MonoBehaviour
{
	private GameControl gameControl;
	public int hitPoints = 1;

	void Start()
	{
		gameControl = FindObjectOfType<GameControl>();
	}

	void OnCollisionEnter(Collision col)
	{
		if (col.collider.CompareTag("Ball"))
		{
			Debug.Log("Ball hit " + name);
			gameObject.SetActive(false);
			gameControl.points += hitPoints;
		}
	}
}
