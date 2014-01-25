using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GUIElement : MonoBehaviour
{
    public bool selected;

    private AnimationCurve m_selectedCurve;
    private GUIText m_text;

	// Use this for initialization
	void Start () {
        m_text = GetComponent<GUIText>();
        m_selectedCurve = AnimationCurve.EaseInOut(0, m_text.fontSize, 0.5f, m_text.fontSize * (.50f) + m_text.fontSize);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    
    void OnGUI()
    {
        if (selected) {
            m_text.fontSize = (int)m_selectedCurve.Evaluate(Time.time % .5f);
        }
    }

    public void ResetFont()
    {
        m_text.fontSize = 30;
    }
}
