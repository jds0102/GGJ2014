using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

	public static GameManager m_singleton;

	private GameObject m_enemyParent;//This can be taken out once we remove the old enemies

    void Awake()
    {
        DontDestroyOnLoad(transform.gameObject);
    }

	// Use this for initialization
	void Start () {
		m_singleton = this;
		m_enemyParent = GameObject.Find("Enemies");
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	/// <summary>
	/// This is where we need to stop everything while we switch levels
	/// </summary>
	public void PauseWorld() {
		foreach(Player player in PlayerManager.m_singleton.m_players) {
			if (player != null) {
				player.m_character.gameObject.GetComponent<Rigidbody2D>().isKinematic = true;
			}
		}
		if (m_enemyParent == null) {
			m_enemyParent = GameObject.Find("Enemies");
		}
		foreach(Rigidbody2D enemy in m_enemyParent.GetComponentsInChildren<Rigidbody2D>()) {
			enemy.isKinematic = true;
			enemy.gameObject.GetComponent<Enemy>().moveSpeed = 0;
		}
	}

	public void UnPauseWorld() {
		foreach(Player player in PlayerManager.m_singleton.m_players) {
			if (player != null) {
				player.m_character.gameObject.GetComponent<Rigidbody2D>().isKinematic = false;
			}
		}
		foreach(Rigidbody2D enemy in m_enemyParent.GetComponentsInChildren<Rigidbody2D>()) {
			enemy.isKinematic = false;
			enemy.gameObject.GetComponent<Enemy>().moveSpeed = 6;
		}
	}
}
