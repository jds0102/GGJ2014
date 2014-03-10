using UnityEngine;
using InControl;
using System.Collections;

public class InControlManager : MonoBehaviour {

	public static InControlManager singleton;
	// Use this for initialization
	void Start () {
		InputManager.Setup ();
		InputManager.AttachDevice( new UnityInputDevice( new KeyboardProfile() ) );
		if (singleton == null) {
			singleton = this;
		}

		//Whenever we have a device change we want to update the players
		InputManager.OnDeviceAttached += inputDevice => PlayerManager.m_singleton.DeviceChangeDetected ();
		InputManager.OnDeviceDetached += inputDevice => PlayerManager.m_singleton.DeviceChangeDetected ();

	}
	
	// Update is called once per frame
	void Update () {
		InputManager.Update ();

		for(int i=0; i<InputManager.Devices.Count; i++) {
			//Debug.Log(InputManager.Devices[i].Meta);
			//Debug.Log(InputManager.Devices[i].Action1);
		}
		
		//Debug.Log ("Active = " + InputManager.ActiveDevice.Meta);
	}
}
