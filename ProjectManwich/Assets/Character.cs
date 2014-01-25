using UnityEngine;
using System.Collections;

public class Character : MonoBehaviour
{
	private bool m_Jump = false;
	
	public float MoveForce = 365f;
	public float MaxSpeed = 5f;
	public float JumpForce = 1000f;	
	
	public CircleCollider2D GroundCollider;
    public PlayerClass m_class;
	
	private Transform m_GroundCheck;			
	private bool m_Grounded = false;
	
	
	void Awake()
	{
		m_GroundCheck = transform.Find("groundCheck");
        m_class = new Hobo();
	}
	
	
	void Update()
	{
		m_Grounded = Physics2D.Linecast(transform.position, m_GroundCheck.position, 1 << LayerMask.NameToLayer("Ground") | 1 << LayerMask.NameToLayer("Platform")); 
		
		if(m_Grounded){
			
		}
		
		if(Input.GetButtonDown("Jump") && m_Grounded){
			m_Jump = true;
		}

        if (Input.GetKeyDown(KeyCode.Space)) {
            UseAbility(1);
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

		if(m_Jump)
		{			
			rigidbody2D.AddForce(Vector2.up * JumpForce);
			
			m_Jump = false;
		}
	}
	
	void UseAbility(int slot)
	{
        m_class.FireSkill(slot);
	}
}
