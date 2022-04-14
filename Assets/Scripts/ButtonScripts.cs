using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using System.IO;

public class ButtonScripts : MonoBehaviour
{
    GameObject self;

    public GameObject lvlup;
    public TMP_Dropdown save;

    public string slot;
    void OnDisable()
    {
        PlayerPrefs.SetString("Slot", slot);
    }
    public virtual void Start()
    {
        self = gameObject;
    }
    private void Update()
    {
        if(save != null)
        {
            slot = ""+(save.value+1);
        }
    }
    public virtual void StartGame()
    {
        SceneManager.LoadScene("Demo");
    }
    public virtual void Close()
    {
        Application.Quit();
    }
    public virtual void Hide(bool doHide)
    {
        self.SetActive(doHide);
    }
    public virtual void Dash()
    {
        GameObject.Find("First person player").GetComponent<playerWalk>().dashUnlocked = true;
        lvlup.SetActive(false);
        Time.timeScale = 1f;
        Cursor.lockState = CursorLockMode.Locked;
    }
    public virtual void Jumps()
    {
        GameObject.Find("First person player").GetComponent<playerWalk>().maxJumps++;
        lvlup.SetActive(false);
        Time.timeScale = 1f;
        Cursor.lockState = CursorLockMode.Locked;
    }
    public virtual void Del()
    {
        File.Delete(Application.dataPath.Replace("/Assets", "") + "/save" + slot + ".json");
        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.name);
    }
}
