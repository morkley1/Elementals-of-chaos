using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Swing : MonoBehaviour
{
    Collider m_Collider;
    Renderer rend;

    public string key = "Click";

    // Start is called before the first frame update
    void Start()
    {
        m_Collider = GetComponent<Collider>();
        rend = GetComponent<Renderer>();
        rend.enabled = true;
    }

    // Update is called once per frame
    void Update()
    {
        m_Collider.enabled = false;
        rend.enabled = false;

        if (Input.GetButtonDown(key))
        {
            m_Collider.enabled = true;
            rend.enabled = true;
        }
    }
}
