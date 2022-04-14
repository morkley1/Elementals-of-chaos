using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawn : MonoBehaviour
{
    public Transform spawnPos;
    public GameObject spawnee;
    public GameObject spawnee2;
    public int chance;
    public int AltChance;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Random.Range(1, chance) == 1)
        {
            if (Random.Range(1, AltChance) == 1)
            {
                Instantiate(spawnee2, spawnPos.position, spawnPos.rotation);
            }
            else
            {
                Instantiate(spawnee, spawnPos.position, spawnPos.rotation);
            }
        }
    }
}
