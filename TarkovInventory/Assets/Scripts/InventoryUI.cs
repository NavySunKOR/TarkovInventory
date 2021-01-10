using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    public GameObject emptyGrid;
    public float gridWidth;
    public float gridHeight;

    public Item sampleItem1;
    public Item sampleItem2;

    private Inventory inventory;



    private void Awake()
    {
        inventory = new Inventory();
        inventory.heightGridCount = 5;
        inventory.widthGridCount = 6;
        inventory.ResetInventory();
        DrawInitInventory();
        inventory.AddItem(sampleItem1);
        inventory.AddItem(sampleItem2);




    }

    private void DrawInitInventory()
    {
        for (int x = 0; x < inventory.widthGridCount; x++)
        {
            for (int y = 0; y < inventory.heightGridCount; y++)
            {
                GameObject gb = Instantiate(emptyGrid, transform);
                gb.transform.localPosition = new Vector2(x * gridWidth - gridWidth, y * gridHeight - gridHeight / 2f);
            }
        }
    }

    private void UpdateInventoryUI()
    {
        for(int i = 0; i< inventory.items.Count;i++)
        {
            
        }
    }

}
