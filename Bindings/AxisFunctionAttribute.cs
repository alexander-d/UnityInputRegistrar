using System;
using UnityEngine;

public class AxisFunctionAttribute : PropertyAttribute
{
	public Type Type;

	public AxisFunctionAttribute(Type type)
	{
		this.Type = type;
	}
}