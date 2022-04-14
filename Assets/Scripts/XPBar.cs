using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class XPBar : MonoBehaviour
{
    GameObject player;

    public GameObject lvlup;
    public int level;
    public Slider bar;
    public string slot;

    void OnEnable()
    {
        slot = PlayerPrefs.GetString("Slot");
    }
    // Start is called before the first frame update
    void Start()
    {
        if (GameObject.Find("First person player") != null)
        {
            player = GameObject.Find("First person player");
        }
        bar = this.GetComponent<Slider>();
        bar.value = 0;
        bar.maxValue = 100;
        level = 1;
        if (File.Exists(Application.dataPath.Replace("/Assets", "") + "/save" + slot + ".json"))
        {
            string sav = File.ReadAllText(Application.dataPath.Replace("/Assets", "") + "/save" + slot + ".json");
            Save save = JsonUtility.FromJson<Save>(sav);
            level = save.xpMax;
            if (level > 0)
            {

            }
            else
            {
                if (player.GetComponent<playerWalk>().dashUnlocked)
                {
                    level = player.GetComponent<playerWalk>().maxJumps + 1;
                }
                else
                {
                    level = player.GetComponent<playerWalk>().maxJumps;
                }
            }
            bar.maxValue = 100 * (level * level);
            bar.value = save.xp;
        }
        else
        {
            bar.value = 0;
            bar.maxValue = 100;
        }
    }

    // Update is called once per frame
    [System.Obsolete]
    void Update()
    {
        if (bar.value >= bar.maxValue)
        {
            player.GetComponent<playerWalk>().maxMana += 25;
            player.GetComponent<playerWalk>().maxHealthTEMP += 10;
            player.GetComponent<playerWalk>().mana = player.GetComponent<playerWalk>().maxMana;
            player.GetComponent<playerWalk>().healthTEMP = player.GetComponent<playerWalk>().maxHealthTEMP;
            bar.value -= bar.maxValue;
            bar.maxValue = bar.maxValue * 2;
            Time.timeScale = 0f;
            Cursor.lockState = CursorLockMode.None;
            lvlup.active = true;
        }
    }
}
