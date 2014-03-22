using UnityEngine;
using InControl;
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

	private InputDevice m_device;

    public Player(int index, int playerInputLayer, InputDevice inputDevice)
    {
        m_playerIndex = index;
        m_playerInputLayer = playerInputLayer;
        m_money = 500;
        m_health = 3;
		m_device = inputDevice;
    }

    public void Update()
    {
		if(Input.GetKeyDown(KeyCode.X)){
			m_money -= 500;
		}
        if (!m_dead) {
            CheckPlayerPrefab();
            GetPlayerInput();
        } else {
            if (Time.time - m_deathTime > k_respawnTime) {
                m_dead = false;
                m_deathTime = 0.0f;
                SpawnPlayerPrefab();
                m_health = 3;
            }
        }
    }

	void GetPlayerInput()
	{
        if (m_character == null || !m_character.m_loaded) {
            return;
        }
		
		if(m_device.Action1.WasPressed){
			m_character.Jump();
		}
		//Debug.Log (Input.GetAxis(player + "Vertical"));
		float deviceX, deviceY;
		deviceX = m_device.Direction.x;
		deviceY = m_device.Direction.y;
		m_character.Move(deviceX);
		if(deviceY < -.33f){
			m_character.Drop();
		}

		if(m_device.Action2.WasPressed){
			m_character.FireSkill(0);
		}

		if(m_device.Action3.WasPressed){
			m_character.FireSkill(1);
		}

		if(m_device.Action4.WasPressed){
			m_character.FireSkill(2);
		}
	}

	void CheckPlayerPrefab()
	{ 
		if(m_money < 800){
			if(m_character.Type != "Hobo"){
				ReplaceCharacter(PlayerManager.m_singleton.m_hoboPrefab,"Hobo");
			}
		} else if(m_money < 900){
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

	//For now this is called whenever a device is connected or disconnected.
	//This is because we need to reconnect to the controller we originally connected to.
	public void DeviceUpdate()
	{
		foreach(InputDevice d in InputManager.Devices) {
			if (d.Meta == m_device.Meta) {
				m_device = d;
				return;
			}
		}
	}

	public void ReturnToSpawn()
	{
		m_character.transform.position = GameObject.Find("Player" + m_playerIndex + "Spawner").transform.position;
	}
}
