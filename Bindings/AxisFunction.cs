using UnityEngine;

[CreateAssetMenu(fileName = "Axis Function", menuName = "SO/Player Axis Function", order = 4)]
public class AxisFunction : ScriptableObject
{
	[AxisFunctionAttribute(typeof(Player))]
	public string Function;
	public string Name;
	public string Description;
}