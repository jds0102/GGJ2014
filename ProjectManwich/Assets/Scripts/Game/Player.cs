using UnityEngine;
using System.Collections;

public class Player
{
    public int m_playerIndex;
    public int m_playerInputLayer;
	public Character m_character;
	
    public int m_health;
    public int m_money;

    public bool m_dead;
    public float m_deathTime;

    private const float k_respawnTime = 1.0f;

    public Player(int index, int playerInputLayer)
    {
        m_playerIndex = index;
        m_playerInputLayer = playerInputLayer;
        m_money = 500;
        m_health = 3;
    }

    public void Update()
    {
        if (!m_dead) {
            CheckPlayerPrefab();
            GetPlayerInput(m_playerInputLayer);
        } else {
            if (Time.time - m_deathTime > k_respawnTime) {
                m_dead = false;
                m_deathTime = 0.0f;
                SpawnPlayerPrefab();
                m_health = 3;
            }
        }
    }

	void GetPlayerInput(int num)
	{
		string player = "Player" + num.ToString();

        if (m_character == null || !m_character.m_loaded) {
            return;
        }
		
		if(Input.GetButtonDown(player + "Jump")){
			m_character.Jump();
		}
		//Debug.Log (Input.GetAxis(player + "Vertical"));
		m_character.Move(Input.GetAxis(player + "Horizontal"));
		if(Input.GetAxis(player + "Vertical") > 0.0f){
			m_character.Drop();
		}

		if(Input.GetButtonDown(player + "Skill1")){
			m_character.FireSkill(0);
		}

		if(Input.GetButtonDown(player + "Skill2")){
			m_character.FireSkill(1);
		}

		if(Input.GetButtonDown(player + "Skill3")){
			m_character.FireSkill(2);
		}
	}

	void CheckPlayerPrefab()
	{ 
		if(m_money < 4000){
			if(m_character.Type != "Hobo"){
				GameObject newCharacter = GameObject.Instantiate(PlayerManager.m_singleton.m_hoboPrefab, m_character.transform.position,m_character.transform.rotation) as GameObject;
				newCharacter.layer = m_character.gameObject.layer;
				GameObject.Destroy(m_character.gameObject);
				m_character = newCharacter.GetComponent<Character>();
			}
		} else if(m_money < 5000){
            if (m_character.Type != "FryCook") {
				GameObject newCharacter = GameObject.Instantiate(PlayerManager.m_singleton.m_fryCookPrefab,m_character.transform.position,m_character.transform.rotation) as GameObject;
				newCharacter.layer = m_character.gameObject.layer;
				GameObject.Destroy(m_character.gameObject);
				m_character = newCharacter.GetComponent<Character>();
			}
		} else if(m_money < 6000){
            if (m_character.Type != "BlueCollar") {
				GameObject newCharacter = GameObject.Instantiate(PlayerManager.m_singleton.m_constructionPrefab,m_character.transform.position,m_character.transform.rotation) as GameObject;
				newCharacter.layer = m_character.gameObject.layer;
				GameObject.Destroy(m_character.gameObject);
				m_character = newCharacter.GetComponent<Character>();
			}
		} else if(m_money < 8000){
            if (m_character.Type != "WhiteCollar") {
				GameObject newCharacter = GameObject.Instantiate(PlayerManager.m_singleton.m_officePrefab,m_character.transform.position,m_character.transform.rotation) as GameObject;
				newCharacter.layer = m_character.gameObject.layer;
				GameObject.Destroy(m_character.gameObject);
				m_character = newCharacter.GetComponent<Character>();
			}
		} else if(m_money < 10000){
            if (m_character.Type != "CEO") {
				GameObject newCharacter = GameObject.Instantiate(PlayerManager.m_singleton.m_ceoPrefab,m_character.transform.position,m_character.transform.rotation) as GameObject;
				newCharacter.layer = m_character.gameObject.layer;
				GameObject.Destroy(m_character.gameObject);
				m_character = newCharacter.GetComponent<Character>();
			}
		}
	}


    public void SpawnPlayerPrefab()
	{
        Vector3 spawnPosition = GameObject.Find("Player" + m_playerIndex + "Spawner").transform.position;

        GameObject newCharacter = null;
		if(m_money < 4000){
			newCharacter = GameObject.Instantiate(PlayerManager.m_singleton.m_hoboPrefab, spawnPosition, Quaternion.identity) as GameObject;
		} else if(m_money < 5000){
            newCharacter = GameObject.Instantiate(PlayerManager.m_singleton.m_fryCookPrefab, spawnPosition, Quaternion.identity) as GameObject;
		} else if(m_money < 6000){
            newCharacter = GameObject.Instantiate(PlayerManager.m_singleton.m_constructionPrefab, spawnPosition, Quaternion.identity) as GameObject;
		} else if(m_money < 8000){
            newCharacter = GameObject.Instantiate(PlayerManager.m_singleton.m_officePrefab, spawnPosition, Quaternion.identity) as GameObject;
		} else {
            newCharacter = GameObject.Instantiate(PlayerManager.m_singleton.m_ceoPrefab, spawnPosition, Quaternion.identity) as GameObject;
		}

        newCharacter.name = "Player " + m_playerIndex;
        newCharacter.layer = LayerMask.NameToLayer("Player" + (m_playerIndex + 1));
        m_character = newCharacter.GetComponent<Character>();
        m_character.m_Player = this;
	}

    public bool TakeDamage(int amount) //returns true if damage kills
    {
        m_character.TakeDamage(amount);
        Debug.Log(m_health);
        m_health -= amount;
        Debug.Log("Took Damage - remaining health: " + m_health);
        if (m_health <= 0) {
            m_health = 0;
            Death();
            return true;
        }
        return false;
    }

    public void Death()
    {
        m_dead = true;
        m_deathTime = Time.time;
        GameObject.Destroy(m_character.gameObject);
    }
}
