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

    [SerializeField]
    private Inventory inventory;
    private RectTransform rectTransform;



    private void Awake()
    {
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

            float totalGridWidthPx = gridWidth * inventory.widthGridCount;//1칸당 그리드 픽셀 * 인벤토리의 가로 그리드 칸 수 
            float totalGridHeightPx = gridHeight * inventory.heightGridCount; //1칸당 그리드 픽셀 * 인벤토리의 세로 그리드 칸 수 
            float itemSizeGridWidthPx = gridWidth * inventory.items[i].sizeX; //1칸당 그리드 픽셀 *  아이템의 가로 그리드 칸 수
            float itemSizeGridHeightPx = gridHeight * inventory.items[i].sizeY; //1칸당 그리드 픽셀 *  아이템의 세로 그리드 칸 수
            float startPosXPx = inventory.items[i].startPosX * gridWidth;// 아이템의 x 그리드 칸 위치 * 1칸당 그리드 픽셀 
            float startPosYPx = inventory.items[i].startPosY * gridHeight; // 아이템의 y 그리드 칸 위치 * 1칸당 그리드 픽셀 

            gb.GetComponent<Image>().sprite = inventory.items[i].spriteIcon;
            gb.transform.localScale = new Vector2(inventory.items[i].sizeX, inventory.items[i].sizeY);
            gb.transform.localPosition = new Vector2(startPosXPx - (totalGridWidthPx / 2) + (itemSizeGridWidthPx / 2)
                ,
                (totalGridHeightPx - itemSizeGridHeightPx) // 아이템 시작 위치를 위로 한다면
                - startPosYPx  - (totalGridHeightPx / 2) + (itemSizeGridHeightPx / 2)); // 0,0 은 정확히 중앙이기에 땡겨줘야함.
            gb.GetComponent<InventoryItemGrid>().itemInfo = inventory.items[i];
            gb.GetComponent<InventoryItemGrid>().sourceInventory = this;
        }
    }

    public bool CanTransferToItem(Item pItem,Vector2 pMousePosition)
    {
        Rect compareRect = new Rect();
        Vector2 rectPos1 = new Vector2(pMousePosition.x - ((pItem.sizeX * gridWidth)/2), pMousePosition.y - ((pItem.sizeY*gridHeight) /2));
        Vector2 rectPos2 = new Vector2(pMousePosition.x + ((pItem.sizeX * gridWidth) / 2), pMousePosition.y - ((pItem.sizeY * gridHeight) / 2));
        Vector2 rectPos3 = new Vector2(pMousePosition.x - ((pItem.sizeX * gridWidth) / 2), pMousePosition.y + ((pItem.sizeX * gridWidth) / 2));
        Vector2 rectPos4 = new Vector2(pMousePosition.x + ((pItem.sizeX * gridWidth) / 2), pMousePosition.y + ((pItem.sizeX * gridWidth) / 2));
        for(int i = 0; i < inventory.items.Count;i++)
        {
            float totalGridWidthPx = gridWidth * inventory.widthGridCount;//1칸당 그리드 픽셀 * 인벤토리의 가로 그리드 칸 수 
            float totalGridHeightPx = gridHeight * inventory.heightGridCount; //1칸당 그리드 픽셀 * 인벤토리의 세로 그리드 칸 수 
            float itemSizeGridWidthPx = gridWidth * inventory.items[i].sizeX; //1칸당 그리드 픽셀 *  아이템의 가로 그리드 칸 수
            float itemSizeGridHeightPx = gridHeight * inventory.items[i].sizeY; //1칸당 그리드 픽셀 *  아이템의 세로 그리드 칸 수
            float startPosXPx = inventory.items[i].startPosX * gridWidth;// 아이템의 x 그리드 칸 위치 * 1칸당 그리드 픽셀 
            float startPosYPx = inventory.items[i].startPosY * gridHeight; // 아이템의 y 그리드 칸 위치 * 1칸당 그리드 픽셀 

            compareRect.x = startPosXPx - (totalGridWidthPx / 2) + (itemSizeGridWidthPx / 2);
            compareRect.y = (totalGridHeightPx - itemSizeGridHeightPx) - startPosYPx - (totalGridHeightPx / 2) + (itemSizeGridHeightPx / 2);
            compareRect.width = inventory.items[i].sizeX * gridWidth;
            compareRect.height = inventory.items[i].sizeY * gridHeight;

            if(compareRect.Contains(rectPos1) || compareRect.Contains(rectPos2) || compareRect.Contains(rectPos3) || compareRect.Contains(rectPos4))
            {
                return false;
            }

        }

        return true;
    }

}
