using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireballEnemE : MonoBehaviour
{
    public Transform self;
    public float speed;
    public float expans;

    // Start is called before the first frame update
    void Start()
    {

    }
    
    // Update is called once per frame
    void Update()
    {
        transform.position += transform.forward * speed * Time.deltaTime;
    }

    // OnTriggerEnter is called when the Collider other enters the trigger
    private void OnTriggerStay(Collider other)
    {
        if (other.tag != "Enemy")
        {
            explode();
        }
    }

    void explode()
    {
        transform.localScale = Vector3.one * expans;
        timer timer = gameObject.AddComponent<timer>();
        timer.kill = 3;
    }
}
