using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class navmeshEnemy : MonoBehaviour
{
    public Transform player;

    UnityEngine.AI.NavMeshAgent myNavMeshAgent;

    public float[] health = new float[10] { 25f, 100f, 10f, 5f, 10f, 5f, 10f, 5f, 10f, 5f };
    public float maxHealthTEMP = 44f;
    public float healthTEMP = 44f;

    [SerializeField] List<GameObject> drops = new List<GameObject>{};

    // Start is called before the first frame update
    void Start()
    {
        myNavMeshAgent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        if (GameObject.Find("First person player") != null)
        {
            player = GameObject.Find("First person player").transform;
        }
    }

    // Update is called once per frame
    void Update()
    {
        myNavMeshAgent.SetDestination(player.position);
        if (healthTEMP <= 0)
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
        healthTEMP -= dMG.DMG;
    }
}
