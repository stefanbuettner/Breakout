using UnityEngine;
using UnityEditor;

public class editorScripts : MonoBehaviour
{
	[MenuItem("Breakout/PrefabInfo")]
	static void Blaa()
	{
		Debug.Log((PrefabUtility.GetPrefabObject(Selection.activeGameObject).name));
	}
}
