using UnityEngine;
using System.Collections;

public class Character : MonoBehaviour
{
	private bool m_Jump = false;

	[HideInInspector]
	public bool FaceRight = true;

	public float MoveForce = 365f;
	public float MaxSpeed = 5f;
	public float JumpForce = 1000f;	
	
	public CircleCollider2D GroundCollider;
	
	private Transform m_GroundCheck;			
	private bool m_Grounded = false;

	private Collider2D m_Collider;
    public Skill[] m_skills = new Skill[3];
	
	
	void Awake()
	{
		m_Collider = this.GetComponent<Collider2D>();
		m_GroundCheck = transform.Find("groundCheck");
	}
	
	
	void Update()
	{
		m_Grounded = Physics2D.Linecast(transform.position, m_GroundCheck.position, 1 << LayerMask.NameToLayer("Ground") | 1 << LayerMask.NameToLayer("Platform")); 
	}
	
	
	void FixedUpdate ()
	{
		if(m_Jump)
		{			
			rigidbody2D.AddForce(Vector2.up * JumpForce);
			
			m_Jump = false;
		}
	}

    public void FireSkill(int slot)
    {
        //Skill skillToFire = m_skills[slot];
        //skillToFire.Execute();
    }

	void UseAbility(int slot)
	{
        Debug.Log("Use Ability Called - Slot [" + slot + "]");
        FireSkill(slot);
	}

	void OnTriggerEnter2D(Collider2D collider)
	{
		if(collider.gameObject.layer == LayerMask.NameToLayer("BelowPlatform")){
			Physics2D.IgnoreLayerCollision(this.gameObject.layer,LayerMask.NameToLayer("Platform"),true);
		}
		if(collider.gameObject.layer == LayerMask.NameToLayer("AbovePlatform")){
			Physics2D.IgnoreLayerCollision(this.gameObject.layer,LayerMask.NameToLayer("Platform"),false);
		}
	}

	public void Jump()
	{
		if(m_Grounded){
			m_Jump = true;
		}
	}

	public void Move(float h)
	{
		h *= 10.0f*Time.deltaTime;
		if(h * rigidbody2D.velocity.x < MaxSpeed){
			rigidbody2D.AddForce(Vector2.right * h * MoveForce);
		}
		
		if(Mathf.Abs(rigidbody2D.velocity.x) > MaxSpeed){
			rigidbody2D.velocity = new Vector2(Mathf.Sign(rigidbody2D.velocity.x) * MaxSpeed, rigidbody2D.velocity.y);
		}
		if(h < 0.0f){
			FaceRight = false;
			transform.rotation = Quaternion.Euler(0.0f,0.0f,0.0f);
		}
		if(h > 0.0f){
			FaceRight = true;
			transform.rotation = Quaternion.Euler(0.0f,180.0f,0.0f);
		}
	}

	public void Drop()
	{
		Physics2D.IgnoreLayerCollision(this.gameObject.layer,LayerMask.NameToLayer("Platform"),true);
	}

}
