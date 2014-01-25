using UnityEngine;
using System.Collections;

public class PlayerManager : MonoBehaviour
{
    public Player[] m_players = new Player[4];
    public int playerCount;

	public GameObject PlayerPrefab;

    public static PlayerManager m_singleton;

    // Use this for initialization
	void Start ()
    {
        if (m_singleton == null) {
            m_singleton = this;
        }
	}
	
	// Update is called once per frame
	void Update () {
        for (int index = 0; index < m_players.Length; index++) {
            Player player = m_players[index];
            if (player != null) {
                player.Update();
            }
        }
	}

    void OnLevelWasLoaded(int levelID)
    {
        if (levelID == 2) {
            // Setup Characters
			AddPlayer();
			GameObject newPlayer = (GameObject)Instantiate(PlayerPrefab,Vector3.zero,Quaternion.identity);
			m_players[0].m_character = newPlayer.GetComponent<Character>();

        }
    }

    public static void AddPlayer()
    {
        Player newPlayer = new Player(m_singleton.playerCount);
        m_singleton.playerCount++;
    }

    public static Player GetPlayer(int index)
    {
        Player player = m_singleton.m_players[index];
        return player;
    }

    public static Player[] GetPlayers()
    {        
        return m_singleton.m_players;
    }

}
