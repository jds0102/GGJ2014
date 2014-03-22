using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GUIElement : MonoBehaviour
{
    public bool selected;

    private AnimationCurve m_selectedCurveUp, m_selectedCurveDown, m_currentCurve;
    private GUIText m_text;

	private bool m_resetting;
	private int m_defaultFontSize, m_fontMax;

	private float guiTimeModifier = 0;

	// Use this for initialization
	void Start () {
        m_text = GetComponent<GUIText>();
		m_defaultFontSize = m_text.fontSize;
		m_fontMax = (int) (m_text.fontSize * (.50f) + m_text.fontSize);
		m_selectedCurveUp = AnimationCurve.EaseInOut(0, m_defaultFontSize, .5f, m_fontMax);
		m_selectedCurveDown = AnimationCurve.EaseInOut(.5f, m_fontMax, 1.0f,m_defaultFontSize );
		m_currentCurve = m_selectedCurveUp;
	}
	
	// Update is called once per frame
	void Update () {
		if (m_resetting) {
			if ( Mathf.Abs(m_text.fontSize - m_defaultFontSize) < 1) {
				m_text.fontSize = m_defaultFontSize;
				m_resetting = false;
			}
		}

		if (selected) {
			if ((Time.time - guiTimeModifier) % (1.0f) > .5f) {
				m_currentCurve = m_selectedCurveDown;
			} else {
				m_currentCurve = m_selectedCurveUp;
			}
		}
	}
    
    void OnGUI()
    {
		//Basically this makes it look pretty when you switch between buttons, it is black magic
        if (selected ) {
			int nextSize = (int)m_currentCurve.Evaluate((Time.time - guiTimeModifier) % (1.0f));
			if (Mathf.Abs(m_text.fontSize - nextSize) < 5) {
				m_text.fontSize = nextSize;
			} else {
				m_currentCurve = m_selectedCurveUp;
				guiTimeModifier = Time.time % 1.0f;
				nextSize = (int)m_currentCurve.Evaluate((Time.time - guiTimeModifier) % 1.0f);
				m_text.fontSize = nextSize;
			}
		} else if (m_resetting) {
			m_text.fontSize -= (int)((m_text.fontSize - m_defaultFontSize) * .2f);
		}
    }

    public void ResetFont()
    {
		guiTimeModifier = 0;
		m_resetting = true;
    }
}
