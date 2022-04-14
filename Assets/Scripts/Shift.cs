using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shift : MonoBehaviour
{
    public Transform pos1;
    public Transform pos2;
    public Transform self;

    public float secondIsSeconds = 1.0f;

    public float distance;

    // Start is called before the first frame update
    void Start()
    {
        distance = 0;
    }

    // Update is called once per frame
    void Update()
    {
        Patrol();
    }

    void Patrol()
    {
        self.position = pos1.position + ((pos2.position - pos1.position) * Mathf.SmoothStep(0, 1, Mathf.PingPong((Time.time / secondIsSeconds), 1)));
    }
}
