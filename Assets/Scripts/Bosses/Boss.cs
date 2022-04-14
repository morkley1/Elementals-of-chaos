using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Boss : MonoBehaviour
{
    public Transform player;

    protected NavMeshAgent Agent;

    [SerializeField] List<GameObject> drops = new List<GameObject> { };

    [SerializeField] List<Phase> phases;
    protected Phase phase;
    public float health;
    public float maxHealth;

    // Start is called before the first frame update
    void Start()
    {
        phase = phases[0]; 
        Agent = GetComponent<NavMeshAgent>();
        if (GameObject.Find("First person player") != null)
        {
            player = GameObject.Find("First person player").transform;
        }
    }

    // Update is called once per frame
    void Update()
    {
        phase.Update();
        if (health <= 0)
        {
            GameObject.Find("EXP").GetComponent<XPBar>().bar.value += 20;
            foreach (GameObject game in drops)
            {
                if (Random.Range(1, 10) == 1)
                {
                    Instantiate(game, transform.position + Vector3.up * 1, Quaternion.Euler(new Vector3(Random.Range(0, 360), Random.Range(0, 360), Random.Range(0, 360))));
                }
            }
            Destroy(this.gameObject);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        EnemyDMG dMG = other.GetComponent<EnemyDMG>();
        if (dMG == null) return;
        health -= dMG.DMG;
    }
}
