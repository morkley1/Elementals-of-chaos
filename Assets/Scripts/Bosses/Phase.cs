using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public abstract class Phase
{
    public Transform player;
    protected NavMeshAgent Agent;
    [SerializeField] Phase phaphase;
    protected float phaseHealth;
    protected Phase phase;
    public float health;
    public float maxHealth;
    protected Transform self;
    public FireballEnemE fireball;

    public void start(Boss selff)
    {
        selff.maxHealth = phaseHealth;
        selff.health = phaseHealth;
        player = selff.player;
        Agent = selff.Agent;
        phase = selff.phase;
        self = selff.self;
        health = selff.health;
        maxHealth = selff.maxHealth;
        fireball = selff.fireball;
        Init();
    }

    public abstract void Init();

    public abstract void Update();
}
