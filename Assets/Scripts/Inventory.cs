using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    List<GameObject> gameObjects = new List<GameObject>{};
    public List<InventoryItemData> inventoryItems = new List<InventoryItemData>{};
    [SerializeField] float spacing = 100;
    [SerializeField] Transform thingRenderer;
    [SerializeField] GameObject image;
    [SerializeField] List<InventoryItemData> crafts;
    public List<InventoryItemData> potions = new List<InventoryItemData>{};

    // Update is called once per frame
    void Update()
    {
        Render();
        if (Input.GetKeyDown(KeyCode.C))
        {
            foreach (InventoryItemData craft in crafts)
            {
                craft.Craft();
            }
        }
        if (Input.GetKeyDown(KeyCode.Q))
        {
            foreach (InventoryItemData potion in potions)
            {
                if (inventoryItems.Contains(potion))
                {
                    inventoryItems[inventoryItems.IndexOf(potion)].use();
                    inventoryItems.RemoveAt(inventoryItems.IndexOf(potion));
                }
            }
        }
    }

    void Render()
    {
        foreach (GameObject gameObject in gameObjects)
        {
            Destroy(gameObject);
        }
        gameObjects.Clear();
        int index = 0;
        int yindex = 0;
        foreach (InventoryItemData item in inventoryItems)
        {
            item.Update();
            if (index > 9)
            {
                index -= 10;
                yindex++;
            }
            Image temp = Instantiate(image, thingRenderer.position + Vector3.down * index * spacing * Screen.width / 1066.667f + Vector3.right * yindex * spacing * Screen.height / 600f, new Quaternion(), thingRenderer).GetComponent<Image>();
            temp.sprite = item.icon;
            gameObjects.Add(temp.gameObject);
            index++;
        }
    }
}
