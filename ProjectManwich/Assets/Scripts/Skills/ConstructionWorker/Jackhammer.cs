using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class Jackhammer : Skill
{
    public float m_range;
    public bool Stunning { get; set; }

    protected float m_stunTimer;
    public float m_stunTime;

    private List<Character> m_stunnedChars;

    public override void Execute()
    {
        if (!Locked) {
            m_stunnedChars = new List<Character>();

            Debug.Log("Fired Skill - Bottle - Character[" + m_myCharacter + "]");
            Vector3 charPos = m_myCharacter.gameObject.transform.position;


            RaycastHit2D[] playerHitsRight;
            RaycastHit2D[] playerHitsLeft;

            Vector3 endPos = charPos;
            endPos.x += m_range;
            playerHitsRight = Physics2D.LinecastAll(charPos, endPos, 1 << LayerMask.NameToLayer("Player1") | 1 << LayerMask.NameToLayer("Player2") | 1 << LayerMask.NameToLayer("Player3") | 1 << LayerMask.NameToLayer("Player4"));
                
            endPos = charPos;
            endPos.x -= m_range;
            playerHitsLeft = Physics2D.LinecastAll(charPos, endPos, 1 << LayerMask.NameToLayer("Player1") | 1 << LayerMask.NameToLayer("Player2") | 1 << LayerMask.NameToLayer("Player3") | 1 << LayerMask.NameToLayer("Player4"));

            //handle right hits
            foreach (RaycastHit2D hit in playerHitsRight) {
                if (hit != null) {
                    Debug.Log("Hit: " + hit.transform.gameObject);
                    if (hit.transform.gameObject != m_myCharacter.gameObject) {
                        Debug.Log("Bottle hit: " + hit.transform.gameObject);
                        Character hitChar = hit.transform.gameObject.GetComponent<Character>();
                        if (hitChar != null) {
                            hitChar.m_Stunned = true;
                            m_stunnedChars.Add(hitChar);
                        }
                    }
                }
            }

            //handle left hits
            foreach (RaycastHit2D hit in playerHitsLeft) {
                if (hit != null) {
                    Debug.Log("Hit: " + hit.transform.gameObject);
                    if (hit.transform.gameObject != m_myCharacter.gameObject) {
                        Debug.Log("Bottle hit: " + hit.transform.gameObject);
                        Character hitChar = hit.transform.gameObject.GetComponent<Character>();
                        if (hitChar != null) {
                            hitChar.m_Stunned = true;
                            m_stunnedChars.Add(hitChar);
                        }
                    }
                }
            }

            /*RaycastHit2D[] breakableHits;
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
						} else {
							hitObj = hit.transform.gameObject.GetComponent<Civilian>();
							if(hitObj != null) {
								hitObj.Activate(m_myCharacter.m_Player);
							}
						}
                    }
                }
            }*/

            StartStunTimer();
            StartCooldownTimer();
            Locked = true;
        }
    }

    public int StunTimer(int arg)
    {
        m_stunTimer += Time.deltaTime;
        if (m_stunTimer > m_stunTime) {
            Stunning = false;
            foreach (Character character in m_stunnedChars) {
                character.m_Stunned = false;
            }
            return 1;
        }
        return 0;
    }

    public void StartStunTimer()
    {
        if (!Stunning) {
            Stunning = true;
        } else {
            return;
        }

        m_stunTimer = 0.0f;
        Func<int, int> executable = StunTimer;
        CoroutineHandler.StartCoroutine(executable);
    }
}
