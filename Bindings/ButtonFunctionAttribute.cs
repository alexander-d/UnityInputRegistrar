using System;
using UnityEngine;

public class ButtonFunctionAttribute : PropertyAttribute
{
	public Type Type;

	public ButtonFunctionAttribute(Type type)
	{
		this.Type = type;
	}
}