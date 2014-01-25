using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

	public static GameManager m_singleton;

	public GameObject player;

	private GameObject m_enemyParent;

	// Use this for initialization
	void Start () {
		m_singleton = this;
		m_enemyParent = GameObject.Find ("Enemies");
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	/// <summary>
	/// This is where we need to stop everything while we switch levels
	/// </summary>
	public void PauseWorld() {
		player.GetComponent<Rigidbody2D>().isKinematic = true;
		foreach(Rigidbody2D enemy in m_enemyParent.GetComponentsInChildren<Rigidbody2D>()) {
			enemy.isKinematic = true;
			enemy.gameObject.GetComponent<Enemy>().moveSpeed = 0;
		}
	}

	public void UnPauseWorld() {
		player.GetComponent<Rigidbody2D>().isKinematic = false;
		foreach(Rigidbody2D enemy in m_enemyParent.GetComponentsInChildren<Rigidbody2D>()) {
			enemy.isKinematic = false;
			enemy.gameObject.GetComponent<Enemy>().moveSpeed = 6;
		}
	}
}
