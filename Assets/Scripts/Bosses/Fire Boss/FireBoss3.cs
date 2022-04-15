using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBoss3 : Phase
{
    public FireBoss3(float health)
    {
        phaseHealth = health;
    }
    // Start is called before the first frame update
    public override void Init()
    {
        Debug.Log(this);
    }

    // Update is called once per frame
    public override void Update()
    {
        Agent.SetDestination(player.position);
    }
}
