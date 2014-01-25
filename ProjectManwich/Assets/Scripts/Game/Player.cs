using UnityEngine;
using System.Collections;

public class Player : ScriptableObject
{
    public int m_playerIndex;
    public PlayerClass m_class;
	public Character m_character;
	
    public int m_health;
    public int m_money;

    public Player(int index)
    {
        m_playerIndex = index;
        m_class = new Hobo();
        m_money = 500;
        m_health = 3;
    }

    public void Update()
    {
		CheckForPlayerInput();
    }

	void CheckForPlayerInput()
	{
		if(m_character.gameObject.layer == LayerMask.NameToLayer("Player1")){
			GetPlayerInput(1);
		}
		if(m_character.gameObject.layer == LayerMask.NameToLayer("Player2")){
			GetPlayerInput(2);
		}
		if(m_character.gameObject.layer == LayerMask.NameToLayer("Player3")){
			GetPlayerInput(3);
		}
		if(m_character.gameObject.layer == LayerMask.NameToLayer("Player4")){
			GetPlayerInput(4);
		}
	}

	void GetPlayerInput(int num)
	{
		string player = "Player" + num.ToString();
		
		if(Input.GetButton(player + "Jump")){
			m_character.Jump();
		}

		m_character.Move(Input.GetAxis(player + "Horizontal"));
	}

}
