using UnityEngine;
using System;
using System.Reflection;
using InputRegistrar;
using System.Collections.Generic;

public class Player : MonoBehaviour
{
	[Range(0, 4)]
	[SerializeField]
	private int m_controllerNumber;

	[SerializeField]
	private PlayerBindings m_bindings;

	private InputManager m_inputManager;

	private void Start()
	{
		m_inputManager = new InputManager<XboxInputRegistrar, int>(m_controllerNumber);

		for (int i = 0; i < m_bindings.ButtonBindings.Length; i++)
		{
			PlayerButtonBinding pbb = m_bindings.ButtonBindings[i];
			string methodName = pbb.Function.Function;
			MethodInfo methodInfo = this.GetType().GetMethod(methodName, BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly);
			Action action = (System.Action)System.Delegate.CreateDelegate(typeof(Action), this, methodInfo);
			m_inputManager.RegisterButtonInput(new ButtonGesture(pbb.InputValue, pbb.ButtonAction), action);
		}

		for (int i = 0; i < m_bindings.AxisBindings.Length; i++)
		{
			PlayerAxisBinding pab = m_bindings.AxisBindings[i];
			string methodName = pab.Function.Function;
			MethodInfo mi = typeof(Player).GetMethod(methodName, BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly, null, new Type[1] { typeof(float) }, null);
			Action<float> action = (System.Action<float>)System.Delegate.CreateDelegate(typeof(Action<float>), this, mi);
			m_inputManager.RegisterAxisInput(new AxisGesture(pab.InputValue, pab.AxisAction), action);
		}

		ShowBindings();
	}

	private void Jump()
	{
		Debug.Log("Jump");
	}

	private void Crouch()
	{
		Debug.Log("Crouch");
	}

	private void Move(float f)
	{
		if (f != 0f)
			Debug.Log(f);
	}

	private void ShowBindings()
	{
		InputRegistrar<ButtonInput.Xbox, AxisInput.Xbox> ir = (InputRegistrar<ButtonInput.Xbox, AxisInput.Xbox>)m_inputManager.m_inputRegistrar;
		for (int i = 0; i < m_bindings.ButtonBindings.Length; i++)
		{
			PlayerButtonBinding pbb = m_bindings.ButtonBindings[i];
			string buttonName = ir.m_inputButtonBindings[pbb.InputValue].ToString();
			string methodName = pbb.Function.Name;
			string methodDescription = pbb.Function.Description;
			Debug.Log(string.Format("Press {0} to {1}: {2}", buttonName, methodName, methodDescription));
		}

		for (int i = 0; i < m_bindings.AxisBindings.Length; i++)
		{
			PlayerAxisBinding pab = m_bindings.AxisBindings[i];
			string axisName = ir.m_inputAxisBindings[pab.InputValue].ToString();
			string methodName = pab.Function.Name;
			string methodDescription = pab.Function.Description;
			Debug.Log(string.Format("Move {0} to {1}: {2}", axisName, methodName, methodDescription));
		}
	}

	private void Update()
	{
		m_inputManager.Update();
	}
}