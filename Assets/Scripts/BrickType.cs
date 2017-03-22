using UnityEngine;

[ExecuteInEditMode]
public class BrickType : MonoBehaviour
{
	[HideInInspector]
	public BrickManager brickManager;
	[HideInInspector]
	public string prefabType = "";

	void OnDestroy()
	{
		brickManager.OnBrickDestroyed(gameObject);
	}
}
