using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace InControl
{
	public class KeyboardProfile : UnityInputDeviceProfile
	{
		public KeyboardProfile()
		{
			Name = "FPS Keyboard/Mouse";
			Meta = "A keyboard and mouse combination profile appropriate for FPS.";
			
			SupportedPlatforms = new[]
			{
				"Windows",
				"Mac",
				"Linux"
			};
			
			Sensitivity = 1.0f;
			LowerDeadZone = 0.0f;
			
			ButtonMappings = new[]
			{
				new InputControlMapping
				{
					Handle = "Jump",
					Target = InputControlType.Action1,
					Source = KeyCodeButton(KeyCode.Space)
				},
				new InputControlMapping
				{
					Handle = "Skill1",
					Target = InputControlType.Action2,
					Source = KeyCodeButton(KeyCode.Alpha1)
				},
				new InputControlMapping
				{
					Handle = "Skill2",
					Target = InputControlType.Action3,
					Source = KeyCodeButton(KeyCode.Alpha2)
				},
				new InputControlMapping
				{
					Handle = "Skill3",
					Target = InputControlType.Action4,
					Source = KeyCodeButton(KeyCode.Alpha3)
				},
				new InputControlMapping
				{
					Handle = "Start",
					Target = InputControlType.Start,
					Source = KeyCodeButton( KeyCode.Return )
				}
			};
			
			AnalogMappings = new[]
			{
				new InputControlMapping
				{
					Handle = "Move X",
					Target = InputControlType.LeftStickX,
					Source = KeyCodeAxis( KeyCode.LeftArrow, KeyCode.RightArrow )
				},
				new InputControlMapping
				{
					Handle = "Move Y",
					Target = InputControlType.LeftStickY,
					Source = KeyCodeAxis( KeyCode.DownArrow, KeyCode.UpArrow )
				}
			};
		}
	}
}

