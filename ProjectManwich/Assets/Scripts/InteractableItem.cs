using UnityEngine;
using System.Collections;

public class InteractableItem : MonoBehaviour {

	public InteractionType interaction;
	public Sprite activatedImage;

	private BoxCollider2D collisionBox;
	private bool m_activated;

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
		//Only activate it once for now
		if (m_activated) return;

		if(interaction == InteractionType.onCollide) {
			Player player = other.gameObject.GetComponent<Character>().m_Player;
			if (player != null) {
				Activate(player);
			}
		}
	}

	void Activate(Player player) {
		m_activated = true;
		GetComponent<SpriteRenderer> ().sprite = activatedImage;
		player.m_money += 500;
	}
}

public enum InteractionType{
	onCollide,
	onHit
}