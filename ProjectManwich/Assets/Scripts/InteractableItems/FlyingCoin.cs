using UnityEngine;
using System.Collections;

public class FlyingCoin : InteractableItem {

	private Player coinTarget;
	private int moneyAmount;
	private bool initiated;
	private Vector2 pos, targetPos;
	// Use this for initialization
	void Start () {
		pos = new Vector2 ();
		targetPos = new Vector2 ();
	}
	
	// Update is called once per frame
	void Update () {
		if(initiated && coinTarget != null) {
			pos.x = transform.position.x;
			pos.y = transform.position.y;
			targetPos.x = coinTarget.m_character.transform.position.x;
			targetPos.y = coinTarget.m_character.transform.position.y;
			rigidbody2D.AddForce((targetPos - pos).normalized*100);
		}
	}

	override public void Activate(Player player) {
		player.m_money += moneyAmount;
		Destroy (gameObject);
	}
	
	public void Initiate(int value, Vector3 startPosition, Player target) {
//		transform.position = startPosition;
//		pos.x = transform.position.x;
//		pos.y = transform.position.y;
//		targetPos.x = target.m_character.transform.position.x;
//		targetPos.y = target.m_character.transform.position.y;
//		Vector2 vec = (pos - targetPos).normalized * 1.0f;
//		Vector3 vec3 = new Vector3 (vec.x, vec.y, transform.position.z);
//		transform.position = transform.position + vec3;
		transform.position = Vector3.zero;
		coinTarget = target;
		moneyAmount = value;
		destroyAfterInteraction = true;
		initiated = true;
	}
}
