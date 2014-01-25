﻿using UnityEngine;
using System.Collections;

public class InteractableItem : MonoBehaviour {

	public InteractionType interaction;
	public Sprite activatedImage;

	private BoxCollider2D collisionBox;

	// Use this for initialization
	void Start () {
		collisionBox = gameObject.GetComponent<BoxCollider2D> ();
		if (collisionBox == null) {
			Debug.LogError("Interactable object " + gameObject.name + " has no box collider attached. Disabling the object");
			this.enabled = false;
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter2D(Collider2D other) {
		
		Debug.Log("Activated Interactable Object");
		if(interaction == InteractionType.onCollide) {
			if (other.gameObject.GetComponent<Character>()) {
				Activate();
			}
		}
	}

	void Activate() {
		GetComponent<SpriteRenderer> ().sprite = activatedImage;

	}
}

public enum InteractionType{
	onCollide,
	onHit
}