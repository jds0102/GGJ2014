using UnityEngine;
using System.Collections;

public class Notification : MonoBehaviour
{
    public AnimationCurve m_curve;

    public float m_duration;

    private float m_startTime;
    private float m_startX = 1.33f;
    private float m_endX = -0.66f;

	// Use this for initialization
	void Start ()
    {
        m_startTime = Time.time;
	}
	
	// Update is called once per frame
	void Update ()
    {
        float timeElapsed = Time.time - m_startTime;
        float percentComplete = timeElapsed / m_duration;

        float lerpPercent = m_curve.Evaluate(percentComplete);

        float newX = Mathf.Lerp(m_startX, m_endX, lerpPercent);
        transform.position = new Vector3(newX, transform.position.y, transform.position.z);

        if (percentComplete >= 1.0) {
            Destroy(this.gameObject);
        }
	}

    public void SetText(string text)
    {
        GUIText mesh = GetComponent<GUIText>();
        mesh.text = text;
    }
}
