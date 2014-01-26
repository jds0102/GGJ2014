using UnityEngine;
using System.Collections;

public class CameraManager : MonoBehaviour {

    public const float minFoV = 25.0f;


	void Start()
	{
	
	}
	
	void Update()
	{
		Vector3 position = Vector3.zero;
		for(int i = 0; i < PlayerManager.m_singleton.playerCount; i++){
            Player p = PlayerManager.m_singleton.m_players[i];
            if (p != null && p.m_character != null) {
                position = position + p.m_character.gameObject.transform.position;
            }
		}


		float distance = 0.0f;
		for(int i = 0; i < PlayerManager.m_singleton.playerCount; i++){
			for(int j = i+1; j < PlayerManager.m_singleton.playerCount; j++){
                Player p1 = PlayerManager.m_singleton.m_players[0];
                if (p1 == null || p1.m_character == null) {
                    continue;
                }

                Player p = PlayerManager.m_singleton.m_players[j];
                if (p == null || p.m_character == null) {
                    continue;
                }

                float d = (p1.m_character.transform.position - p.m_character.transform.position).magnitude;
				if(distance < d){
					distance = d;
				}
			} 
		}
        
		Vector3 destination = new Vector3(position.x/PlayerManager.m_singleton.playerCount,position.y/PlayerManager.m_singleton.playerCount,transform.position.z);//new Vector3(position.x/PlayerManager.m_singleton.playerCount,Camera.main.transform.position.z);
        transform.position = Vector3.Lerp(transform.position, destination, Time.deltaTime);
		//Camera.main.rect = new Rect(Camera.main.rect.x,Camera.main.rect.y,30.0f/distance,30.0f/distance);

        float cameraSize = distance / 2;
        cameraSize = Mathf.Max(minFoV, cameraSize);
        Camera.main.orthographicSize = cameraSize;
	}

	void ZoomIn()
	{

	}

	void ZoomOut()
	{

	}
}
