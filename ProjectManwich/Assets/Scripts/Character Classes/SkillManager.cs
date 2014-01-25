using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SkillManager : MonoBehaviour {

    private static SkillManager m_instance;
    public List<Skill> m_skills;

    public static List<Skill> Skills {
        get { return m_instance.m_skills; }
    }

	void Awake () 
    {
        if (m_instance == null) {
            m_instance = this;
        }
    }	
}
