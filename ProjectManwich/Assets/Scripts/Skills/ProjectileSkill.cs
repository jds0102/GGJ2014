using UnityEngine;
using System.Collections;

public class ProjectileSkill : Skill {
    public Projectile m_projectile;

    public override void Execute()
    {
        if (!Locked) {
            Debug.Log("Fired Skill - Projectile - Character [" + m_myCharacter + "]");

            Vector3 spawnPos = m_myCharacter.gameObject.transform.position;
            
            Transform launchPos = null;
            if (m_myCharacter.FaceRight) {
                launchPos = m_myCharacter.transform.FindChild("projectileLauncherRight");
            } else {
                launchPos = m_myCharacter.transform.FindChild("projectileLauncherLeft");
            }

            if (launchPos != null) {
                spawnPos = launchPos.transform.position;
            }

            //Debug.Log("Facing" + m_myCharacter.FaceRight);
            if (m_myCharacter.FaceRight) {
                Projectile bulletInstance = (Projectile)(GameObject.Instantiate(m_projectile, spawnPos, Quaternion.Euler(new Vector3(0, 0, 0))));
                bulletInstance.FireDirection = Projectile.Direction.Right;
                bulletInstance.Owner = m_myCharacter.m_Player;
            } else {
                Projectile bulletInstance = (Projectile)(GameObject.Instantiate(m_projectile, spawnPos, Quaternion.Euler(new Vector3(0, 0, 180))));
                bulletInstance.FireDirection = Projectile.Direction.Left;
                bulletInstance.Owner = m_myCharacter.m_Player;
            }
            StartCooldownTimer();
            Locked = true;
        }
    }
}
