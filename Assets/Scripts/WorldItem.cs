using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldItem : MonoBehaviour
{
    [SerializeField]InventoryItemData data;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // OnCollisionEnter is called when this collider/rigidbody has begun touching another rigidbody/collider
    public void picup(Inventory inv)
    {
        inv.inventoryItems.Add(data);
        Destroy(gameObject);
    }
}
