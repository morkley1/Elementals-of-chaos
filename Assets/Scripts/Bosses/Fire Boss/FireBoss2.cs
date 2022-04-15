using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBoss2 : Phase
{
    public float castDelay = 0.1f;
    float delay;
    public FireBoss2(float health)
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
        Agent.SetDestination(player.position + ((self.position - player.position).normalized * 15f));
        if (((self.position - player.position).normalized * 10f).magnitude < (self.position - player.position).magnitude && ((self.position - player.position).normalized * 20f).magnitude > (self.position - player.position).magnitude && delay <= 0)
        {
            Vector3 direction = player.transform.position - (self.position + Vector3.up);
            Cast(Quaternion.LookRotation(direction, Vector3.up));
            delay = castDelay;
        }
        delay -= Time.deltaTime;
    }
    void Cast(Quaternion dir)
    {
        MonoBehaviour.Instantiate(fireball, self.position + Vector3.up + mult(Vector3.forward, dir.eulerAngles) * 1, dir);
    }

    Vector3 mult(Vector3 vec1, Vector3 vec2)
    {
        return new Vector3(vec1.x * vec2.x, vec1.y * vec2.y, vec1.z * vec2.z);
    }
}
