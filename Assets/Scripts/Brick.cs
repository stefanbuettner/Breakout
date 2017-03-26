using UnityEngine;

[ExecuteInEditMode]
public abstract class Brick : Hittable
{
	/** Used in Brick Editor by FindProperty! So don't rename. */
	[SerializeField, HideInInspector]
	private string prefabType = "";
	public void SetPrefabType(string prefabTypeName)
	{
		prefabType = prefabTypeName;
	}

	public abstract int GetHitpoints();

	public delegate void OnDestroyCallback(GameObject go);
	public event OnDestroyCallback OnDestroyEvent;

	void OnDestroy()
	{
		OnDestroyCallback handler = OnDestroyEvent;
		if (handler != null)
			handler(gameObject);
	}
}
