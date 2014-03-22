using UnityEngine;
using InControl;
using System.Collections;
using System.Collections.Generic;

public class MainMenu : MonoBehaviour
{

	public GUIStyle defaultStyle;
	public GUIStyle titleStyle;

	private Rect titleRect = new Rect(0,0,0,100);

    private bool m_enable = true;
    private GameObject m_uiElements;
    private GameObject m_playerSignIn;

    private GameObject m_newGameElement;
    private GameObject m_exitElement;
    private List<GameObject> m_elements;

    private int m_prevSelectedIndex = -1;
    private int m_selectedIndex = 0;

	// Use this for initialization
	void Start () {
        m_uiElements = transform.FindChild("UIElements").gameObject;
        m_playerSignIn = transform.parent.FindChild("Player SignIn").gameObject;

        m_newGameElement = m_uiElements.transform.FindChild("GUI Element - New Game").gameObject;
        m_exitElement = m_uiElements.transform.FindChild("GUI Element - Exit").gameObject;

        m_elements = new List<GameObject>();
        m_elements.Add(m_newGameElement);
        m_elements.Add(m_exitElement);

		AudioManager.Singleton.PlayMenuMusic ();
	}

    void Update()
    {
        if (!m_enable) {
            return;
        }

		InputDevice device = InputManager.ActiveDevice;

		if (device.Direction.y < 0) {
            m_selectedIndex++;
        }

		if (device.Direction.y > 0) {
            m_selectedIndex--;
        }

        if (m_selectedIndex < 0) {
            m_selectedIndex = 0;
        }

        if (m_selectedIndex >= 2) {
            m_selectedIndex = 1;
        }

        if (m_prevSelectedIndex != m_selectedIndex) {
            if (m_prevSelectedIndex >= 0) {
                m_elements[m_prevSelectedIndex].GetComponent<GUIElement>().selected = false;
                m_elements[m_prevSelectedIndex].GetComponent<GUIElement>().ResetFont();
            }


            m_elements[m_selectedIndex].GetComponent<GUIElement>().selected = true;
            m_prevSelectedIndex = m_selectedIndex;
        }

		if (device.Action1.WasPressed || device.GetControl(InputControlType.Start).WasPressed) {
            HandleSelection();
        }
    }

    void OnGUI()
    {
		if (m_enable) {
			GUIContent temp = new GUIContent("CLASS WARFAIRE");
			titleRect.width = titleStyle.CalcSize(temp).x;
			titleRect.x = (Screen.width / 2.0f) - (titleRect.width / 2.0f);
			titleRect.y = (Screen.height / 3.0f);
			GUI.Label(titleRect, temp, titleStyle);

		}
    }

    private void HandleSelection()
    {
        switch (m_selectedIndex) {
            case 0:
                // Load player sign in
                EnableUIElements(false);
                m_playerSignIn.GetComponent<PlayerSignin>().EnableUIElements(true);
                break;

//            case 1:
//                Debug.LogError("NYI");
//                break;

            case 1:
				Application.Quit();
				break;
            default:
                Application.Quit();
                break;
        }
    }

    public void EnableUIElements(bool enable)
    {
        m_enable = enable;
        m_uiElements.SetActive(enable);
    }
}
