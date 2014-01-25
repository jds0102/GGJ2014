using UnityEngine;
using System.Collections;

public class PlayerSignin : MonoBehaviour
{
    private bool m_enabled = false;
    private GameObject m_mainMenu;
    private GameObject m_uiElements;

    private int m_prevSelectedIndex = -1;
    private int m_selectedIndex = 0;

    // Use this for initialization
    void Start()
    {
        m_mainMenu = transform.parent.FindChild("Main Menu").gameObject;
        m_uiElements = transform.FindChild("UIElements").gameObject;
    }

    void Update()
    {
        if (!m_enabled) {
            return;
        }

        if (Input.GetKeyDown(KeyCode.Escape)) {
            EnableUIElements(false);
            m_mainMenu.GetComponent<MainMenu>().EnableUIElements(true);
        }


        if (Input.GetKeyDown(KeyCode.Space)) {
            Application.LoadLevel("TestLevel");
        }

        if (Input.GetButton("Player1" + "Jump")) {
            PlayerManager.AddPlayer(1);
        }

        if (Input.GetButton("Player2" + "Jump")) {

        }
        
        if (Input.GetButton("Player3" + "Jump")) {

        }

        if (Input.GetButton("Player4" + "Jump")) {

        }


    }

    void OnGUI()
    {

    }

    private void HandleSelection()
    {
        
    }

    public void EnableUIElements(bool enable)
    {
        m_enabled = enable;
        m_uiElements.SetActive(enable);
    }
}
