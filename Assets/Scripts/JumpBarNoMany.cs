using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpBarNoMany : MonoBehaviour
{
    public GameObject image;
    public GameObject image2;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        int i = 0;
        while (i < GameObject.Find("First person player").GetComponent<playerWalk>().jumps)
        {
            Instantiate(image, new Vector3(i * 125, 0, 0) + transform.position, new Quaternion(0, 0, 0, 0), transform);
            i++;
        }
        while (i < GameObject.Find("First person player").GetComponent<playerWalk>().maxJumps)
        {
            Instantiate(image2, new Vector3(i * 125, 0, 0) + transform.position, new Quaternion(0, 0, 0, 0), transform);
            i++;
        }
    }
}
