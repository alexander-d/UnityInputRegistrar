using UnityEngine;
using InputRegistrar;

public class Player : MonoBehaviour
{
	[Range(0, 4)]
	[SerializeField]
	private int m_controllerNumber;

	private InputManager m_inputManager;

	private void Start()
	{
		m_inputManager = new InputManager<PS4InputRegistrar, int>(m_controllerNumber);

		m_inputManager.RegisterButtonInput(new ButtonGesture(InputValue.Alpha, ButtonAction.OnPressDown), Foo);
	}

	private void Foo()
	{
		Debug.Log("Bar");
	}

	private void Update()
	{
		m_inputManager.Update();
	}
}
