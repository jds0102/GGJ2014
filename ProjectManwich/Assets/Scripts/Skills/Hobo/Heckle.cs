using UnityEngine;
using System.Collections;

public class Heckle : Skill {

    public Projectile m_projectile;

    void Update()
    {

    }

    public override void Execute()
    {
        Debug.Log("Fired Skill - Heckle");

        if (!Activated && !Locked) {
            Activated = true;
            Vector3 spawnPos = m_myCharacter.gameObject.transform.position;
            Projectile bulletInstance = (Projectile)(GameObject.Instantiate(m_projectile, spawnPos, Quaternion.Euler(new Vector3(0, 0, 0))));
        }
        Activated = false;
    }
}
