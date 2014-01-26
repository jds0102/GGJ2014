using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CivilianSpawner : MonoBehaviour {

	public GameObject civilian;
	public int direction;
	public int maxCivilians;
	public Vector3 secondaryLocation;
	private List<GameObject> m_civilians;

	private float lastSpawnTime = 0;

	public static CivilianSpawner m_singleton;

	// Use this for initialization
	void Start () {
		m_singleton = this;
		m_civilians = new List<GameObject> ();
		if (direction == 0) {
			direction = 1;
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (Time.time - lastSpawnTime > 5 && m_civilians.Count < maxCivilians) {
			lastSpawnTime = Time.time;
			GameObject civ = (GameObject)Instantiate (civilian);
			if (Random.Range(-1,1) < 0) {
				civ.GetComponent<Civilian>().SetDirection(direction);
				civ.transform.position = this.transform.position;
			} else {
				civ.GetComponent<Civilian>().SetDirection(direction*-1);
				civ.transform.position = secondaryLocation;
			}
			m_civilians.Add (civ);
		}
	}

	public void KillCivilian(GameObject civToRemove) {
		int civToKill = -1;
		for(int i=0; i<m_civilians.Count; i++) {
			if(m_civilians[i] == civToRemove) {
				civToKill = i;
				break;
			}
		}
		if (civToKill != -1) {
			m_civilians.RemoveAt(civToKill);
		}
	}

	public void PauseForTransition() {
		foreach (GameObject civObj in m_civilians) {
			civObj.GetComponent<Civilian>().Pause();
		}
	}


	public void UnPauseForTransition() {
		foreach (GameObject civObj in m_civilians) {
			civObj.GetComponent<Civilian>().UnPause();
		}
	}
}
