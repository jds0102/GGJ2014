using UnityEngine;
using System.Collections;

public class Heckle : Skill {

    public Projectile m_projectile;
    private int m_routineHandle;

    public override void Execute()
    {
        Debug.Log("Fired Skill - Heckle - Character [" + m_myCharacter + "]");

        if (!Locked) {
            Vector3 spawnPos = m_myCharacter.gameObject.transform.position;
            //Debug.Log("Facing" + m_myCharacter.FaceRight);
            if (m_myCharacter.FaceRight) {
                Projectile bulletInstance = (Projectile)(GameObject.Instantiate(m_projectile, spawnPos, Quaternion.Euler(new Vector3(0, 0, 0))));
                bulletInstance.FireDirection = Projectile.Direction.Right;
            } else {
                Projectile bulletInstance = (Projectile)(GameObject.Instantiate(m_projectile, spawnPos, Quaternion.Euler(new Vector3(0, 0, 180))));
                bulletInstance.FireDirection = Projectile.Direction.Left;
            }
            StartCooldownTimer();
            Locked = true;
        } 
    }
}
