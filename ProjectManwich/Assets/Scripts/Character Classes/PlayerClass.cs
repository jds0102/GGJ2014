using UnityEngine;
using System;
using System.Collections;

public class PlayerClass {

    public Skill[] m_skills = new Skill[3];

    public PlayerClass()
    {

    }

    public void FireSkill(int slot)
    {
        Skill skillToFire = m_skills[slot];
        skillToFire.Execute();
    }
}

