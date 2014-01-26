using UnityEngine;
using System.Collections;

public class Character : MonoBehaviour
{
	private bool m_Jump = false;

	private bool m_FallThroughPlatform = false;
	private float m_FallStartTime;

	public bool b;

	[HideInInspector]
	public bool FaceRight;

	public float MoveForce = 365f;
	public float MaxSpeed = 5f;
	public float JumpForce = 1000f;
	
	private Transform m_GroundCheck;			
	private bool m_Grounded = false;

    public Skill[] m_skills;
    private Skill[] m_instancedSkills;

    private Animator m_anim;

	public Player m_Player { get; set;}
	
	void Awake()
	{
		m_GroundCheck = transform.Find("groundCheck");
	}

    void Start()
    {
        m_instancedSkills = new Skill[m_skills.Length];
        for (int i = 0; i < m_skills.Length; i++) {
            Skill s = m_skills[i];
            
            Skill instanced = (Skill)(GameObject.Instantiate(s));
            instanced.gameObject.transform.parent = this.gameObject.transform;
            instanced.m_myCharacter = this;
            s.m_myCharacter = this;
            m_instancedSkills[i] = instanced;
            //Debug.Log(instanced.m_myCharacter);
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
	

		Collider2D[] collisions = Physics2D.OverlapCircleAll(transform.position,1.7f);
		bool turnOffCollision = false;
		foreach(Collider2D collision in collisions){
			if(Physics2D.GetIgnoreLayerCollision(this.gameObject.layer,LayerMask.NameToLayer("Platform"))){
				if(collision.gameObject.layer == LayerMask.NameToLayer("Platform")){
					turnOffCollision = true;
				}
			}
		}

		if(rigidbody2D.velocity.y > 0.0f){
			turnOffCollision = true;
		} else if(rigidbody2D.velocity.y < 0.0f){
			turnOffCollision = turnOffCollision || false;
		}

		if(m_FallThroughPlatform && Time.time - m_FallStartTime < 0.2f){
			turnOffCollision = true;
			this.collider2D.isTrigger = false;
		} else {
			m_FallThroughPlatform = turnOffCollision || false;
		}

		Physics2D.IgnoreLayerCollision(this.gameObject.layer,LayerMask.NameToLayer("Platform"),turnOffCollision);

		if(m_Jump)
		{			
			rigidbody2D.AddForce(Vector2.up * JumpForce);
			
			m_Jump = false;
		}
	}

	void OnDrawGizmos()
	{
		Gizmos.DrawWireSphere(transform.position,1.7f);
	}

    public void FireSkill(int slot)
    {
        Skill skillToFire = m_instancedSkills[slot];
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

		h *= Time.deltaTime;
		if(h * rigidbody2D.velocity.x < MaxSpeed){
			rigidbody2D.AddForce(Vector2.right * h * MoveForce);
		}
		
		if(Mathf.Abs(rigidbody2D.velocity.x) > MaxSpeed){
			rigidbody2D.velocity = new Vector2(Mathf.Sign(rigidbody2D.velocity.x) * MaxSpeed, rigidbody2D.velocity.y);
		}
		rigidbody2D.velocity = new Vector2(rigidbody2D.velocity.x*(1.0f-10.0f*Time.deltaTime),rigidbody2D.velocity.y);

		if(h < -0.0f){
			FaceRight = false;
		}
		if(h > 0.0f){
			FaceRight = true;
		}
	}

	public void Drop()
	{
        m_anim.SetTrigger("Jump");
		if(m_Grounded){
			m_FallStartTime = Time.time;
			m_FallThroughPlatform = true;
			this.collider2D.isTrigger = true;
		}
	}
}
