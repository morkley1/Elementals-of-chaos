using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirBoss : MonoBehaviour
{
    public List<bool> phase = new List<bool> {true, false, false};
    public float health;

    // Start is called before the first frame update
    void Start()
    {
        EnterPhase(1, 400);
    }
    // Update is called once per frame
    void Update()
    {
        if (phase[0])
        {
            Phase1();
        }
        else if (phase[1])
        {
            Phase2();
        }
        else if (phase[2])
        {
            Phase3();
        }
    }
    void Phase1()
    {
        if (health <= 0)
        {
            EnterPhase(2, 800);
        }
    }
    void Phase2()
    {
        if (health <= 0)
        {
            EnterPhase(3, 200);
        }
    }
    void Phase3()
    {
        
    }
    void EnterPhase(int phaseToEnter, float newHealth)
    {
        int i = 0;
        while (i < phase.Count)
        {
            phase[i] = false;
            i++;
        }
        health = newHealth;
        phase[phaseToEnter - 1] = true;
    }
}
