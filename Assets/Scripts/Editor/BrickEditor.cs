﻿using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Brick))]
public class BrickEditor : Editor
{
	Object[] brickPrefabs;
	private string[] brickTypeNames;
	private int selectedIndex;

	public override void OnInspectorGUI()
	{
		// Pull all the information from the target into the serializedObject.
        serializedObject.Update ();

        // Create a Rect for the full width of the inspector with enough height for the drop area.
        Rect fullWidthRect = GUILayoutUtility.GetRect(GUIContent.none, GUIStyle.none, GUILayout.Height(30.0f));
		fullWidthRect.y += 5f;

		SetReactionNamesArray();

        // Display a popup in the top half showing all the reaction types.
        selectedIndex = EditorGUI.Popup(fullWidthRect, selectedIndex, brickTypeNames);

		//if (PrefabUtility.FindPrefabRoot)

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
