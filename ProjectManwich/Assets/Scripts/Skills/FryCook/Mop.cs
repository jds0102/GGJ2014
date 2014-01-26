using UnityEngine;
using System.Collections;

public class Mop : Skill {
    public MopWater m_mopWater;

    public override void Execute()
    {
        if (!Locked) {
            MopWater water = (MopWater)(GameObject.Instantiate(m_mopWater, m_myCharacter.m_GroundCheck.transform.position, Quaternion.Euler(new Vector3(0, 0, 0))));
            water.Owner = m_myCharacter.m_Player;
            StartCooldownTimer();
            Locked = true;
        }
    }
}
