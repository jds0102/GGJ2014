using UnityEngine;
using System.Collections;

public class Player
{
    public int m_playerIndex;
    public PlayerClass m_class;

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
        
    }

}
/*
using UnityEngine;
using System;
using System.Collections;

public class PlayerClass : MonoBehaviour {
	
	public Skill[] m_skills = new Skill[3];
	
	public PlayerClass()
	{
		
	}
	
	void Update()
	{
		CheckForPlayerInput();
	}
	
	void CheckForPlayerInput()
	{
		if(gameObject.layer == LayerMask.NameToLayer("Player1")){
			GetPlayerInput(1);
		}
		if(gameObject.layer == LayerMask.NameToLayer("Player2")){
			GetPlayerInput(2);
		}
		if(gameObject.layer == LayerMask.NameToLayer("Player3")){
			GetPlayerInput(3);
		}
		if(gameObject.layer == LayerMask.NameToLayer("Player4")){
			GetPlayerInput(4);
		}
	}
	
	void GetPlayerInput(int num)
	{
		string player = "Player" + num.ToString();
		
		if(Input.GetButton(player + "Jump")){
			this.GetComponent<Character>().Jump();
		}
		
		this.GetComponent<Character>().Move(Input.GetAxis(player + "Horizontal"));
		
	}
	
	public void FireSkill(int slot)
	{
		Skill skillToFire = m_skills[slot];
		skillToFire.Execute();
	}
}
*/
