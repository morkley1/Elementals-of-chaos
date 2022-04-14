using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Shop : MonoBehaviour
{
    public string shopName = "New Shop";
    public List<string> stock = new List<string>{};
    public List<int> stockAmount = new List<int>{};
    public List<int> costs = new List<int>{};
    public Dictionary<string, KeyValuePair<int, int>> items = new Dictionary<string, KeyValuePair<int, int>>(){};
    public GameObject display;
    public GameObject text;
    public PauseMenu pause;

    bool isOpen = false;

    // Start is called before the first frame update
    void Start()
    {
        int index = 0;
        foreach (string item in stock)
        {
            items.Add(item, new KeyValuePair<int, int>(stockAmount[index], costs[index]));
            index++;
        }
        gameObject.SetActive(false);
        isOpen = false;
        index = 0;
        foreach (KeyValuePair<string, KeyValuePair<int, int>> item in items)
        {
            GameObject disp = Instantiate(display, text.transform.position, new Quaternion(0, 0, 0, 0), text.transform);
            disp.transform.position += new Vector3(0, -80 * (index + 1), 0);
            TextMeshProUGUI[] texts = disp.GetComponentsInChildren<TextMeshProUGUI>();
            texts[0].text = item.Key;
            texts[1].text = "" + item.Value.Key;
            texts[2].text = "" + item.Value.Value;
            index++;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (isOpen && Input.GetButtonDown("Cancel"))
        {
            gameObject.SetActive(false);
            isOpen = false;
            pause.Resume();
            Cursor.lockState = CursorLockMode.Locked;
        }
    }

    public void open()
    {
        gameObject.SetActive(true);
        isOpen = true;
    }

    IEnumerator Unlock()
    {
        yield return null;
        Cursor.lockState = CursorLockMode.Locked;
    }
}
