using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Brick)), CanEditMultipleObjects]
public class BrickEditor : Editor
{
	Object[] brickPrefabs;
	private string[] brickTypeNames;

	public override void OnInspectorGUI()
	{
		// Pull all the information from the target into the serializedObject.
        serializedObject.Update ();

        // Create a Rect for the full width of the inspector with enough height for the drop area.
        Rect fullWidthRect = GUILayoutUtility.GetRect(GUIContent.none, GUIStyle.none, GUILayout.Height(30.0f));
		fullWidthRect.y += 5f;

		SetReactionNamesArray();

        SerializedProperty prefabTypeProp = serializedObject.FindProperty("prefabType");
        int selectedIndex = ArrayUtility.IndexOf(brickTypeNames, prefabTypeProp.stringValue);

        // Display a popup in the top half showing all the reaction types.
        int newSelectedIndex = EditorGUI.Popup(fullWidthRect, selectedIndex, brickTypeNames);
		
        if (newSelectedIndex != selectedIndex)
        {
            SerializedProperty brickMgrProp = serializedObject.FindProperty("brickManager");
            BrickManager brickMgr = brickMgrProp.objectReferenceValue as BrickManager;
            brickMgr.ChangeBrickTypeSelection(brickTypeNames[newSelectedIndex]);
            return;
        }

        // Push the information back from the serializedObject to the target.
        serializedObject.ApplyModifiedProperties ();
	}

	private void SetReactionNamesArray ()
    {
		brickPrefabs = Resources.LoadAll("Bricks");

        // Create an empty list of strings to store the names of the Reaction types.
        List<string> reactionTypeNameList = new List<string>();

        // Go through all the Reaction types and add their names to the list.
        for (int i = 0; i < brickPrefabs.Length; i++)
        {
            reactionTypeNameList.Add(brickPrefabs[i].name);
        }

        // Convert the list to an array and store it.
        brickTypeNames = reactionTypeNameList.ToArray();
    } 
}
