using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Phase : ScriptableObject
{
    PhaseScript phase;
    float health;

    public void start(Boss self)
    {
        self.maxHealth = health;
        self.health = health;
    }

    public void Update()
    {

    }
}
