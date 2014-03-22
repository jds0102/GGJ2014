using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Character : MonoBehaviour
{
	private bool m_Jump = false;

	private bool m_FallThroughPlatform = false;
	
	[HideInInspector]
	public bool FaceRight;

	public string Type;

	public float MoveForce = 365f;
	public float MaxSpeed = 5f;
	public float JumpForce = 1000f;
	
    [HideInInspector]
	public Transform m_GroundCheck;			
	private bool m_Grounded = false;
    [HideInInspector]
    public bool m_Slowed = false;
    [HideInInspector]
    public int m_Marked = 0;
    [HideInInspector]
    public Character m_bribeTarget = null;

    public Skill[] m_skills;
    private Skill[] m_instancedSkills;

	public Vector2 CircleCastOffset;
	public float CircleCastRadius;

    private Animator m_anim;

	public Player m_Player { get; set;}
    public bool m_loaded;
	
	void Awake()
	{
		m_GroundCheck = transform.Find("groundCheck");
	}

    protected virtual void Start()
    {
        Physics2D.IgnoreLayerCollision(this.gameObject.layer, LayerMask.NameToLayer("MopWater"), true);
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
        m_loaded = true;
	}
	
	void Update()
	{

		if(FaceRight){
			transform.rotation = Quaternion.Euler(0.0f,180.0f,0.0f);
		} else {
			transform.rotation = Quaternion.Euler(0.0f,0.0f,0.0f);
		}
		m_Grounded = Physics2D.Linecast(transform.position, m_GroundCheck.position, 1 << LayerMask.NameToLayer("Ground") | 1 << LayerMask.NameToLayer("Platform"));

        m_Slowed = false;
        RaycastHit2D waterHit = Physics2D.Linecast(transform.position, m_GroundCheck.position, 1 << LayerMask.NameToLayer("MopWater"));
        if (waterHit != null && waterHit.transform != null && waterHit.transform.gameObject != null) {
            MopWater mopWater = waterHit.transform.gameObject.GetComponent<MopWater>();
            if (mopWater.Owner != m_Player) {
                m_Slowed = true;
            }
        }
        //Debug.Log("Slowed? " + m_Slowed);
	

		Collider2D[] collisions = Physics2D.OverlapCircleAll(new Vector2(transform.position.x+CircleCastOffset.x,transform.position.y+CircleCastOffset.y),CircleCastRadius);
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

		if(m_FallThroughPlatform){
			m_FallThroughPlatform = false;
			turnOffCollision = true;
			this.collider2D.isTrigger = false;
		}

		Physics2D.IgnoreLayerCollision(this.gameObject.layer,LayerMask.NameToLayer("Platform"),turnOffCollision);

		if(m_Jump)
		{			
			rigidbody2D.AddForce(Vector2.up * JumpForce);
			
			m_Jump = false;
		}

        if (!m_Grounded && !m_Jump) {
            m_anim.SetTrigger("Jump");
        } else {
            m_anim.ResetTrigger("Jump");
        }

        if (m_anim.IsInTransition(0)) {
            m_anim.ResetTrigger("Action1");
            m_anim.ResetTrigger("Action2");
            m_anim.ResetTrigger("Special");
            m_anim.ResetTrigger("Damaged");
        }
	}

	void OnDrawGizmos()
	{
		Gizmos.DrawWireSphere(new Vector2(transform.position.x+CircleCastOffset.x,transform.position.y+CircleCastOffset.y),CircleCastRadius);
	}

    public void FireSkill(int slot)
    {

        Skill skillToFire = m_instancedSkills[slot];
        if (!skillToFire.Locked) {
            skillToFire.Execute();
            AudioManager.Singleton.PlaySFX(skillToFire.m_sfx);

            if (slot == 0) {
                m_anim.SetTrigger("Action2"); //melee
            } else if (slot == 1) {
                m_anim.SetTrigger("Action1"); //ranged
            } else if (slot == 2) {
                m_anim.SetTrigger("Special"); //special
            }
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
            
			m_Jump = true;
		}
	}

	public void Move(float h)
	{
        if (m_Slowed) {
            h = h / 3;
        }
        m_anim.SetFloat("Speed", Mathf.Abs(h));
        if(m_anim.GetCurrentAnimatorStateInfo(0).IsName("Special") || m_anim.GetCurrentAnimatorStateInfo(0).IsName("Action1")) return;
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
			m_FallThroughPlatform = true;
			this.collider2D.isTrigger = true;
		} 
	}
     

    public void TakeDamage(int amount)
    {
        m_anim.SetTrigger("Damaged");
    }
}
