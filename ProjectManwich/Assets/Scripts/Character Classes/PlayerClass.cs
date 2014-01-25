using UnityEngine;
using System;
using System.Collections;

public class PlayerClass : MonoBehaviour {

    public Skill[] m_skills = new Skill[3];

	void Start () 
    {
	    
	}
	
	void Update () 
    {
	
	}

    public void FireSkill(int slot)
    {
        Skill skillToFire = m_skills[slot];
        skillToFire.Execute();
    }
}

