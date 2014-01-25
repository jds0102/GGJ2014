using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

	public static GameManager m_singleton;

	public GameObject player;

	// Use this for initialization
	void Start () {
		m_singleton = this;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
