using UnityEngine;
using System.Collections;

public class PlayerSignin : MonoBehaviour
{
    private bool m_enabled = false;
    private GameObject m_mainMenu;
    private GameObject m_uiElements;

    private Transform m_player1Element;
    private Transform m_player2Element;
    private Transform m_player3Element;
    private Transform m_player4Element;

    private int m_prevSelectedIndex = -1;
    private int m_selectedIndex = 0;

    // Use this for initialization
    void Start()
    {
        m_mainMenu = transform.parent.FindChild("Main Menu").gameObject;
        m_uiElements = transform.FindChild("UIElements").gameObject;

        m_player1Element = m_uiElements.transform.FindChild("Player1");
        m_player2Element = m_uiElements.transform.FindChild("Player2");
        m_player3Element = m_uiElements.transform.FindChild("Player3");
        m_player4Element = m_uiElements.transform.FindChild("Player4");
    }

    void Update()
    {
        if (!m_enabled) {
            return;
        }

        //if (Input.GetKeyDown(KeyCode.Escape)) {
        //    EnableUIElements(false);
        //    m_mainMenu.GetComponent<MainMenu>().EnableUIElements(true);
        //}

        if (Input.GetKeyDown(KeyCode.Return) || Input.GetButtonDown("Pause")) {
			AudioManager.Singleton.FadeBetweenLevels(0,1,1);
			Application.LoadLevel("Level"); 
        }

        if (Input.GetButtonDown("Player1Jump")) {
            // Check if a player has already been registered for this input
            Player p = PlayerManager.GetPlayerByInputLayer(1);
            if (p == null) {
                PlayerManager.AddPlayer(1);

                m_player1Element.FindChild("GUI Element - Join").gameObject.GetComponent<GUIText>().text = "is Ready!";
            }
        }

        if (Input.GetButtonDown("Player2Jump")) {
            // Check if a player has already been registered for this input
            Player p = PlayerManager.GetPlayerByInputLayer(2);
            if (p == null) {
                PlayerManager.AddPlayer(2);

                m_player2Element.FindChild("GUI Element - Join").gameObject.GetComponent<GUIText>().text = "is Ready!";
            }
        }

        if (Input.GetButtonDown("Player3Jump")) {
            // Check if a player has already been registered for this input
            Player p = PlayerManager.GetPlayerByInputLayer(3);
            if (p == null) {
                PlayerManager.AddPlayer(3);

                m_player3Element.FindChild("GUI Element - Join").gameObject.GetComponent<GUIText>().text = "is Ready!";
            }
        }

        if (Input.GetButtonDown("Player4Jump")) {
            // Check if a player has already been registered for this input
            Player p = PlayerManager.GetPlayerByInputLayer(4);
            if (p == null) {
                PlayerManager.AddPlayer(4);

                m_player4Element.FindChild("GUI Element - Join").gameObject.GetComponent<GUIText>().text = "is Ready!";
            }
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
