using UnityEngine;

[ExecuteInEditMode]
public class Brick : MonoBehaviour
{
	[SerializeField, HideInInspector]
	private GameObject m_BrickInstance;

	void Awake()
	{
		if (m_BrickInstance == null)
		{
			Object stdBrick = Resources.Load("Bricks/StandardBrick");
			m_BrickInstance = UnityEditor.PrefabUtility.InstantiatePrefab(stdBrick) as GameObject;
			m_BrickInstance.transform.parent = transform;
		}
	}
}
