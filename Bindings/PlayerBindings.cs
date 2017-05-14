using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using InputRegistrar;

[CreateAssetMenu(fileName = "PlayerBindings", menuName = "SO/Player Bindings", order = 2)]
public class PlayerBindings : ScriptableObject
{
	public PlayerButtonBinding[] ButtonBindings;
	public PlayerAxisBinding[] AxisBindings;
}

[System.Serializable]
public struct PlayerButtonBinding
{
	public InputValue InputValue;
	public ButtonAction ButtonAction;
	public ButtonFunction Function;
}

[System.Serializable]
public struct PlayerAxisBinding
{
	public InputValue InputValue;
	public AxisAction AxisAction;
	public AxisFunction Function;
}