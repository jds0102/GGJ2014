using UnityEngine;
using System.Collections;

public class ArcProjectile : Projectile {
    
    // Use this for initialization
    public override void Start()
    {
        Vector2 direction = new Vector2(-1, 1.5f) * 300;
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
