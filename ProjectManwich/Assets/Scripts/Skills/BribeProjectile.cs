using UnityEngine;
using System.Collections;

public class BribeProjectile : Projectile {

    public GameObject m_targetMarker;

    public void OnMarkTarget(Character hitChar) {
        Vector3 hitCharPos = hitChar.transform.position;
        hitCharPos.y += 5.0f;
        GameObject target = (GameObject)(GameObject.Instantiate(m_targetMarker, hitCharPos, Quaternion.EulerAngles(0, 0, 0)));
        target.transform.parent = hitChar.transform;
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
