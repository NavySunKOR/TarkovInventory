using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{
    public GameObject emptyGrid;
    public GameObject itemGrid;
    public float gridWidth;
    public float gridHeight;

    public Item sampleItem1;
    public Item sampleItem2;

    [SerializeField]
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

    private void Start()
    {
        UpdateInventoryUI();
    }

    private void DrawInitInventory()
    {
        for (int x = 0; x < inventory.widthGridCount; x++)
        {
            for (int y = 0; y < inventory.heightGridCount; y++)
            {
                GameObject gb = Instantiate(emptyGrid, transform);
                gb.transform.localPosition = new Vector2(x * gridWidth - ((gridWidth * inventory.widthGridCount)/2) + (gridWidth/2), y * gridHeight - ((gridHeight*inventory.heightGridCount)/2) + (gridHeight/2));
            }
        }
    }

    private void UpdateInventoryUI()
    {
        for(int i = 0; i< inventory.items.Count;i++)
        {
            GameObject gb = Instantiate(itemGrid, transform);
            gb.GetComponent<Image>().sprite = inventory.items[i].spriteIcon;
            gb.transform.localScale = new Vector2(inventory.items[i].sizeX, inventory.items[i].sizeY);
            gb.transform.localPosition = new Vector2(inventory.items[i].startPosX * gridWidth - ((gridWidth * inventory.widthGridCount) / 2) + ((gridWidth * inventory.items[i].sizeX )/ 2)
                ,
                ((gridHeight * inventory.heightGridCount) - ((gridHeight * inventory.items[i].sizeY))) // 아이템 시작 위치를 위로 한다면
                -
                inventory.items[i].startPosY * gridHeight - ((gridHeight * inventory.heightGridCount) / 2) + ((gridHeight * inventory.items[i].sizeY) / 2));

        }
    }

}
