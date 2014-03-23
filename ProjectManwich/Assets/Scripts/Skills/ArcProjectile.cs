using UnityEngine;
using System.Collections;

public class ArcProjectile : Projectile
{

    public float horizontalFactor;
    public float verticalFactor;
    public float power;

    public override void Start()
    {
        if (m_animator == null) {
            m_animator = GetComponent<Animator>();
        }
        Vector2 direction = new Vector2(-1 * horizontalFactor, 1.5f * verticalFactor) * power;
        if (FireDirection == Direction.Right) {
            direction.x *= -1;
        }
        this.rigidbody2D.AddForce(direction);
        Destroy(gameObject, 5);
    }


    public override void Update()
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
    }
}
	
