using UnityEngine;

[RequireComponent(typeof(Collider))]
public class StandardBrick : MonoBehaviour
{
	void OnCollisionEnter(Collision col)
	{
		gameObject.SetActive(false);
	}
}
