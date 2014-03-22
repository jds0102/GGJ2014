using UnityEngine;
using System.Collections;

public class NotificationManager : MonoBehaviour 
{
    public GameObject m_notificationPrefab;

    private static NotificationManager m_singleton;

	// Use this for initialization
	void Start () {
        m_singleton = this;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public static void CreateNewNotification(string text)
    {
        GameObject notificationObject = (GameObject)GameObject.Instantiate(m_singleton.m_notificationPrefab);
        Notification notification = notificationObject.GetComponent<Notification>();
        notification.transform.parent = m_singleton.transform;
        notification.SetText(text);
    }
}
