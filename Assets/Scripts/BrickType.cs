using UnityEngine;

[ExecuteInEditMode]
public class BrickType : MonoBehaviour
{
	[SerializeField, HideInInspector]
	private BrickManager brickManager;
	[HideInInspector]
	public string prefabType = "";

	public void SetBrickManager(BrickManager mgr)
	{
		brickManager = mgr;
	}
}
