using UnityEngine;
using System.Collections;

public class Bottle : Skill {

    public float m_range;

    public override void Execute()
    {
        Debug.Log("Fired Skill - Bottle");

        if (!Activated && !Locked) {
            Activated = true;
            Vector3 charPos = m_myCharacter.gameObject.transform.position;


            RaycastHit2D[] hits; 
            if (m_myCharacter.FaceRight) {
                Vector3 endPos = charPos;
                endPos.x += m_range;
                hits = Physics2D.LinecastAll(charPos, endPos, 1 << LayerMask.NameToLayer("Player")); 
            } else {
                Vector3 endPos = charPos;
                endPos.x -= m_range;
                hits = Physics2D.LinecastAll(charPos, endPos, 1 << LayerMask.NameToLayer("Player")); 
            }

            foreach (RaycastHit2D hit in hits) {
                if (hit != null) {
                    if (hit.transform.gameObject != m_myCharacter.gameObject) {
                        // TODO: HANDLE HIT STUFF HERE
                        Debug.Log("Bottle hit: " + hit.transform.gameObject);
                    }
                }
            }
        }
        Activated = false;
    }
}
