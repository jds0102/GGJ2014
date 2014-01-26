using UnityEngine;
using System.Collections;

public class MoneyDrop : InteractableItem {
	
	private int moneyAmount;
	private float m_startTime = 0;
	private float m_duration = 0;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if(Time.time - m_startTime > m_duration && m_startTime != 0) {
			Destroy(this.gameObject);
		}
	}

	override public void Activate(Player player) {
		player.m_money += moneyAmount;
		Destroy (gameObject);
	}

	public void Initiate(int value, float duration) {
		moneyAmount = value;
		m_duration = duration;
		m_startTime = Time.time;
		destroyAfterInteraction = true;
	}
}
