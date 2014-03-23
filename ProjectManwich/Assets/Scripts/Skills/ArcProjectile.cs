using UnityEngine;
using System.Collections;

public class ArcProjectile : Projectile {
    
    public float horizontalFactor;
    public float verticalFactor;
    public float power;

    public override void Start()
    {
        Vector2 direction = new Vector2(-1 * horizontalFactor, 1.5f * verticalFactor) * power;
        if (FireDirection == Direction.Right) {
            direction.x *= -1;
        }
        this.rigidbody2D.AddForce(direction);
        Destroy(gameObject, 5);
    }

    public override void Update()
    {

    }

}
