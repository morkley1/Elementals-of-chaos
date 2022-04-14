using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.VisualScripting;

public class HandleDMG : MonoBehaviour
{
    public Variables Variables;

    public float health = 20;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (health <= 0)
        {
            Destroy(this.gameObject);
        }
    }

    // OnCollisionEnter is called when this collider/rigidbody has begun touching another rigidbody/collider
    private void OnCollisionEnter(Collision collision)
    {
        GameObject sword = (GameObject)Variables.Scene(this.gameObject.scene).Get("Sword");
        GameObject blast = (GameObject)Variables.Scene(this.gameObject.scene).Get("Blast");
        if (collision.gameObject == sword)
        {
            GameObject player = (GameObject)Variables.Scene(this.gameObject.scene).Get("Player");
            health -= 10;
            player.GetComponent<playerWalk>().mana += 5;
            if (player.GetComponent<playerWalk>().mana > player.GetComponent<playerWalk>().maxMana)
            {
                player.GetComponent<playerWalk>().mana = player.GetComponent<playerWalk>().maxMana;
            }
        }
        else if (collision.gameObject == blast)
        {
            health -= 5;
        }
    }

}
