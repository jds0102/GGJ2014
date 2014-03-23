using UnityEngine;
using System.Collections;

public class Civilian : BreakableObject {

	public GameObject m_moneyDrop;
	public int m_value;
	private Vector2 temp;
	private int direction = 1;
	private int speed = 4;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (Random.Range(0,1000) < 2) {
			direction *= -1;
		}
		temp = rigidbody2D.velocity;
		temp.x = speed * direction;
		rigidbody2D.velocity = temp;
	}

	//Called by players melee attack
	override public void Activate(Player player) {
		base.Activate(player);
		GameObject money = (GameObject)Instantiate (m_moneyDrop);
		money.GetComponent<MoneyDrop> ().Initiate (m_value, 2);
		money.transform.position = transform.position;
		CivilianSpawner.m_singleton.KillCivilian (gameObject);
		Destroy (this.gameObject);
	}

	public void OnTriggerEnter2D(Collider2D other) {
		if (other.GetComponent<Character>() || other.GetComponent<Civilian>() || other.gameObject.tag == "Boundry") { 
			direction *= -1;
		}
	}

	public void Pause() {
		speed = 0;
		rigidbody2D.isKinematic = true;
	}

	public void UnPause() {
		speed = 4;
		rigidbody2D.isKinematic = false;
	}

	public void SetDirection(int dir) {
		direction = dir;
	}

}
