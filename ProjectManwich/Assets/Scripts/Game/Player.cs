using UnityEngine;
using System.Collections;

public class Player
{
    public int m_playerIndex;
    public int m_playerInputLayer;
	public Character m_character;
	
    public int m_health;
    public int m_money;

    public Player(int index, int playerInputLayer)
    {
        m_playerIndex = index;
        m_playerInputLayer = playerInputLayer;
        m_money = 500;
        m_health = 3;
    }

    public void Update()
    {
		GetPlayerInput(m_playerInputLayer);

		CheckPlayerPrefab();
    }

	void GetPlayerInput(int num)
	{
		string player = "Player" + num.ToString();
		
		if(Input.GetButtonDown(player + "Jump")){
			m_character.Jump();
		}

		m_character.Move(Input.GetAxis(player + "Horizontal"));
		if(Input.GetAxis(player + "Vertical") < 0.0f){
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
				GameObject newCharacter = GameObject.Instantiate(PlayerManager.m_singleton.m_hoboPrefab,m_character.transform.position,m_character.transform.rotation) as GameObject;
				newCharacter.layer = m_character.gameObject.layer;
				GameObject.Destroy(m_character.gameObject);
				m_character = newCharacter.GetComponent<Character>();
			}
		} else if(m_money < 5000){
			if(m_character.Type != "FryCook"){
				GameObject newCharacter = GameObject.Instantiate(PlayerManager.m_singleton.m_fryCookPrefab,m_character.transform.position,m_character.transform.rotation) as GameObject;
				newCharacter.layer = m_character.gameObject.layer;
				GameObject.Destroy(m_character.gameObject);
				m_character = newCharacter.GetComponent<Character>();
			}
		} else if(m_money < 6000){
			if(m_character.Type != "BlueCollar"){
				GameObject newCharacter = GameObject.Instantiate(PlayerManager.m_singleton.m_constructionPrefab,m_character.transform.position,m_character.transform.rotation) as GameObject;
				newCharacter.layer = m_character.gameObject.layer;
				GameObject.Destroy(m_character.gameObject);
				m_character = newCharacter.GetComponent<Character>();
			}
		} else if(m_money < 8000){
			if(m_character.Type != "WhiteCollar"){
				GameObject newCharacter = GameObject.Instantiate(PlayerManager.m_singleton.m_officePrefab,m_character.transform.position,m_character.transform.rotation) as GameObject;
				newCharacter.layer = m_character.gameObject.layer;
				GameObject.Destroy(m_character.gameObject);
				m_character = newCharacter.GetComponent<Character>();
			}
		} else if(m_money < 10000){
			if(m_character.Type != "CEO"){
				GameObject newCharacter = GameObject.Instantiate(PlayerManager.m_singleton.m_ceoPrefab,m_character.transform.position,m_character.transform.rotation) as GameObject;
				newCharacter.layer = m_character.gameObject.layer;
				GameObject.Destroy(m_character.gameObject);
				m_character = newCharacter.GetComponent<Character>();
			}
		}
	}

    public void TakeDamage(int amount)
    {
        Debug.Log(m_health);
        m_health -= amount;
        if (m_health <= 0) {
            m_health = 0;
            Death();
        }
    }

    public void Death()
    {
        // NYI
    }
}
