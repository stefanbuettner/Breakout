using UnityEngine;

[ExecuteInEditMode]
public class Brick : MonoBehaviour
{
	private BrickManager brickManager;
	[HideInInspector]
	public string prefabType = "";

	public void SetBrickManager(BrickManager mgr)
	{
		brickManager = mgr;
	}
}
