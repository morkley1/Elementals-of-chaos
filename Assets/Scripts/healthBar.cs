using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class healthBar : MonoBehaviour
{
    public Slider HealthBar;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        HealthBar.value = GameObject.Find("First person player").GetComponent<playerWalk>().healthTEMP;
        HealthBar.maxValue = GameObject.Find("First person player").GetComponent<playerWalk>().maxHealthTEMP;
    }
}
