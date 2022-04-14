using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bap : MonoBehaviour
{
    public Transform zap;
    public Transform home;
    public Transform self;
    public LayerMask enemyMask;
    RaycastHit zapHit;
    public float zapDist = 100f;
    bool zapCheck;

    Collider m_Collider;
    Renderer rend;

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
        Fire();
    }
    void Fire()
    {
        m_Collider.enabled = false;
        rend.enabled = false;

        if (GameObject.Find("First person player").GetComponent<playerWalk>().mana >= 10)
        {
            Ray ray = new Ray(zap.position, zap.forward);
            zapCheck = Physics.Raycast(ray, out zapHit, zapDist, enemyMask);
            if (zapCheck && Input.GetButtonDown("RClick"))
            {
                GameObject.Find("First person player").GetComponent<playerWalk>().mana -= 10;
                self.position = zapHit.point;
                m_Collider.enabled = true;
                rend.enabled = true;
            }
            else
            {
                self.position = home.position;
            }
        }
    }
}