using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Item", fileName = "new item")]
public class InventoryItemData : ScriptableObject
{
    public string displayName;
    public itemType type = itemType.craftingMaterial;
    public Sprite icon;
    public GameObject prefab;
    public float DMGOverride;
    public float RDMGOverride;
    public float healing;
    public List<InventoryItemData> craftingRecipe = new List<InventoryItemData>{};

    public InventoryItemData Craft()
    {
        if (craftingRecipe.Equals(new List<InventoryItemData>{})) return null;
        if (containsAll(FindObjectOfType<Inventory>().inventoryItems, craftingRecipe))
        {
            foreach (InventoryItemData itemData in craftingRecipe) FindObjectOfType<Inventory>().inventoryItems.Remove(itemData);
            FindObjectOfType<Inventory>().inventoryItems.Add(this);
            return this;
        }
        return null;
    }

    // This function is called when the script is started
    public void Update()
    {
        if (type == itemType.weapon) GameObject.Find("DMG").GetComponent<EnemyDMG>().DMG = DMGOverride;
        if (type == itemType.spell) GameObject.Find("RDMG").GetComponent<EnemyDMG>().DMG = RDMGOverride;
    }

    public void use()
    {
        if (type == itemType.consumable)
        {
            GameObject.Find("First person player").GetComponent<playerWalk>().healthTEMP += healing;
        }
    }

    public override bool Equals(object obj)
    {
        return obj is InventoryItemData data &&
               base.Equals(obj) &&
               EqualityComparer<GameObject>.Default.Equals(prefab, data.prefab);
    }

    public override int GetHashCode()
    {
        int hashCode = 660907545;
        hashCode = hashCode * -1521134295 + base.GetHashCode();
        hashCode = hashCode * -1521134295 + EqualityComparer<GameObject>.Default.GetHashCode(prefab);
        return hashCode;
    }

    public bool containsAll<T>(List<T> list, List<T> contains)
    {
        bool output = true;
        List<T> subList = new List<T>{};
        foreach (T item in list)
        {
            subList.Add(item);
        }
        foreach (T item in contains)
        {
            if (!subList.Contains(item)) output = false;
            subList.Remove(item);
        }
        return output;
    }
}
public enum itemType
{
    weapon,
    spell,
    craftingMaterial,
    consumable
}