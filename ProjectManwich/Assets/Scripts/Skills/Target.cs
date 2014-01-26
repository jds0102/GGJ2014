using UnityEngine;
using System.Collections;

public class Target : MonoBehaviour {

    TextMesh[] m_textMesh;

    public Character Targeted
    {
        get;
        set;
    }

    public Character Owner
    {
        get;
        set;
    }

	void Start () {
        Renderer[] textRenderer = gameObject.GetComponentsInChildren<Renderer>();
        if (textRenderer != null) {
            Debug.Log(textRenderer[1]);
            textRenderer[1].sortingLayerID = 5;
        }
        m_textMesh = gameObject.GetComponentsInChildren<TextMesh>();
	}

    void Update()
    {
        if (Owner == null || Targeted == null) {
            Destroy(this.gameObject);
        }

        if (m_textMesh[0] != null && Targeted != null) {
            m_textMesh[0].text = "x" + Targeted.m_Marked;
            if (!Targeted.FaceRight) {
                Quaternion rot = m_textMesh[0].gameObject.transform.rotation;
                rot.y = 0.0f;
                m_textMesh[0].gameObject.transform.rotation = rot;
            } else {
                Quaternion rot = m_textMesh[0].gameObject.transform.rotation;
                rot.y = 180.0f;
                m_textMesh[0].gameObject.transform.localRotation = rot;
            }
        }
    }
}
