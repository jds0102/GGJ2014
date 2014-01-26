using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class Skill : MonoBehaviour {
    public string m_name;
	public Texture m_icon;
	public string m_description;
    public Character m_myCharacter;
	public KeyValuePair<AudioClip,float> m_sfx;
	
	public float m_cooldownTime;
	protected float m_cooldownTimer;

	public virtual bool Locked {
		get; set;
	}

	public virtual bool Cooldown {
		get; set;
	}

	public virtual bool Activated {
		get; set;
	}	
	
	// Execute the skill, this can be overriden by children for special skills
	public virtual void Execute() 
	{

	}

	public virtual void Finish() 
	{

	}

	public virtual int CooldownTimer(int arg) 
	{
		m_cooldownTimer += Time.deltaTime;
		if (m_cooldownTimer > m_cooldownTime) {
			Locked = false;
			Cooldown = false;
			return 1;
		}
		return 0;
	}

	public virtual void StartCooldownTimer() 
	{
		if (!Cooldown) {
			Cooldown = true;
		} else {
			return;
		}

		m_cooldownTimer = 0.0f;
		Func<int, int> executable = CooldownTimer;
		CoroutineHandler.StartCoroutine(executable);
	}

	public float GetCooldownTime() 
	{
		return m_cooldownTimer;
	}
}
