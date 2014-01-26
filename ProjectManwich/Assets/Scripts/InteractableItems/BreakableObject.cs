using UnityEngine;
using System.Collections;

public class BreakableObject : InteractableItem {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	//This should be called by the players melee atack
	override public void Activate(Player player){
		m_activated = true;
		GetComponent<SpriteRenderer> ().sprite = activatedImage;
	}
}
