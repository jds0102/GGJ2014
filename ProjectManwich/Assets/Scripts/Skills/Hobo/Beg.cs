using UnityEngine;
using System.Collections;

public class Beg : Skill {

    public float m_effectDistance;
    /*private int m_routineHandle;

public override void Execute() 
{
    if (!Activated && !Locked) {
        Activated = true;
        Color tmp = PlayerCharacter.GetColor();
        tmp = new Color(1.0f, 0.0f, 0.0f, tmp.a);
        PlayerCharacter.ChangeColor(tmp);
        Func<int, int> executable = SlowHeartRate;
        m_routineHandle = CoroutineHandler.StartCoroutine(executable);
        Locked = true;
    } else {
        Activated = false;
        Color tmp = Color.white;
        tmp.a = PlayerCharacter.GetColor().a;
        PlayerCharacter.ChangeColor(tmp);
        CoroutineHandler.TakeDown(m_routineHandle);
        StartCooldownTimer();
    }
}

public int SlowHeartRate(int arg) {
    PlayerCharacter.s_singleton.SlowHeartRate(1);
    return 0;
}*/

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
                if (curr != null && curr.m_character.gameObject != this.gameObject) {
                    float distance = Vector3.Distance(gameObject.transform.position, curr.m_character.gameObject.transform.position);
                    if (distance <= m_effectDistance) {
                        // TODO: hook into player to steal $$
                    }
                }
            }
        }
    }
}
