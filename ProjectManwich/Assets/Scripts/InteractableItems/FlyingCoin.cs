using UnityEngine;
using System.Collections;

public class FlyingCoin : InteractableItem {

	private Player coinTarget, coinOrigin;
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
			rigidbody2D.AddForce((targetPos - pos).normalized * 75);
		}

        
	}

    void FixedUpdate()
    {
        if (Vector3.Distance(this.gameObject.transform.position, coinTarget.m_character.transform.position) < 3) {
            Activate(coinTarget);
        }
    }

	override public void Activate(Player player) {
		if (player != coinOrigin) {
			player.m_money += moneyAmount;
			Destroy (gameObject);
		}
	}
	
	public void Initiate(int value, Player startPlayer, Player target) {
//		transform.position = startPosition;
//		pos.x = transform.position.x;
//		pos.y = transform.position.y;
//		targetPos.x = target.m_character.transform.position.x;
//		targetPos.y = target.m_character.transform.position.y;
//		Vector2 vec = (pos - targetPos).normalized * 1.0f;
//		Vector3 vec3 = new Vector3 (vec.x, vec.y, transform.position.z);
//		transform.position = transform.position + vec3;
		transform.position = startPlayer.m_character.transform.position;
		coinOrigin = startPlayer;
		coinTarget = target;
		moneyAmount = value;
		destroyAfterInteraction = true;
		initiated = true;
	}
}
