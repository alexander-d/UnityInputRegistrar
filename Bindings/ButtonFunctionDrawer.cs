using UnityEngine;
using UnityEditor;
using System.Linq;
using System.Reflection;
using System.Collections.Generic;

[CustomPropertyDrawer(typeof(ButtonFunctionAttribute))]
public class ButtonFunctionDrawer : PropertyDrawer
{
	public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
	{
		string currentValue = property.stringValue;
		ButtonFunctionAttribute actionAttribute = attribute as ButtonFunctionAttribute;

		MethodInfo[] playerMethods = actionAttribute.Type.GetMethods(BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly).Where(mi => mi.GetParameters().Length == 0).ToArray();
		int length = playerMethods.Length;
		string[] playerMethodNames = playerMethods.Select(mi => mi.Name).ToArray();

		int currentValueIndex = -1;
		for (int i = 0; i < length; i++)
		{
			playerMethodNames[i] = playerMethods[i].Name;
			if (playerMethodNames[i].Equals(currentValue))
			{
				currentValueIndex = i;
			}
		}

		// if we didn't find our current value in the array, use the first element
		if (currentValueIndex == -1)
		{
			currentValueIndex = 0;
		}

		int newIndex = EditorGUI.Popup(position, label.text, currentValueIndex, playerMethodNames);

		property.stringValue = playerMethodNames[newIndex];
	}
}