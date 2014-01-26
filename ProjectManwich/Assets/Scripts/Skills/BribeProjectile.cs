using UnityEngine;
using System.Collections;

public class BribeProjectile : Projectile {

    public void OnMarkTarget(Character hitChar) {
        
    }

    public override void OnTriggerEnter2D(Collider2D col)
    {
        if (Owner.m_character == null) {
            OnExplode();
            Destroy(gameObject);
            return;
        }
        if (col != Owner.m_character.collider2D) {
            foreach (Player p in PlayerManager.GetPlayers()) {
                if (p != null && p.m_character != null && col.gameObject == p.m_character.gameObject) {
                    Debug.Log("Hit: " + col.gameObject);
                    Character hitChar = col.gameObject.GetComponent<Character>();
                    Debug.Log("Hit Player: " + hitChar);
                    if (hitChar != null) {
                        /*if (hitChar.m_Player.TakeDamage(1)) {
                            Debug.Log(Owner + " killed " + hitChar + "!");
                        }*/                   
                        OnMarkTarget(hitChar);
                    }
 
                    Destroy(gameObject);
                }
            }
            if (col.gameObject.layer == LayerMask.NameToLayer("Platform")) {
                Debug.Log("Hit: " + col.gameObject);
                OnExplode();
                Destroy(gameObject);
            }
        }
    }
}
