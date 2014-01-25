using UnityEngine;
using System.Collections;

public class Character : MonoBehaviour
{
	private bool m_Jump = false;

	[HideInInspector]
	public bool FaceRight;

	public float MoveForce = 365f;
	public float MaxSpeed = 5f;
	public float JumpForce = 1000f;	
	
	public Collider2D GroundCollider;
	public Collider2D m_Collider;
	
	private Transform m_GroundCheck;			
	private bool m_Grounded = false;

    public Skill[] m_skills;

    private Animator m_anim;

	public Player m_Player { get; set;}
	
	void Awake()
	{
		m_GroundCheck = transform.Find("groundCheck");
        foreach (Skill s in m_skills) {
            s.m_myCharacter = this;
        }
        m_anim = GetComponent<Animator>();
	}
	
	
	void Update()
	{
		if(FaceRight){
			transform.rotation = Quaternion.Euler(0.0f,180.0f,0.0f);
		} else {
			transform.rotation = Quaternion.Euler(0.0f,0.0f,0.0f);
		}
		m_Grounded = Physics2D.Linecast(transform.position, m_GroundCheck.position, 1 << LayerMask.NameToLayer("Ground") | 1 << LayerMask.NameToLayer("Platform")); 
	
		Collider2D[] collisions = Physics2D.OverlapCircleAll(transform.position,1.5f);
		foreach(Collider2D collision in collisions){
			if(collision.gameObject.layer == LayerMask.NameToLayer("BelowPlatform")){
				Physics2D.IgnoreLayerCollision(this.gameObject.layer,LayerMask.NameToLayer("Platform"),true);
			} else if(collision.gameObject.layer == LayerMask.NameToLayer("AbovePlatform")){
				Physics2D.IgnoreLayerCollision(this.gameObject.layer,LayerMask.NameToLayer("Platform"),false);
			}
		}
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
        Skill skillToFire = m_skills[slot];
        skillToFire.Execute();

        if (slot == 1) {
            m_anim.SetTrigger("Action1");
        } else if (slot == 2) {
            m_anim.SetTrigger("Action2");
        } else if (slot == 3) {
            m_anim.SetTrigger("Special");
        }
    }

    /*
	void UseAbility(int slot)
	{
        //Debug.Log("Use Ability Called - Slot [" + slot + "]");
        FireSkill(slot);
	}
    */

	public void Jump()
	{
		if(m_Grounded){
            m_anim.SetTrigger("Jump");
			m_Jump = true;
		}
	}

	public void Move(float h)
	{
        m_anim.SetFloat("Speed", Mathf.Abs(h));

		h *= 20.0f*Time.deltaTime;
		if(h * rigidbody2D.velocity.x < MaxSpeed){
			rigidbody2D.AddForce(Vector2.right * h * MoveForce);
		}
		
		if(Mathf.Abs(rigidbody2D.velocity.x) > MaxSpeed){
			rigidbody2D.velocity = new Vector2(Mathf.Sign(rigidbody2D.velocity.x) * MaxSpeed, rigidbody2D.velocity.y);
		}

		if(h < -0.01f){
			FaceRight = false;
		}
		if(h > 0.01f){
			FaceRight = true;
		}
	}

	public void Drop()
	{
        m_anim.SetTrigger("Jump");
		Physics2D.IgnoreLayerCollision(this.gameObject.layer,LayerMask.NameToLayer("Platform"),true);
	}

}
