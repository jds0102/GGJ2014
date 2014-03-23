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

	private Vector3 m_destination;

	// Use this for initialization
	void Start () {
		m_character = gameObject.GetComponent<Character> ();
		m_creationTime = Time.time;
	}
	
	// Update is called once per frame
	void Update () {
		//Debug.Log (m_character.IsGrounded ());
		m_enemyToAttack = PlayerManager.m_singleton.FindClosestEnemy (Owner);
		if (m_enemyToAttack != null) {
			m_enemyDirectionFromPlayer = (m_enemyToAttack.m_character.transform.position - Owner.m_character.transform.position);
			if (m_enemyDirectionFromPlayer.magnitude <= range) {
				m_attack = true;
			} else {
				m_attack = false;
			}
		}


		if (m_destination != null) {
			Vector3 direction = m_destination - this.transform.position;
			if (direction.x > 2 || direction.x < -2) {
				m_character.Move (Mathf.Sign(direction.x));
			}
			if (direction.y > 2) {
				m_character.Jump();
			} else if (direction.y < -2) {
				m_character.Drop();
			}
		}

		if (m_attack) {
			m_destination = m_enemyToAttack.m_character.transform.position;
		} else {
			m_destination = Owner.m_character.transform.position;
		}
		if (Time.time - m_creationTime > duration) {
			Destroy(this.gameObject);
		}
	}
}
