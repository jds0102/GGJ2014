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
		if(m_money < 2000){

		} else if(m_money < 4000){

		} else if(m_money < 6000){

		} else if(m_money < 8000){

		} else if(m_money < 10000){

		}
	}
}
