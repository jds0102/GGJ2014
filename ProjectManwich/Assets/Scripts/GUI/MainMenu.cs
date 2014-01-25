using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MainMenu : MonoBehaviour
{
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
	}

    void Update()
    {
        if (!m_enable) {
            return;
        }

        if (Input.GetKeyDown(KeyCode.DownArrow)) {
            m_selectedIndex++;
        }

        if (Input.GetKeyDown(KeyCode.UpArrow)) {
            m_selectedIndex--;
        }

        if (m_selectedIndex < 0) {
            m_selectedIndex = 0;
        }

        if (m_selectedIndex >= 3) {
            m_selectedIndex = 2;
        }

        if (m_prevSelectedIndex != m_selectedIndex) {
            if (m_prevSelectedIndex >= 0) {
                m_elements[m_prevSelectedIndex].GetComponent<GUIElement>().selected = false;
                m_elements[m_prevSelectedIndex].GetComponent<GUIElement>().ResetFont();
            }


            m_elements[m_selectedIndex].GetComponent<GUIElement>().selected = true;
            m_prevSelectedIndex = m_selectedIndex;
        }

        if (Input.GetKeyDown(KeyCode.Space)) {
            HandleSelection();
        }
    }

    void OnGUI()
    {

    }

    private void HandleSelection()
    {
        switch (m_selectedIndex) {
            case 0:
                // Load player sign in
                EnableUIElements(false);
                m_playerSignIn.GetComponent<PlayerSignin>().EnableUIElements(true);
                break;

            case 1:
                Debug.LogError("NYI");
                break;

            case 2:
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
