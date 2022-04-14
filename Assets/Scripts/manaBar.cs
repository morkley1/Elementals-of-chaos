using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class manaBar : MonoBehaviour
{
    public Slider ManaBar;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        ManaBar.value = GameObject.Find("First person player").GetComponent<playerWalk>().mana;
        ManaBar.maxValue = GameObject.Find("First person player").GetComponent<playerWalk>().maxMana;
        GameObject.Find("First person player").GetComponent<playerWalk>().mana = ManaBar.value;
    }
}
