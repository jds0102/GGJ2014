using UnityEngine;
using System.Collections;

public class Intern : MonoBehaviour {

	public float duration;
	private Character m_character;
	private Animator m_anim;
	public Player Owner { get; set; }
	private float m_creationTime;
	private Player m_enemyToAttack;
	private Vector2 m_enemyDirectionFromPlayer;
	private float range = 20;
	private bool m_attack = false;
	private float m_lastAttack = 0;
	private float m_attackRange = 3.0f;

	private Vector3 m_destination;
	private bool m_attackReady = true;

	private float m_lastAction = 0;
	private bool m_actionReady = true; //This makes it so that the AI can't perform a crazy number of actions per frame

	private const float ATTACK_COOLDOWN = 1;
	private const float ACTION_COOLDOWN = .33f;
	// Use this for initialization
	void Start () {
		m_character = gameObject.GetComponent<Character> ();
		m_creationTime = Time.time;
	}
	
	// Update is called once per frame
	void Update () {
		
		if (Time.time - m_creationTime > duration || Owner.m_character == null) {
			Destroy(this.gameObject);
		}

		if (Time.time - m_lastAttack > ATTACK_COOLDOWN && !m_attackReady) {
			m_attackReady = true;
		}

		if (Time.time - m_lastAction > ACTION_COOLDOWN && !m_actionReady) {
			m_actionReady = true;
		}

		//Debug.Log (m_character.IsGrounded ());
		m_enemyToAttack = PlayerManager.m_singleton.FindClosestEnemy (Owner);
		if (m_enemyToAttack != null) {
			m_enemyDirectionFromPlayer = (m_enemyToAttack.m_character.transform.position - Owner.m_character.transform.position);
			if (m_enemyDirectionFromPlayer.magnitude <= range) {
				m_attack = true;
			} else {
				m_attack = false;
			}
		} else {
			m_attack = false;
		}



		if (m_destination != null) {
			Vector3 direction = m_destination - this.transform.position;
			if (direction.x > 1 || direction.x < -1) {
				m_character.Move (Mathf.Sign(direction.x));
			}
			if (direction.y > 1 && m_actionReady) {
				m_character.Jump();
				m_actionReady = false;
				m_lastAction = Time.time;
			} else if (direction.y < -1) {
				m_character.Drop();
			}
		}

		if (m_attack && m_enemyToAttack != null) {
			m_destination = m_enemyToAttack.m_character.transform.position;

			if ((m_enemyToAttack.m_character.transform.position - this.transform.position).magnitude < 2 && m_attackReady && m_actionReady) {
				MeleeAttack();
			}

		} else {
			m_destination = Owner.m_character.transform.position;
		}
	}

	void MeleeAttack()
	{
		Vector3 charPos = m_character.gameObject.transform.position;
		RaycastHit2D[] playerHits;
		if (m_character.FaceRight) {
			Vector3 endPos = charPos;
			endPos.x += m_attackRange;
			playerHits = Physics2D.LinecastAll(charPos, endPos, 1 << LayerMask.NameToLayer("Player1") | 1 << LayerMask.NameToLayer("Player2") | 1 << LayerMask.NameToLayer("Player3") | 1 << LayerMask.NameToLayer("Player4"));
		} else {
			Vector3 endPos = charPos;
			endPos.x -= m_attackRange;
			playerHits = Physics2D.LinecastAll(charPos, endPos, 1 << LayerMask.NameToLayer("Player1") | 1 << LayerMask.NameToLayer("Player2") | 1 << LayerMask.NameToLayer("Player3") | 1 << LayerMask.NameToLayer("Player4"));
		}

		foreach (RaycastHit2D hit in playerHits) {
			if (hit != null) {
				Debug.Log("Hit: " + hit.transform.gameObject);
				if (hit.transform.gameObject != m_character.gameObject && hit.transform.gameObject != Owner.m_character.gameObject) {
					// TODO: HANDLE HIT STUFF HERE
					Character hitChar = hit.transform.gameObject.GetComponent<Character>();
					if (hitChar != null) {
						if (hitChar.m_Player.TakeDamage(1)) {
							Debug.Log(Owner.m_character.gameObject + " killed " + hitChar + "!");
							if (hitChar.m_Marked > 0) {
								Owner.m_character.m_Player.m_money += 100 * hitChar.m_Marked;
							}
						}
					}
				}
			}
		}

		m_attackReady = false;
		m_lastAttack = Time.time;
		m_actionReady = false;
		m_lastAction = Time.time;
	}
}
