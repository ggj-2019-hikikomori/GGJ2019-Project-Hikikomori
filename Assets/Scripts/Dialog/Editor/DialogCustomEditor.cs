using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Dialog))]
public class DialogCustomEditor : Editor {


	private static GUILayoutOption miniButtonWidth = GUILayout.Width(20f);

	private static GUIContent moveButtonContent = new GUIContent("\u21b4", "move down");
	private static GUIContent duplicateButtonContent = new GUIContent("+", "duplicate");
	private static GUIContent deleteButtonContent = new GUIContent("-", "delete");

	public enum ListType
	{
		DialogStep,
		StoryVariable,
		Choice
	}

	private void OnEnable()
	{
	}

	public override void OnInspectorGUI()
	{

		serializedObject.Update();
		serializedObject.FindProperty("unlocalizedName").stringValue = EditorGUILayout.TextField("Unlocalized Name", serializedObject.FindProperty("unlocalizedName").stringValue);
		serializedObject.FindProperty("displaySpeedInverse").floatValue = EditorGUILayout.FloatField("Display Speed Inverse", serializedObject.FindProperty("displaySpeedInverse").floatValue);
		ShowElements("Conditions", serializedObject.FindProperty("conditions"), ListType.StoryVariable);
		ShowElements("Dialog Steps", serializedObject.FindProperty("dialogSteps"), ListType.DialogStep);

		serializedObject.ApplyModifiedProperties();
	}

	private static void ShowElements(string category, SerializedProperty list, ListType type)
	{

		list.isExpanded = EditorGUILayout.Foldout(list.isExpanded, category, true);
		if (list.isExpanded)
		{

			if (list.arraySize == 0)
			{
				if (GUILayout.Button(duplicateButtonContent))
				{
					list.InsertArrayElementAtIndex(0);
				}
			}
			for (int i = 0; i < list.arraySize; i++)
			{
				EditorGUI.indentLevel++;
				EditorGUILayout.BeginHorizontal();
				GUILayout.Space(EditorGUI.indentLevel * 20);
				GUILayout.BeginVertical(EditorStyles.helpBox);
				if (type == ListType.DialogStep)
				{
					DialogStepFied(list.GetArrayElementAtIndex(i), i);
				}
				else if (type == ListType.StoryVariable)
				{
					StoryVariableField(list.GetArrayElementAtIndex(i));
				}
				else if (type == ListType.Choice)
				{
					ChoiceField(list.GetArrayElementAtIndex(i));
				}
				ShowButtons(list, i);
				GUILayout.EndVertical();
				EditorGUILayout.EndHorizontal();
				EditorGUI.indentLevel--;
			}

		}

	}

	private static void DialogStepFied(SerializedProperty dialogStep, int index)
	{
		dialogStep.isExpanded = EditorGUILayout.Foldout(dialogStep.isExpanded, "Dialog Step (" + index + ")", true);
		if (dialogStep.isExpanded)
		{
			dialogStep.FindPropertyRelative("text").stringValue = EditorGUILayout.TextField("Text", dialogStep.FindPropertyRelative("text").stringValue);
			dialogStep.FindPropertyRelative("player").boolValue = EditorGUILayout.Toggle("Player", dialogStep.FindPropertyRelative("player").boolValue);
			dialogStep.FindPropertyRelative("internalThoughts").boolValue = EditorGUILayout.Toggle("Internal Thoughts", dialogStep.FindPropertyRelative("internalThoughts").boolValue);
			dialogStep.FindPropertyRelative("delayBeforeDisplay").floatValue = EditorGUILayout.FloatField("Delay Before Display", dialogStep.FindPropertyRelative("delayBeforeDisplay").floatValue);

			var choiceList = dialogStep.FindPropertyRelative("choices");
			ShowElements("Choices", dialogStep.FindPropertyRelative("choices"), ListType.Choice);
			if(choiceList.arraySize == 0)
				dialogStep.FindPropertyRelative("next").intValue = EditorGUILayout.IntField("Next Step", dialogStep.FindPropertyRelative("next").intValue);
			ShowElements("Variable Updates", dialogStep.FindPropertyRelative("variableUpdates"), ListType.StoryVariable);
			EditorGUILayout.PropertyField(dialogStep.FindPropertyRelative("animation"), true);
		}
	}

	private static void StoryVariableField(SerializedProperty storyVariable)
	{
		storyVariable.FindPropertyRelative("name").stringValue = EditorGUILayout.TextField("Name", storyVariable.FindPropertyRelative("name").stringValue);
		storyVariable.FindPropertyRelative("value").intValue = EditorGUILayout.IntField("Value", storyVariable.FindPropertyRelative("value").intValue);
	}

	private static void ChoiceField(SerializedProperty choice)
	{
		choice.FindPropertyRelative("name").stringValue = EditorGUILayout.TextField("Name", choice.FindPropertyRelative("name").stringValue);
		choice.FindPropertyRelative("target").intValue = EditorGUILayout.IntField("Target", choice.FindPropertyRelative("target").intValue);
	}

	private static void ShowButtons(SerializedProperty list, int index)
	{
		EditorGUILayout.BeginHorizontal();
		EditorGUILayout.Space();
		if (GUILayout.Button(moveButtonContent, EditorStyles.miniButtonLeft, miniButtonWidth))
		{
			list.MoveArrayElement(index, index + 1);
		}
		if (GUILayout.Button(duplicateButtonContent, EditorStyles.miniButtonMid, miniButtonWidth))
		{
			list.InsertArrayElementAtIndex(index);
		}
		if (GUILayout.Button(deleteButtonContent, EditorStyles.miniButtonRight, miniButtonWidth))
		{
			list.DeleteArrayElementAtIndex(index);
		}
		EditorGUILayout.EndHorizontal();
	}
}
