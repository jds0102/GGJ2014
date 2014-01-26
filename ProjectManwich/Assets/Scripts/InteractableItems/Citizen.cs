using UnityEngine;
using System.Collections;

public class Citizen : BreakableObject {

	public GameObject m_moneyDrop;
	public int m_value;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	//Called by players melee attack
	override public void Activate(Player player) {
		base.Activate(player);
		GameObject money = (GameObject)Instantiate (m_moneyDrop);
		money.GetComponent<MoneyDrop> ().Initiate (m_value, 2);
		money.transform.position = transform.position;
	}
}
