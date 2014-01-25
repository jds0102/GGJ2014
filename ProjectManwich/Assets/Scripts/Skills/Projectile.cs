using UnityEngine;
using System.Collections;

public class Projectile : MonoBehaviour 
{
	public GameObject explosion;		// Prefab of explosion effect.
    public float m_speed;


	void Start () 
	{
		// Destroy the rocket after 2 seconds if it doesn't get destroyed before then.
		Destroy(gameObject, 5);
	}

    void Update()
    {
        if (this != null) {
            this.rigidbody2D.velocity = new Vector2(m_speed, 0);
        }

        //this.gameObject.transform.position.Set(this.gameObject.transform.position.x + 5, this.gameObject.transform.position.y, this.gameObject.transform.position.z);
    }

	void OnExplode()
	{
		// Create a quaternion with a random rotation in the z-axis.
		//Quaternion randomRotation = Quaternion.Euler(0f, 0f, Random.Range(0f, 360f));

		// Instantiate the explosion where the rocket is with the random rotation.
		//Instantiate(explosion, transform.position, randomRotation);
	}

    void OnTriggerEnter2D(Collider2D col)
    {

    }
}
