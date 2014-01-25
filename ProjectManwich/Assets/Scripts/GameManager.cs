using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

	public static GameManager m_singleton;

    void Awake()
    {
        DontDestroyOnLoad(transform.gameObject);
    }

	// Use this for initialization
	void Start () {
		m_singleton = this;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	/// <summary>
	/// This is where we need to stop everything while we switch levels
	/// </summary>
	public void PauseWorld() {
		foreach(Player player in PlayerManager.m_singleton.m_players) {
			if (player != null && player.m_character != null) {
				player.m_character.gameObject.GetComponent<Rigidbody2D>().isKinematic = true;
			}
		}
	}

	public void UnPauseWorld() {
		foreach(Player player in PlayerManager.m_singleton.m_players) {
			if (player != null && player.m_character != null) {
				player.m_character.gameObject.GetComponent<Rigidbody2D>().isKinematic = false;
			}
		}
	}
}
