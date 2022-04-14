using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class jumpBar : MonoBehaviour
{
    public Image self;
    public int num;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (GameObject.Find("First person player").GetComponent<playerWalk>().jumps >= num)
        {
            self.color = Color.blue;
        }
        else
        {
            self.color = Color.red;
        }
        gameObject.SetActive(GameObject.Find("First person player").GetComponent<playerWalk>().maxJumps >= num);
    }
}
