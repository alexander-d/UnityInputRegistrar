using UnityEngine;

[CreateAssetMenu(fileName = "Button Function", menuName = "SO/Player Button Function", order = 3)]
public class ButtonFunction : ScriptableObject
{
	[ButtonFunctionAttribute(typeof(Player))]
	public string Function;
	public string Name;
	public string Description;
}