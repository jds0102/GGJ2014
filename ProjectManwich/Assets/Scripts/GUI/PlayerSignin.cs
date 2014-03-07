using UnityEngine;
using InControl;
using System.Collections;
using System.Collections.Generic;

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

	private List<string> devicesInUse;

    // Use this for initialization
    void Start()
    {
        m_mainMenu = transform.parent.FindChild("Main Menu").gameObject;
        m_uiElements = transform.FindChild("UIElements").gameObject;

        m_player1Element = m_uiElements.transform.FindChild("Player1");
        m_player2Element = m_uiElements.transform.FindChild("Player2");
        m_player3Element = m_uiElements.transform.FindChild("Player3");
        m_player4Element = m_uiElements.transform.FindChild("Player4");

		devicesInUse = new List<string>();
    }

    void Update()
    {
        if (!m_enabled) {
            return;
        }

		InputDevice activeDevice = InputManager.ActiveDevice;

		//If any player presses start and at least one player is logged in
		if (InputManager.ActiveDevice.GetControl (InputControlType.Start).WasPressed && PlayerManager.m_singleton.playerCount > 0) {
			AudioManager.Singleton.FadeBetweenLevels(0,1,1);
			Application.LoadLevel("Level"); 
        }

		bool playerAdded = false;
		//If login button pressed and the device that pressed it is not in use by a logged in player
		if (activeDevice.Action1.WasPressed && !devicesInUse.Contains(activeDevice.Meta)) {
			//Loop through each possible player num
			for (int i = 1; i <= 4 && !playerAdded; i++) {
	            // Check if this player has already been registered
	            Player p = PlayerManager.GetPlayerByInputLayer(i);
	            if (p == null) {
					playerAdded = true;
					devicesInUse.Add(activeDevice.Meta);
					//Passing the device to the player like this will break if the players device ever becomes disconnected
	                PlayerManager.AddPlayer(i,activeDevice);
					switch(i) {
					case 1:
						m_player1Element.FindChild("GUI Element - Join").gameObject.GetComponent<GUIText>().text = "is Ready!";
						break;
					case 2:
						m_player2Element.FindChild("GUI Element - Join").gameObject.GetComponent<GUIText>().text = "is Ready!";
						break;
					case 3:
						m_player3Element.FindChild("GUI Element - Join").gameObject.GetComponent<GUIText>().text = "is Ready!";
						break;
					case 4:
						m_player4Element.FindChild("GUI Element - Join").gameObject.GetComponent<GUIText>().text = "is Ready!";
						break;
					}

	            }
			} 
        
		}

//		if (InputManager.Devices.Count > 1) {
//			//Player 2 login
//			if (InputManager.Devices[1].Action1) {
//	            // Check if a player has already been registered for this input
//	            Player p = PlayerManager.GetPlayerByInputLayer(2);
//	            if (p == null) {
//	                PlayerManager.AddPlayer(2);
//
//	                m_player2Element.FindChild("GUI Element - Join").gameObject.GetComponent<GUIText>().text = "is Ready!";
//	            }
//	        }
//		}
//		if (InputManager.Devices.Count > 2) {
//			//Player 3 login
//			if (InputManager.Devices[2].Action1) {
//	            // Check if a player has already been registered for this input
//	            Player p = PlayerManager.GetPlayerByInputLayer(3);
//	            if (p == null) {
//	                PlayerManager.AddPlayer(3);
//
//	                m_player3Element.FindChild("GUI Element - Join").gameObject.GetComponent<GUIText>().text = "is Ready!";
//	            }
//	        }
//		}
//
//		if (InputManager.Devices.Count > 3) {
//			//Player 4 login
//			if (InputManager.Devices[3].Action1) {
//	            // Check if a player has already been registered for this input
//	            Player p = PlayerManager.GetPlayerByInputLayer(4);
//	            if (p == null) {
//	                PlayerManager.AddPlayer(4);
//
//	                m_player4Element.FindChild("GUI Element - Join").gameObject.GetComponent<GUIText>().text = "is Ready!";
//	            }
//	        }
//	    }
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
