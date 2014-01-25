using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{
	[HideInInspector]
	public bool FacingRight = true;
	private bool m_Jump = false;


	public float MoveForce = 365f;
	public float MaxSpeed = 5f;
	public AudioClip[] jumpClips;
	public float JumpForce = 1000f;	

	public CircleCollider2D GroundCollider;

	private Transform m_GroundCheck;			
	private bool m_Grounded = false;


	void Awake()
	{
		m_GroundCheck = transform.Find("groundCheck");
	}


	void Update()
	{
		m_Grounded = Physics2D.Linecast(transform.position, m_GroundCheck.position, 1 << LayerMask.NameToLayer("Ground") | 1 << LayerMask.NameToLayer("Platform")); 

		if(m_Grounded){
			
		}

		if(Input.GetButtonDown("Jump") && m_Grounded){
			m_Jump = true;
		}
	}


	void FixedUpdate ()
	{
		float h = Input.GetAxis("Horizontal");

		if(h * rigidbody2D.velocity.x < MaxSpeed){
			rigidbody2D.AddForce(Vector2.right * h * MoveForce);
		}

		if(Mathf.Abs(rigidbody2D.velocity.x) > MaxSpeed){
			rigidbody2D.velocity = new Vector2(Mathf.Sign(rigidbody2D.velocity.x) * MaxSpeed, rigidbody2D.velocity.y);
		}

		if(h > 0 && !FacingRight){
			Flip();
		} else if(h < 0 && FacingRight){
			Flip();
		}



		if(m_Jump)
		{
			int i = Random.Range(0, jumpClips.Length);
			AudioSource.PlayClipAtPoint(jumpClips[i], transform.position);

			rigidbody2D.AddForce(Vector2.up * JumpForce);

			m_Jump = false;
		}
	}
	
	void UseAbility()
	{

	}

	void Flip ()
	{
		FacingRight = !FacingRight;

		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}
}
