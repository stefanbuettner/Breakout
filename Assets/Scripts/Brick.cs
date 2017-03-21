using UnityEngine;

[ExecuteInEditMode]
public class Brick : MonoBehaviour
{
	private BrickManager brickManager;

	public void SetBrickManager(BrickManager mgr)
	{
		brickManager = mgr;
	}
}
