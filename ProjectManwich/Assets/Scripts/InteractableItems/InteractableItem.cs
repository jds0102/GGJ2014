using UnityEngine;
using System.Collections;

public class InteractableItem : MonoBehaviour {

	public InteractionType interaction;
	public Sprite originalImage, activatedImage;

	private BoxCollider2D collisionBox;
	public bool m_activated;

	// Use this for initialization
	void Start () {
		collisionBox = gameObject.GetComponent<BoxCollider2D> ();
		if (collisionBox == null) {
			Debug.LogError("Interactable object " + gameObject.name + " has no box collider attached. Disabling the object");
			this.enabled = false;
		}
		originalImage = GetComponent<SpriteRenderer> ().sprite;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter2D(Collider2D other) {
		//Only activate it once for now
		if (m_activated) return;
		if(interaction == InteractionType.onCollide) {
			Character character = other.gameObject.GetComponent<Character>();
			if (character) {
				Player player = character.m_Player;
				if (player != null) {
					Activate(player);
				}
			}
		}
	}

	public virtual void Activate(Player player) {}

	public virtual void Reset() {}
}

public enum InteractionType{
	onCollide,
	onHit
}