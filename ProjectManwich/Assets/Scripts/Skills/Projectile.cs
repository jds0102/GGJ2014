using UnityEngine;
using System.Collections;

public class Projectile : MonoBehaviour 
{
	public GameObject explosion;		// Prefab of explosion effect.
    public float m_speed;
    private Animator m_animator;
    private bool m_exploded;

    public enum Direction
    {
        Right,
        Left
    }

    public Direction FireDirection {
        get; set;
    }

    public Player Owner
    {
        get;
        set;
    }


	public virtual void Start () 
	{
		// Destroy the rocket after 2 seconds if it doesn't get destroyed before then.
        Animator m_animator = GetComponent<Animator>();
		Destroy(gameObject, 5);
	}

    public virtual void Update()
    {
        if (m_animator == null) {
            m_animator = GetComponent<Animator>();
        } else {
            if (m_animator.GetCurrentAnimatorStateInfo(0).IsName("Explode")) {
                if ((m_animator.GetCurrentAnimatorStateInfo(0).normalizedTime % 1) > 0.8f) {
                    Destroy(gameObject);
                }
                this.rigidbody2D.velocity = new Vector2(0, 0);
                return;
            }
        }

        float speed = m_speed;
        if (FireDirection == Direction.Left) {
            speed = -speed;
        }

        if (this != null) {
            this.rigidbody2D.velocity = new Vector2(speed, 0);
        }

        //this.gameObject.transform.position.Set(this.gameObject.transform.position.x + 5, this.gameObject.transform.position.y, this.gameObject.transform.position.z);
    }

	public void OnExplode()
	{
        // Create a quaternion with a random rotation in the z-axis.
        if (m_exploded) return;
        Quaternion randomRotation = Quaternion.Euler(0f, 0f, Random.Range(0f, 360f));

        
        m_animator.SetBool("Exploding", true);
        m_exploded = true;
	}

    public virtual void OnTriggerEnter2D(Collider2D col)
    {
        if (Owner.m_character == null) {
            OnExplode();
            Destroy(gameObject);
            return;
        }

        if (!m_exploded) {
            if (col != Owner.m_character.collider2D) {
                foreach (Player p in PlayerManager.GetPlayers()) {
                    if (p != null && p.m_character != null && col.gameObject == p.m_character.gameObject) {
                        Debug.Log("Hit: " + col.gameObject);
                        Character hitChar = col.gameObject.GetComponent<Character>();
                        Debug.Log("Hit Player: " + hitChar);
                        if (hitChar != null) {
                            if (hitChar.m_Player.TakeDamage(1)) {
                                Debug.Log(Owner + " killed " + hitChar + "!");
                                if (hitChar.m_Marked > 0) {
                                    Owner.m_money += 100 * hitChar.m_Marked;
                                }
                            }
                        }
                        //TODO: HANDLE DAMAGE
                        OnExplode();
                        //Destroy(gameObject);
                    }
                }
                if (col.gameObject.layer == LayerMask.NameToLayer("Platform")) {
                    Debug.Log("Hit: " + col.gameObject);
                    OnExplode();
                    //Destroy(gameObject);
                }
            }
        }

    }
}
