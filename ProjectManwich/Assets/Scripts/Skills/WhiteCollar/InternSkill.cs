using UnityEngine;
using System.Collections;

public class InternSkill : Skill {

	public GameObject internPrefab;

	private GameObject m_intern;

	public override void Execute() 
	{
		//Instantiate the intern
		if (!Locked) {
			m_intern = (GameObject)GameObject.Instantiate (internPrefab, m_myCharacter.transform.position, m_myCharacter.transform.rotation);
			m_intern.GetComponent<Intern>().Owner = m_myCharacter.m_Player;
			StartCooldownTimer();
			Locked = true;
		}
	}
}
