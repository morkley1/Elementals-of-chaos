using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBoss1 : FireBoss
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Agent.SetDestination(player.position);
    }
}