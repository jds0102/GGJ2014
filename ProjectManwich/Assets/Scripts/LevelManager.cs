using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LevelManager : MonoBehaviour {

	public List<GameObject> levels;

	private int m_activeLevel = 0;
	// Use this for initialization
	void Start () {
		for(int i=0; i< levels.Count; i++) {
			if (i == m_activeLevel) {
				levels[i].SetActive(true);
			} else {
				levels[i].SetActive(false);
			}
		}
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
	}
}
