using UnityEngine;
using System.Collections;

public class PickupMoney : InteractableItem {

	public int moneyValue;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	override public void Activate(Player player) {
		m_activated = true;
		GetComponent<SpriteRenderer> ().sprite = activatedImage;
		player.m_money += moneyValue;
	}

	override public void Reset() {
		m_activated = false;
		GetComponent<SpriteRenderer> ().sprite = originalImage;
	}
}
