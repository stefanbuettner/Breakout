using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

// IngredientDrawer
[CustomPropertyDrawer(typeof(GameControl.SpeedGainEntry))]
public class SpeedGainEntryDrawer : PropertyDrawer
{
    // Draw the property inside the given rect
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        // Using BeginProperty / EndProperty on the parent property means that
        // prefab override logic works on the entire property.
        EditorGUI.BeginProperty(position, label, property);

        // Draw label
        position = EditorGUI.PrefixLabel(position, GUIUtility.GetControlID(FocusType.Passive), label);

        // Don't make child fields be indented
        var indent = EditorGUI.indentLevel;
        EditorGUI.indentLevel = 0;

        // Calculate rects
		const int LabelWidth = 35;
		Rect hitsLabelRect = new Rect(position.x, position.y, LabelWidth, position.height);
        Rect hitsRect = new Rect(position.x + LabelWidth, position.y, position.width / 3, position.height);
		Rect gainLabelRect = new Rect(position.x + LabelWidth + position.width / 3, position.y, LabelWidth, position.height);
        Rect gainRect = new Rect(position.x + 2 * LabelWidth + position.width / 3, position.y, position.width / 3, position.height);

        // Draw fields - passs GUIContent.none to each so they are drawn without labels
		EditorGUI.LabelField(hitsLabelRect, "Hits");
        EditorGUI.PropertyField(hitsRect, property.FindPropertyRelative("hits"), GUIContent.none);
		EditorGUI.LabelField(gainLabelRect, "Gain");
        EditorGUI.PropertyField(gainRect, property.FindPropertyRelative("gain"), GUIContent.none);

        // Set indent back to what it was
        EditorGUI.indentLevel = indent;

        EditorGUI.EndProperty();
    }
}
