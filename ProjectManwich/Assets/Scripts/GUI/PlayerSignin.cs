using UnityEngine;
using InControl;
using System.Collections;
using System.Collections.Generic;

public class PlayerSignin : MonoBehaviour
{
	public Texture xboxSignInSprite, ps3SignInSprite;
	public GUIStyle defaultStyle;

	private Rect testRect = new Rect(0, 0, 0, 50);
	private Rect pressRect = new Rect(0,0,0,50);
	private float totalPressTextWidth = 0;
	private bool[] m_signedIn = {false, false, false, false};

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
        //m_uiElements = transform.FindChild("UIElements").gameObject;

//        m_player1Element = m_uiElements.transform.FindChild("Player1");
//        m_player2Element = m_uiElements.transform.FindChild("Player2");
//        m_player3Element = m_uiElements.transform.FindChild("Player3");
//        m_player4Element = m_uiElements.transform.FindChild("Player4");

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
					m_signedIn[i-1] = true;
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
		if (m_enabled) {
			for(int playerNum=1; playerNum <= 4; playerNum++) {
				DrawSignInText(playerNum, m_signedIn[playerNum-1]);
			}

			//Draw the thing that says "Press Start", again get rid of the "new"s
			defaultStyle.fontSize += 25;
			Rect pressEnterRect = new Rect(Screen.width/2.0f, Screen.height/2.0f,0, 50);
			GUIContent pressEnterContent = new GUIContent("Press 'Start' to begin.");
			pressEnterRect.width = defaultStyle.CalcSize(pressEnterContent).x;
			pressEnterRect.x -= pressEnterRect.width/2.0f;
			GUI.Label(pressEnterRect, pressEnterContent, defaultStyle);
			defaultStyle.fontSize += 25;
		}
    }

    private void HandleSelection()
    {
        
    }

    public void EnableUIElements(bool enable)
    {
        m_enabled = enable;
    }

	//Only call within OnGUI
	//TODO Need to go back through this and remove terrible variable names and get rid of every time I use "new"
	private void DrawSignInText(int player, bool signedIn)
	{
		//First draw the part that says "Player 1", make it big
		defaultStyle.fontSize = 75;
		Rect playerNumRect = new Rect (0, 0, 0, 100);
		playerNumRect.y = (Screen.height / 4.0f) - 25;
		if (player > 2) {
			playerNumRect.y += Screen.height / 2.0f;
		}

		playerNumRect.y -= 50;

		GUIContent playerNumContent = new GUIContent ("Player " + player);
		playerNumRect.width = defaultStyle.CalcSize(playerNumContent).x;
		playerNumRect.x = (Screen.width / 4.0f) - (playerNumRect.width/ 2.0f);
		if (player % 2 == 0) {
			playerNumRect.x += Screen.width / 2.0f;
		}

		GUI.Label (playerNumRect, playerNumContent, defaultStyle);

		//Now either draw what button to press, or indicate that the player is signed in
		defaultStyle.fontSize = 30;
		if (!signedIn) {
			testRect.y = (Screen.height / 4.0f) - 25;
			if (player > 2) {
					testRect.y += Screen.height / 2.0f;
			}

			testRect.width = totalPressTextWidth;
			testRect.x = (Screen.width / 4.0f) - (totalPressTextWidth / 2.0f);
			if (player % 2 == 0) {
					testRect.x += Screen.width / 2.0f;
			}
			testRect.y += 50;

			GUIContent tempContent;
			GUI.BeginGroup (testRect);
			tempContent = new GUIContent ("Press ");
			pressRect.width = defaultStyle.CalcSize (tempContent).x;
			GUI.Label (pressRect, tempContent, defaultStyle);
			pressRect.x += pressRect.width + 7;
			pressRect.width = 50;
			GUI.DrawTexture (pressRect, xboxSignInSprite);
			tempContent = new GUIContent (" OR ");
			pressRect.x += pressRect.width;
			pressRect.width = defaultStyle.CalcSize (tempContent).x;
			GUI.Label (pressRect, tempContent, defaultStyle);
			pressRect.x += pressRect.width + 7;
			pressRect.width = 50;
			GUI.DrawTexture (pressRect, ps3SignInSprite);
			tempContent = new GUIContent (" to sign in. ");
			pressRect.x += pressRect.width;
			pressRect.width = defaultStyle.CalcSize (tempContent).x;
			GUI.Label (pressRect, tempContent, defaultStyle);
			if (totalPressTextWidth == 0) {
					totalPressTextWidth = pressRect.x + pressRect.width;
			}
			pressRect.width = pressRect.x = 0;
			GUI.EndGroup ();
		} else {
			testRect.y = (Screen.height / 4.0f) - 25;
			if (player > 2) {
				testRect.y += Screen.height / 2.0f;
			}
			testRect.y += 50;
			testRect.width = defaultStyle.CalcSize(new GUIContent("Signed In")).x;
			testRect.x = (Screen.width / 4.0f) - (testRect.width / 2.0f);
			if (player % 2 == 0) {
				testRect.x += Screen.width / 2.0f;
			}
			GUI.Label(testRect, "Signed In", defaultStyle);
		}
	}
}
