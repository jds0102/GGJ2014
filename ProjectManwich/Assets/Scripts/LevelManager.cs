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
	private float m_slideDistance;
	private bool m_sliding; 
	private int m_nextLevel;
	private float m_transitionSpeed = 50.0f;

	// Use this for initialization
	void Start () {
		m_singleton = this;

		for(int i=0; i< levels.Count; i++) {
			if (i == m_activeLevel) {
				levels[i].SetActive(true);
			} else {
				levels[i].SetActive(false);
			}
		}
		m_center = levels [m_activeLevel].transform.position;
		m_slideDistance = Screen.height;
		Debug.Log (m_slideDistance);
	}

	// Update is called once per frame
	void Update () {
		if(Input.GetButtonDown("Jump")) {
			if(m_activeLevel == 0) {
				SwitchToLevel(1);
			} else if(m_activeLevel == 1) {
				SwitchToLevel(0);
			}
		}

		if (m_sliding) {
			float distanceToCenter = (levels[m_nextLevel].transform.position - m_center).magnitude;
			Debug.Log(distanceToCenter);
			if ( Mathf.Abs(distanceToCenter) < 2) {
				EndTransition();
			} else {
				Vector3 temp = levels[m_activeLevel].transform.position;
				temp.y -= m_transitionSpeed*Time.deltaTime;
				levels[m_activeLevel].transform.position = temp;
				temp = levels[m_nextLevel].transform.position;
				temp.y -= m_transitionSpeed*Time.deltaTime;
				levels[m_nextLevel].transform.position = temp;
			}
		}
	}

	void SwitchToLevel(int levelNum) {
		m_activeLevel = levelNum;

		for(int i=0; i< levels.Count; i++) {
			if (i == m_activeLevel) {
				levels[i].SetActive(true);
			} else {
				levels[i].SetActive(false);
			}
		}
		m_nextLevel = levelNum;
		StartTransition ();
	}

	void StartTransition() {
		m_sliding = true;
		Vector3 temp = levels [m_nextLevel].transform.position;
		temp.x = m_center.x;
		temp.y = m_center.y + m_slideDistance;
		levels [m_nextLevel].transform.position = temp;
		GameObject player = GameManager.m_singleton.player;
		player.GetComponent<Rigidbody2D>().isKinematic = true;
	}

	void EndTransition() {
		GameObject player = GameManager.m_singleton.player;
		player.GetComponent<Rigidbody2D> ().isKinematic = false;
		m_sliding = false;
		m_activeLevel = m_nextLevel;
	}
}
