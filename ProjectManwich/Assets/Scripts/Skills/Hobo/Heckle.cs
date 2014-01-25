using UnityEngine;
using System.Collections;

public class Heckle : Skill {

    public Projectile m_projectile;

    public override void Execute()
    {
        Debug.Log("Fired Skill - Heckle");

        if (!Activated && !Locked) {
            Activated = true;
            Rigidbody2D bulletInstance = Instantiate(m_projectile, m_myCharacter.gameObject.transform.position, Quaternion.Euler(new Vector3(0, 0, 0))) as Rigidbody2D;
            if (bulletInstance != null) {
                bulletInstance.velocity = new Vector2(m_projectile.m_speed, 0);
            }
        }
        Activated = false;
    }
}
