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
		if(Input.GetKeyDown(KeyCode.X)){
			m_money -= 500;
		}
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
		if(m_money < 800){
			if(m_character.Type != "Hobo"){
				ReplaceCharacter(PlayerManager.m_singleton.m_hoboPrefab,"Hobo");
			}
		} else if(m_money < 5000){
            if (m_character.Type != "FryCook") {
				ReplaceCharacter(PlayerManager.m_singleton.m_fryCookPrefab,"Fry Cook");
			}
		} else if(m_money < 6000){
            if (m_character.Type != "BlueCollar") {
				ReplaceCharacter(PlayerManager.m_singleton.m_constructionPrefab,"Construction Worker");
			}
		} else if(m_money < 8000){
            if (m_character.Type != "WhiteCollar") {
				ReplaceCharacter(PlayerManager.m_singleton.m_officePrefab,"Paper Pusher");
			}
		} else if(m_money < 10000){
            if (m_character.Type != "CEO") {
				ReplaceCharacter(PlayerManager.m_singleton.m_ceoPrefab,"CEO");
			}
		}
	}

	void ReplaceCharacter(GameObject prefab, string name)
	{
		GameObject newCharacter = GameObject.Instantiate(prefab,m_character.transform.position,m_character.transform.rotation) as GameObject;
		newCharacter.layer = m_character.gameObject.layer;
		newCharacter.gameObject.name = m_character.name;
		GameObject.Destroy(m_character.gameObject);
		m_character = newCharacter.GetComponent<Character>();
		newCharacter.GetComponent<Character>().m_Player = this;
		HUD.ChangePlayerJobString(m_playerIndex, name);
	}

    public void SpawnPlayerPrefab()
	{
		if(m_money < 1000){
			SpawnCharacter(PlayerManager.m_singleton.m_hoboPrefab,"Hobo");
		} else if(m_money < 2000){
			SpawnCharacter(PlayerManager.m_singleton.m_fryCookPrefab,"Fry Cook");
		} else if(m_money < 6000){
			SpawnCharacter(PlayerManager.m_singleton.m_constructionPrefab,"Construction Worker");
		} else if(m_money < 8000){
			SpawnCharacter(PlayerManager.m_singleton.m_officePrefab,"Paper Pusher");
		} else {
			SpawnCharacter(PlayerManager.m_singleton.m_ceoPrefab,"CEO");
		}
	}

	void SpawnCharacter(GameObject prefab, string name)
	{
		Vector3 spawnPosition = GameObject.Find("Player" + m_playerIndex + "Spawner").transform.position;
		GameObject newCharacter = GameObject.Instantiate(prefab, spawnPosition, Quaternion.identity) as GameObject;
		HUD.ChangePlayerJobString(m_playerIndex, name);
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
		int moneyLost = (int) (m_money * .05f);
        if (m_character.m_bribeTarget != null) {
            m_character.m_bribeTarget.m_Marked--;
        }
		PlayerManager.m_singleton.DropMoney(m_character.transform.position, moneyLost);
		m_money -= moneyLost;
        GameObject.Destroy(m_character.gameObject);
    }
}
