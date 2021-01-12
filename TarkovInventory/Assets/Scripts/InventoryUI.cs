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
    public Rect actualRect;

    public Item sampleItem1;
    public Item sampleItem2;

    private List<InventoryItemGrid> itemGridUIs;

    [SerializeField]
    private Inventory inventory;
    private RectTransform rectTransform;



    private void Awake()
    {
        itemGridUIs = new List<InventoryItemGrid>();
        rectTransform = GetComponent<RectTransform>();
        actualRect = new Rect();
        actualRect.x = transform.position.x - (rectTransform.rect.width / 2);
        actualRect.y = transform.position.y - (rectTransform.rect.height / 2);
        actualRect.width = rectTransform.rect.width;
        actualRect.height = rectTransform.rect.height;
        inventory = new Inventory();
        inventory.heightGridCount = 5;
        inventory.widthGridCount = 6;
        inventory.ResetInventory();
        DrawInitInventoryUI();
        AddItemUI(sampleItem1);
        AddItemUI(sampleItem2);


    }

    private void DrawInitInventoryUI()
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

    public void AddItemUI(Item pItem)
    {
        pItem = inventory.AddItem(pItem);
        if(pItem != null)
        {
            GameObject gb = Instantiate(itemGrid, transform);
            InventoryItemGrid inventoryGrid = gb.GetComponent<InventoryItemGrid>();
            inventoryGrid.itemInfo = pItem;
            inventoryGrid.sourceInventory = this;
            inventoryGrid.totalGridWidthPx =  inventory.widthGridCount * gridWidth; //1칸당 그리드 픽셀 * 인벤토리의 가로 그리드 칸 수 
            inventoryGrid.totalGridHeightPx = inventory.heightGridCount * gridHeight; //1칸당 그리드 픽셀 * 인벤토리의 세로 그리드 칸 수 
            inventoryGrid.itemSizeGridWidthPx =  pItem.sizeX * gridWidth; //1칸당 그리드 픽셀 *  아이템의 가로 그리드 칸 수
            inventoryGrid.itemSizeGridHeightPx =  pItem.sizeY * gridHeight; //1칸당 그리드 픽셀 *  아이템의 세로 그리드 칸 수
            inventoryGrid.startPosXPx = pItem.startPosX * gridWidth; // 아이템의 x 그리드 칸 위치 * 1칸당 그리드 픽셀 
            inventoryGrid.startPosYPx = pItem.startPosY * gridHeight; // 아이템의 y 그리드 칸 위치 * 1칸당 그리드 픽셀 
            gb.GetComponent<Image>().sprite = pItem.spriteIcon;
            gb.transform.localScale = new Vector2(pItem.sizeX, pItem.sizeY);
            gb.transform.localPosition = new Vector2(inventoryGrid.startPosXPx - (inventoryGrid.totalGridWidthPx / 2) + (inventoryGrid.itemSizeGridWidthPx / 2)
                ,
                (inventoryGrid.totalGridHeightPx - inventoryGrid.itemSizeGridHeightPx) // 아이템 시작 위치를 위로 한다면
                - inventoryGrid.startPosYPx - (inventoryGrid.totalGridHeightPx / 2) + (inventoryGrid.itemSizeGridHeightPx / 2)); // 0,0 은 정확히 중앙이기에 땡겨줘야함.
          
            itemGridUIs.Add(gb.GetComponent<InventoryItemGrid>());
        }
       
    }

    public bool CanMoveItem(Item pItem,Vector2 pMousePosition)
    {
        Rect compareRect = new Rect();
        Vector2 rectPos1 = new Vector2(pMousePosition.x - ((pItem.sizeX * gridWidth)/2), pMousePosition.y - ((pItem.sizeY*gridHeight) /2));
        Vector2 rectPos2 = new Vector2(pMousePosition.x + ((pItem.sizeX * gridWidth) / 2), pMousePosition.y - ((pItem.sizeY * gridHeight) / 2));
        Vector2 rectPos3 = new Vector2(pMousePosition.x - ((pItem.sizeX * gridWidth) / 2), pMousePosition.y + ((pItem.sizeX * gridWidth) / 2));
        Vector2 rectPos4 = new Vector2(pMousePosition.x + ((pItem.sizeX * gridWidth) / 2), pMousePosition.y + ((pItem.sizeX * gridWidth) / 2));
        for(int i = 0; i < itemGridUIs.Count;i++)
        {
            compareRect.x = itemGridUIs[i].startPosXPx - (itemGridUIs[i].totalGridWidthPx / 2) + (itemGridUIs[i].itemSizeGridWidthPx / 2);
            compareRect.y = (itemGridUIs[i].totalGridHeightPx - itemGridUIs[i].itemSizeGridHeightPx) - itemGridUIs[i].startPosYPx - (itemGridUIs[i].totalGridHeightPx / 2) + (itemGridUIs[i].itemSizeGridHeightPx / 2);
            compareRect.width = itemGridUIs[i].itemSizeGridWidthPx;
            compareRect.height = itemGridUIs[i].itemSizeGridHeightPx;

            if(compareRect.Contains(rectPos1) || compareRect.Contains(rectPos2) || compareRect.Contains(rectPos3) || compareRect.Contains(rectPos4))
            {
                return false;
            }
        }

        return true;
    }

}
