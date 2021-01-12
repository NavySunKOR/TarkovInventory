using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InventoryItemGrid : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public Item itemInfo;
    public InventoryUI sourceInventory;
    public float totalGridWidthPx;
    public float totalGridHeightPx;
    public float itemSizeGridWidthPx;
    public float itemSizeGridHeightPx;
    public float startPosXPx;
    public float startPosYPx;
    
    private Vector2 originPos;
    private Coroutine coMouse;



    public void OnPointerEnter(PointerEventData eventData)
    {
        originPos = transform.position;
        coMouse = StartCoroutine(Co_CheckMousePos());
    }

    IEnumerator Co_CheckMousePos()
    {
        while(true)
        {
            if(Input.GetMouseButton(0))
            {
                transform.position = Input.mousePosition;
            }
            else if(Input.GetMouseButtonUp(0))
            {
                //아이템이 바깥에 있는가?
                
                //아이템의 4면 끝이 전부 인


                if(!sourceInventory.actualRect.Contains(transform.position))
                {
                    Debug.Log(sourceInventory.actualRect.position + " / " +sourceInventory.actualRect + " / " + transform.position);
                    Debug.Log("Yes drop the item");
                    transform.position = originPos;

                }
                //아이템이 아이템 창 안에 있으면 아이템을 옮길 수 있는가?
                else if (sourceInventory.CanMoveItem(itemInfo, transform.position))
                {
                    Debug.Log("Yes can transfer");


                    transform.position = originPos;
                }
                else
                {
                    //아니면 원래대로 돌아간다.
                    transform.position = originPos;
                }
            }
            yield return null;
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        transform.position = originPos;
        StopCoroutine(coMouse);
    }
}
