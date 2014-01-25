using UnityEngine;
using System.Collections;

public class Spawner : MonoBehaviour
{
	public float spawnTime = 5f;		// The amount of time between each spawn.
	public float spawnDelay = 3f;		// The amount of time before spawning starts.
	public GameObject[] enemies;		// Array of enemy prefabs.

	private GameObject m_enemyParent;
	void Start ()
	{
		// Start calling the Spawn function repeatedly after a delay .
		InvokeRepeating("Spawn", spawnDelay, spawnTime);
		m_enemyParent = GameObject.Find ("Enemies");
	}


	void Spawn ()
	{
		// Instantiate a random enemy.
		int enemyIndex = Random.Range(0, enemies.Length);
		GameObject Enemy = (GameObject)Instantiate(enemies[enemyIndex], transform.position, transform.rotation);

		Enemy.transform.parent = m_enemyParent.transform;

		// Play the spawning effect from all of the particle systems.
		foreach(ParticleSystem p in GetComponentsInChildren<ParticleSystem>())
		{
			p.Play();
		}
	}
}
