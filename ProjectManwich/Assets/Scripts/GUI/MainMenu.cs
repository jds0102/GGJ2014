using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MainMenu : MonoBehaviour
{
    private GameObject m_newGameElement;
    private GameObject m_settingsElement;
    private GameObject m_exitElement;
    private List<GameObject> m_elements;

    private int m_prevSelectedIndex = -1;
    private int m_selectedIndex = 0;

	// Use this for initialization
	void Start () {
        m_newGameElement = transform.FindChild("GUI Element - New Game").gameObject;
        m_settingsElement = transform.FindChild("GUI Element - Settings").gameObject;
        m_exitElement = transform.FindChild("GUI Element - Exit").gameObject;

        m_elements = new List<GameObject>();
        m_elements.Add(m_newGameElement);
        m_elements.Add(m_settingsElement);
        m_elements.Add(m_exitElement);
	}

    void Update()
    {
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
                //Load Scene
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
}
