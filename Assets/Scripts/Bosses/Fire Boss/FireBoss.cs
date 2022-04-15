using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBoss : Boss
{
    public FireBoss1 fireBoss1 = new FireBoss1(500);
    public FireBoss2 fireBoss2 = new FireBoss2(400);
    public FireBoss3 fireBoss3 = new FireBoss3(300);

    // Start is called before the first frame update
    void Start()
    {
        pha = 1;
        phacount = 3;
        self = transform;
        Agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        if (GameObject.Find("First person player") != null)
        {
            player = GameObject.Find("First person player").transform;
        }
        phase = fireBoss1;
        phase.start(this);
    }

    // Update is called once per frame
    public override void update()
    {
        if (pha == 1)
        {
            fireBoss1.Update();
        }
        else if (pha == 2)
        {
            fireBoss2.Update();
        }
        else
        {
            fireBoss3.Update();
        }
    }
    public override void start()
    {
        if (pha == 1)
        {
            //fireBoss1.start(this);
        }
        else if (pha == 2)
        {
            fireBoss2.start(this);
        }
        else
        {
            fireBoss3.start(this);
        }
    }
}
