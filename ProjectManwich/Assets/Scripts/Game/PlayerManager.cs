using UnityEngine;
using System.Collections;

public class PlayerManager : MonoBehaviour
{
    public Player[] m_players = new Player[4];
    public int playerCount;
    public bool levelLoaded = false;

    public GameObject m_hoboPrefab;
    public GameObject m_fryCookPrefab;
    public GameObject m_constructionPrefab;
    public GameObject m_officePrefab;
    public GameObject m_ceoPrefab;

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
        if (!levelLoaded) {
            return;
        }

        for (int index = 0; index < m_players.Length; index++) {
            Player player = m_players[index];
            if (player != null) {
                player.Update();
            }
        }
	}

    void OnLevelWasLoaded(int levelID)
    {
        if(Application.loadedLevelName == "Level") {
        
            // Setup Characters
            for (int index = 0; index < m_singleton.m_players.Length; index++) {
                Player p = m_singleton.m_players[index];
                if (p != null) {
                    GameObject newPlayer = (GameObject)Instantiate(m_hoboPrefab, Vector3.zero, Quaternion.identity);
                    newPlayer.name = "Player " + index;
                    m_players[index].m_character = newPlayer.GetComponent<Character>();

                    //Temp, eventually I will create spawners and attach them to player manager 
                    m_players[index].m_character.transform.position = GameObject.Find("Player1Spawner").transform.position;
                }
            }

            levelLoaded = true;

        }
    }

    public static void AddPlayer(int playerInputLayer)
    {
        Player newPlayer = new Player(m_singleton.playerCount, playerInputLayer);
		m_singleton.m_players[newPlayer.m_playerIndex] = newPlayer;
        m_singleton.playerCount++;
    }

    public static Player GetPlayer(int index)
    {
        Player player = m_singleton.m_players[index];
        return player;
    }

    public static Player GetPlayerByInputLayer(int inputLayer)
    {
        for (int index = 0; index < m_singleton.m_players.Length; index++) {
            Player p = m_singleton.m_players[index];
            if (p != null) {
                if (p.m_playerInputLayer == inputLayer) {
                    return p;
                }
            }
        }

        return null;
    }

    public static Player[] GetPlayers()
    {        
        return m_singleton.m_players;
    }

}
