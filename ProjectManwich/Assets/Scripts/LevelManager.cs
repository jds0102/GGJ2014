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
	private bool m_sliding; 
	private int m_nextLevel;
	private float m_transitionSpeed = 50.0f;
	private int m_slideDirection = 1;

	private Rect tempScore = new Rect(0,0,300,150);

	// Use this for initialization
	void Start () {
		m_singleton = this;
		//m_center = levels [m_activeLevel].transform.position;
		m_center = new Vector3 (0, 0, 0);

	}

	//TEMP
	void OnGUI() {
		GUI.Label (tempScore, "$" + PlayerManager.m_singleton.m_players [0].m_money);
	}


	// Update is called once per frame
	void Update () {
		if(Input.GetMouseButtonDown(1)) {
			if(m_activeLevel == 0) {
				SwitchToLevel(1);
			} else if(m_activeLevel == 1) {
				SwitchToLevel(0);
			}
		}

		if (m_sliding) {
			float distanceToCenter = levels[m_nextLevel].transform.FindChild("LevelCenter").position.y - m_center.y;
			if ( Mathf.Abs(distanceToCenter) < 1) {
				float deltaY = distanceToCenter;
				Vector3 temp;
				foreach(GameObject level in levels) {
					temp = level.transform.position;
					temp.y = temp.y - deltaY;
					level.transform.position = temp;
				}
				EndTransition();
			} else {
				float deltaY = m_transitionSpeed*Time.deltaTime*m_slideDirection;
				Vector3 temp;
				foreach(GameObject level in levels) {
					temp = level.transform.position;
					temp.y = temp.y - deltaY;
					level.transform.position = temp;
				}

			}
		}
	}

	void SwitchToLevel(int levelNum) {
		m_nextLevel = levelNum;
		StartTransition ();
	}

	void StartTransition() {
		m_sliding = true;
		if (m_activeLevel > m_nextLevel) {
			m_slideDirection = -1;
		} else {
			m_slideDirection = 1;
		}
		Vector3 temp = levels [m_nextLevel].transform.position;
		temp.x -= (levels[m_nextLevel].transform.FindChild ("LevelCenter").position.x - m_center.x);
		levels [m_nextLevel].transform.position = temp;
		GameManager.m_singleton.PauseWorld ();
	}

	void EndTransition() {
		GameManager.m_singleton.UnPauseWorld ();

		m_sliding = false;
		m_activeLevel = m_nextLevel;
	}
}
