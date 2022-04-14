using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class navmeshEnemyFireCast : MonoBehaviour
{
    public Transform player;
    public FireballEnemE fireball;
    Transform self;
    float delay;
    UnityEngine.AI.NavMeshAgent myNavMeshAgent;
    public float castDelay;
    public float[] health = new float[10] { 25f, 100f, 10f, 5f, 10f, 5f, 10f, 5f, 10f, 5f };
    public float maxHealthTEMP = 44f;
    public float healthTEMP = 44f;

    [SerializeField] List<GameObject> drops = new List<GameObject> { };

    // Start is called before the first frame update
    void Start()
    {
        myNavMeshAgent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        if (GameObject.Find("First person player") != null)
        {
            player = GameObject.Find("First person player").transform;
        }
        self = transform;
    }

    // Update is called once per frame
    void Update()
    {
        myNavMeshAgent.SetDestination(player.position + ((self.position - player.position).normalized * 15f));
        if (((self.position - player.position).normalized * 10f).magnitude < (self.position - player.position).magnitude && ((self.position - player.position).normalized * 20f).magnitude > (self.position - player.position).magnitude && delay <= 0)
        {
            Vector3 direction = player.transform.position - (transform.position + Vector3.up);
            Cast(Quaternion.LookRotation(direction, Vector3.up));
            delay = castDelay;
        }
        if (healthTEMP <= 0)
        {
            GameObject.Find("EXP").GetComponent<XPBar>().bar.value += 50;
            foreach (GameObject game in drops)
            {
                Instantiate(game, transform.position + Vector3.up * 1, Quaternion.Euler(new Vector3(Random.Range(0, 360), Random.Range(0, 360), Random.Range(0, 360))));
            }
            Destroy(this.gameObject);
        }
        delay -= Time.deltaTime;
    }
    void OnTriggerEnter(Collider other)
    {
        EnemyDMG dMG = other.GetComponent<EnemyDMG>();
        if (dMG == null) return;
        healthTEMP -= dMG.DMG;
        if (dMG.grantMana)
        {
            player.GetComponent<playerWalk>().mana += 20;
        }
    }

    void Cast(Quaternion dir)
    {
        Instantiate(fireball, transform.position + Vector3.up + mult(Vector3.forward, dir.eulerAngles) * 1, dir);
    }

    Vector3 mult(Vector3 vec1, Vector3 vec2)
    {
        return new Vector3(vec1.x * vec2.x, vec1.y * vec2.y, vec1.z * vec2.z);
    }
}
