using UnityEngine;
using System.Collections;

public class MeleeSkill : Skill {
    public float m_range;

    public override void Execute()
    {
        if (!Locked) {
            Debug.Log("Fired Skill - Bottle - Character[" + m_myCharacter + "]");
            Vector3 charPos = m_myCharacter.gameObject.transform.position;


            RaycastHit2D[] playerHits;
            if (m_myCharacter.FaceRight) {
                Vector3 endPos = charPos;
                endPos.x += m_range;
                playerHits = Physics2D.LinecastAll(charPos, endPos, 1 << LayerMask.NameToLayer("Player1") | 1 << LayerMask.NameToLayer("Player2") | 1 << LayerMask.NameToLayer("Player3") | 1 << LayerMask.NameToLayer("Player4"));
            } else {
                Vector3 endPos = charPos;
                endPos.x -= m_range;
                playerHits = Physics2D.LinecastAll(charPos, endPos, 1 << LayerMask.NameToLayer("Player1") | 1 << LayerMask.NameToLayer("Player2") | 1 << LayerMask.NameToLayer("Player3") | 1 << LayerMask.NameToLayer("Player4"));
            }

            foreach (RaycastHit2D hit in playerHits) {
                if (hit != null) {
                    Debug.Log("Hit: " + hit.transform.gameObject);
                    if (hit.transform.gameObject != m_myCharacter.gameObject) {
                        // TODO: HANDLE HIT STUFF HERE
                        Debug.Log("Bottle hit: " + hit.transform.gameObject);
                        Character hitChar = hit.transform.gameObject.GetComponent<Character>();
                        if (hitChar != null) {
                            if (hitChar.m_Player.TakeDamage(1)) {
                                   Debug.Log(m_myCharacter.gameObject + " killed " + hitChar + "!");
                            }
                        }
                    }
                }
            }

            RaycastHit2D[] breakableHits;
            if (m_myCharacter.FaceRight) {
                Vector3 endPos = charPos;
                endPos.x += m_range;
                breakableHits = Physics2D.LinecastAll(charPos, endPos, 1 << LayerMask.NameToLayer("BreakableObjects"));
            } else {
                Vector3 endPos = charPos;
                endPos.x -= m_range;
                breakableHits = Physics2D.LinecastAll(charPos, endPos, 1 << LayerMask.NameToLayer("BreakableObjects"));
            }

            foreach (RaycastHit2D hit in breakableHits) {
                if (hit != null) {
                    if (hit.transform.gameObject != m_myCharacter.gameObject) {
                        // TODO: HANDLE HIT STUFF HERE
                        Debug.Log("Bottle hit: " + hit.transform.gameObject);
                        BreakableObject hitObj = hit.transform.gameObject.GetComponent<BreakableObject>();
                        if (hitObj != null) {
                            hitObj.Activate(m_myCharacter.m_Player);
                        }
                    }
                }
            }

            StartCooldownTimer();
            Locked = true;
        }
    }
}
