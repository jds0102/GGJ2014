using UnityEngine;
using System.Collections;

public class MopWater : MonoBehaviour {

    public Player Owner
    {
        get;
        set;
    }

	void Start () {
        Destroy(gameObject, 5);
	}
	
	void Update () {
	
	}
}
