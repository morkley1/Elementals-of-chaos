using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    public GameObject[] Obs = { };

    public static bool IsPaused = false;

    public GameObject menu;
    GameObject player;
    public string slot;

    void OnEnable()
    {
        slot = PlayerPrefs.GetString("Slot");
    }
    // Start is called before the first frame update
    public virtual void Start()
    {
        Choos();
        if (GameObject.Find("First person player") != null)
        {
            player = GameObject.Find("First person player");
        }
    }

    // Update is called once per frame
    public virtual void Update()
    {
        if (Input.GetButtonDown("Cancel"))
        {
            if (IsPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }
    public virtual void Pause()
    {
        menu.SetActive(true);
        Time.timeScale = 0f;
        Cursor.lockState = CursorLockMode.None;
        IsPaused = true;
    }
    public virtual void Resume()
    {
        menu.SetActive(false);
        Time.timeScale = 1f;
        Cursor.lockState = CursorLockMode.Locked;
        IsPaused = false;
    }
    public virtual void Close()
    {
        Save Save = new Save();

        Save.Set();

        string json = JsonUtility.ToJson(Save);
        Debug.Log(json);
        File.WriteAllText(Application.dataPath.Replace("/Assets", "") + "/save" + slot + ".json", json);

        SceneManager.LoadScene("Menu");
    }
    public virtual void Choos()
    {
        Pause();
        Resume();
    }
    public virtual void Hide(bool doHide)
    {
        foreach (GameObject Ob in Obs)
        {
            Ob.SendMessage("Hide", doHide);
        }
    }
}
class Save
{
    public Vector3 playerPos;
    public Vector3 playerVel;
    public int playerMaxJumps;
    public float playerMaxHealth;
    public float playerMaxMana;
    public float playerMana;
    public float playerHealth;
    public int playerJumps;
    public bool playerDash;
    public List<InventoryItemData> playerInventory;
    public float xp;
    public int xpMax;
    GameObject player;
    public void Set()
    {
        if (GameObject.Find("First person player") != null)
        {
            player = GameObject.Find("First person player");
        }
        playerPos = player.transform.position;
        playerVel = player.GetComponent<playerWalk>().velocity;
        playerMaxJumps = player.GetComponent<playerWalk>().maxJumps;
        playerJumps = player.GetComponent<playerWalk>().jumps;
        playerMaxHealth = player.GetComponent<playerWalk>().maxHealthTEMP;
        playerMaxMana = player.GetComponent<playerWalk>().maxMana;
        playerMana = player.GetComponent<playerWalk>().mana;
        playerHealth = player.GetComponent<playerWalk>().healthTEMP;
        playerDash = player.GetComponent<playerWalk>().dashUnlocked;
        playerInventory = player.GetComponent<Inventory>().inventoryItems;
        XPBar canvas = Object.FindObjectOfType<XPBar>();
        if (canvas != null)
        {
            GameObject slider = canvas.gameObject;
            Debug.Log(slider);
            Debug.Log(slider.GetComponent<Slider>());
            Debug.Log(slider.GetComponent<XPBar>());
            Debug.Log(slider.GetComponent<Slider>().value);
            Debug.Log(slider.GetComponent<XPBar>().level);
            xp = slider.GetComponent<Slider>().value;
            if (slider.GetComponent<XPBar>().level > 0)
            {
                xpMax = slider.GetComponent<XPBar>().level;
            }
            else
            {
                if (playerDash)
                {
                    xpMax = playerMaxJumps + 1;
                }
                else
                {
                    xpMax = playerMaxJumps;
                }
            }
        }
    }
}