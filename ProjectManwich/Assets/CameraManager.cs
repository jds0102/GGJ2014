using UnityEngine;
using System.Collections;

public class CameraManager : MonoBehaviour {

	void Start()
	{
	
	}
	
	void Update()
	{
		Vector3 position = Vector3.zero;
		for(int i = 0; i < PlayerManager.m_singleton.playerCount; i++){
			position = position + PlayerManager.m_singleton.m_players[i].m_character.gameObject.transform.position;
		}


		float distance = 0.0f;
		for(int i = 0; i < PlayerManager.m_singleton.playerCount; i++){
			for(int j = i+1; j < PlayerManager.m_singleton.playerCount; j++){
				float d = (PlayerManager.m_singleton.m_players[0].m_character.transform.position - PlayerManager.m_singleton.m_players[j].m_character.transform.position).magnitude;
				if(distance < d){
					distance = d;
				}
			} 
		}
		Debug.Log (distance);
		transform.position = new Vector3(position.x/PlayerManager.m_singleton.playerCount,transform.position.z);
		Camera.main.rect = new Rect(Camera.main.rect.x,Camera.main.rect.y,30.0f/distance,30.0f/distance);
	}

	void ZoomIn()
	{

	}

	void ZoomOut()
	{

	}
}
