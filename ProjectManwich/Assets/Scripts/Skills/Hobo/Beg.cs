using UnityEngine;
using System.Collections;

public class Beg : Skill {

    public float m_effectDistance;
	public GameObject coinObject;

    public override void Execute()
    {
        Debug.Log("Fired Skill - Beg");
        if (!Activated && !Locked) {
            Activated = true;
            //Steal Money
            Player[] players = PlayerManager.GetPlayers();
            for (int i = 0; i < players.Length; i++) {
                Player curr = players[i];
                //make sure we are not checking with ourselves
                if (curr != null && curr.m_character != m_myCharacter) {
                    float distance = Vector3.Distance(m_myCharacter.gameObject.transform.position, curr.m_character.gameObject.transform.position);
                    if (distance <= m_effectDistance) {
                        GameObject coin = (GameObject)Instantiate(coinObject);
                        coin.GetComponent<FlyingCoin>().Initiate(100, curr, m_myCharacter.m_Player);
                        curr.m_money -= 100;
                    }
                }
            }
        }

        Activated = false;
        StartCooldownTimer();
        Locked = true;
    }
}
