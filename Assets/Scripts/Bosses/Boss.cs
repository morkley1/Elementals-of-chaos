using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Boss : MonoBehaviour
{
    public Transform player;

    public NavMeshAgent Agent;

    [SerializeField] List<GameObject> drops = new List<GameObject> { };

    [SerializeField] protected List<Phase> phases;
    public Phase phase;
    public float health = 200;
    public float maxHealth = 200;
    public int pha;
    public Transform self;

    public int phacount;
    public FireballEnemE fireball;

    // Start is called before the first frame update
    void Start()
    {
        //phase = phases[0]; 
        //phase.start(this);
        Agent = GetComponent<NavMeshAgent>();
        if (GameObject.Find("First person player") != null)
        {
            player = GameObject.Find("First person player").transform;
        }
    }

    // Update is called once per frame
    public void Update()
    {
        //phase.Update();
        update();
        if (health <= 0)
        {
            GameObject.Find("EXP").GetComponent<XPBar>().bar.value += 200;
            if (pha >= 3/*phases.Count - 1*/)
            {
                Debug.Log("dead");
                foreach (GameObject game in drops)
                {
                    Instantiate(game, transform.position + Vector3.up * 1, Quaternion.Euler(new Vector3(Random.Range(0, 360), Random.Range(0, 360), Random.Range(0, 360))));
                }
                Destroy(gameObject);
            }
            else
            {
                pha++;
                start();
                //phase = phases[pha];
                //phase.start(this);
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        EnemyDMG dMG = other.GetComponent<EnemyDMG>();
        if (dMG == null) return;
        health -= dMG.DMG;
    }

    public virtual void update()
    {

    }
    public virtual void start()
    {

    }
}
