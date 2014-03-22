using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LevelManager : MonoBehaviour {

	public static LevelManager m_singleton;
	public List<GameObject> levels;

	public const int TRANSITION_TIME = 2;
	private float m_transitionStart =0;
	private int m_activeLevel = 0;
	private Vector3 m_center;
	//private bool m_sliding; 
	private bool m_fadingIn;
	private bool m_fadingOut;
	private int m_nextLevel;
	private float m_transitionSpeed = 50.0f;
	private int m_slideDirection = 1;

	private Rect tempScore = new Rect(0,0,300,150);
	private GUITexture m_fadeToBlack;
	//These values are per player, multiply times players in the game to find out what level you should display
	private int[] m_levelThresholds = {0, 600, 1200, 1800, 2200};

	// Use this for initialization
	void Start () {
		m_singleton = this;
		//m_center = levels [m_activeLevel].transform.position;
		m_center = new Vector3 (0, 0, 0);
		m_fadeToBlack = transform.FindChild ("FadeToBlack").gameObject.GetComponent<GUITexture>();
		m_fadeToBlack.color = new Color (0, 0, 0, 0);
	}


	// Update is called once per frame
	void Update () {

		float avgMoneyPerPlayer = 0;
		float totalPlayers = 0;
		foreach(Player p in PlayerManager.m_singleton.m_players) {
			if (p!=null) {
				avgMoneyPerPlayer+=p.m_money;
				totalPlayers++;
			}
		}
		avgMoneyPerPlayer /= totalPlayers;
		//Check which level we should display
		for(int i = m_levelThresholds.Length-1; i>=0; i--) {
			if (avgMoneyPerPlayer >= m_levelThresholds[i]) {
				if (i != m_activeLevel && !(m_fadingIn | m_fadingOut)) {
					SwitchToLevel(i);
				}
				break;
			}
		}
	
		if (m_fadingIn) {
			Color textureColor = m_fadeToBlack.color;
			textureColor.a = Mathf.Max(textureColor.a - Time.deltaTime, 0);
			m_fadeToBlack.color = textureColor;
			if (textureColor.a == 0) {
				m_fadingIn = false;
				EndTransition();
			}
		}

		if (m_fadingOut) {
			Color textureColor = m_fadeToBlack.color;
			textureColor.a = Mathf.Min(textureColor.a + Time.deltaTime, 1);
			m_fadeToBlack.color = textureColor;
			if (textureColor.a == 1) {
				m_fadingOut = false;
				FadeOutComplete();
			}
		}

	}

	void SwitchToLevel(int levelNum) {
		if (levelNum >= levels.Count) {
			Debug.LogError("We don't have a level " + levelNum + " yet!");
			return;
		}
		m_nextLevel = levelNum;
		StartTransition ();
	}

	void StartTransition() {
		AudioManager.Singleton.FadeBetweenLevels (m_activeLevel + 1, m_nextLevel + 1, 1);
		GameManager.m_singleton.PauseWorld ();

		//Start the fade to black, once done switch levels
		m_fadingOut = true;

		//I think instead we should destroy and re create the civs
		//CivilianSpawner.m_singleton.PauseForTransition ();
	}

	void EndTransition() {
		GameManager.m_singleton.UnPauseWorld ();

		//CivilianSpawner.m_singleton.UnPauseForTransition ();
		m_activeLevel = m_nextLevel;

        NotificationManager.CreateNewNotification("NEW LEVEL!");
	}

	void FadeOutComplete() 
	{
		//Here is where we make the switch
		foreach(Player p in PlayerManager.m_singleton.m_players) {
			if (p!=null) {
				p.ReturnToSpawn();
			}
		}
		m_fadingIn = true;
	}
}
