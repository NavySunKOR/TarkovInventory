using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Item
{
    public Sprite spriteIcon;
    public string itemLabel;
    public int id;
    public int itemCode;
    public int sizeX;
    public int sizeY;
    public int startPosX;
    public int startPosY;
    public bool isTranspose;
}

[System.Serializable]
public class Inventory
{
    public int widthGridCount;
    public int heightGridCount;
    public List<Item> items;
    public int[,] grids;

    private int itemSaveStartX;
    private int itemSaveStartY;


    // Start is called before the first frame update
    public Inventory()
    {
        items = new List<Item>();
    }

    public void ResetInventory()
    {
        grids = new int[widthGridCount, heightGridCount];
        for (int x = 0; x < grids.GetLength(0); x++)
        {
            for (int y = 0; y < grids.GetLength(1); y++)
            {
                grids[x, y] = 0;
            }
        }
    }

    private bool IsItemCodeExist(int pItemCode)
    {
        for (int i = 0; i < items.Count; i++)
        {
            if (items[i].itemCode.Equals(pItemCode))
            {
                return true;
            }
        }

        return false;
    }

    public Item AddItem(Item pItem)
    {

        if (HasEmptySpace(pItem))
        {
            pItem.startPosX = itemSaveStartX;
            pItem.startPosY = itemSaveStartY;

            int generatedId = Random.Range(1, 9999);
            while (IsItemCodeExist(generatedId))
            {
                generatedId = Random.Range(1, 9999);
            }
            pItem.id = generatedId;

            for (int x = itemSaveStartX; x < itemSaveStartX + pItem.sizeX; x++)
            {
                for (int y = itemSaveStartY; y < itemSaveStartY + pItem.sizeY; y++)
                {
                    grids[x, y] = pItem.id;
                }
            }

      

            items.Add(pItem);

            return pItem;
        }
        else
        {
            return null;
        }
    }

    public void RemoveItem(Item pItem)
    {
        for(int i = 0; i < items.Count; i++)
        {
            if(pItem.id.Equals(items[i].id))
            {
                items.RemoveAt(i);
                break;
            }
        }

        for(int x = 0; x < grids.GetLength(0);x++)
        {
            for(int y= 0; y < grids.GetLength(1); y++)
            {
                if(grids[x,y] == pItem.id)
                {
                    grids[x ,y] = 0;
                }
            }
        }
    }


    private bool HasEmptySpace(Item pItem)
    {
        for (int x = 0; x < grids.GetLength(0); x++)
        {
            for (int y = 0; y < grids.GetLength(1); y++)
            {
                if (grids[x, y] == 0)
                {
                    if (SearchArea(pItem, x, y))
                    {
                        itemSaveStartX = x;
                        itemSaveStartY = y;
                        return true;
                    }
                }
            }
        }
        return false;
    }


    private bool SearchArea(Item pItem, int startX, int startY)
    {
        int spaceCount = 0;
        for (int x = startX; x < startX + pItem.sizeX; x++)
        {
            if (x <= grids.GetLength(0) - 1)
            {
                for (int y = startY; y < startY + pItem.sizeY; y++)
                {
                    if (y <= grids.GetLength(1) - 1 && grids[x, y] == 0)
                    {
                        spaceCount++;
                    }
                    else if (y > grids.GetLength(1) - 1)
                    {
                        break;
                    }
                }
            }
            else
            {
                break;
            }
        }

        if (spaceCount == pItem.sizeX * pItem.sizeY)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

}
