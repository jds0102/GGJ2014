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

	public GameObject m_moneyDrop;

	public GameObject tempTest;
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
                    p.SpawnPlayerPrefab();
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

	public void DropMoney(Vector3 location, int amount) {
		GameObject moneyDrop3 = (GameObject)Instantiate(m_moneyDrop);
		moneyDrop3.transform.position = location;
		moneyDrop3.GetComponent<MoneyDrop> ().Initiate (amount/3, 5);
		moneyDrop3.rigidbody2D.AddForce (Vector2.up * 1000.0f);

		location.x -= .5f;
		GameObject moneyDrop = (GameObject)Instantiate(m_moneyDrop);
		moneyDrop.transform.position = location;
		moneyDrop.GetComponent<MoneyDrop> ().Initiate (amount/3, 5);
		moneyDrop.rigidbody2D.AddForce (new Vector2(-1,1) * 400.0f);

		location.x += 1;
		GameObject moneyDrop2 = (GameObject)Instantiate(m_moneyDrop);
		moneyDrop2.transform.position = location;
		moneyDrop2.GetComponent<MoneyDrop> ().Initiate (amount/3, 5);
		moneyDrop2.rigidbody2D.AddForce (new Vector2(1,1) * 400.0f);


	}
}
