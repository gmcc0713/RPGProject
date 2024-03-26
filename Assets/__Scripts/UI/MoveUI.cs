using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MoveUI : MonoBehaviour, IBeginDragHandler, IDragHandler
{
    [SerializeField] GameObject movePanel;
    private Vector2 moveOffset;
    public void OnBeginDrag(PointerEventData eventData)
    {
        if (eventData.button.Equals(PointerEventData.InputButton.Left))
        {
                moveOffset.x = movePanel.transform.position.x - eventData.position.x;
                moveOffset.y = movePanel.transform.position.y - eventData.position.y;
        }

    }

    public void OnDrag(PointerEventData eventData)
    {
        if (eventData.button.Equals(PointerEventData.InputButton.Left))
        {
            movePanel.transform.position = eventData.position + moveOffset;
        }

    }
}
