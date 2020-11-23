using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


//Used help from Redefine Gamer on Youtube// for structure and input handling


public class WireScript : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    private Image wireImage;
    private LineRenderer lr;
    private Canvas canv;
    private bool started = false;
    public bool leftWire = false;
    public Color selectedColor;
    private WireTask wt;
    public bool completed = false;

    private void Awake()
    {
        wireImage = GetComponent<Image>();
        lr = GetComponent<LineRenderer>();
        canv = GetComponentInParent<Canvas>();
        wt = GetComponentInParent<WireTask>();
    }

    private void Update()
    {
        if (started)
        {
            Vector2 movePos;
            Vector2 offset = new Vector2(transform.position.x-1, transform.position.y);
            RectTransformUtility.ScreenPointToLocalPointInRectangle(
                canv.transform as RectTransform,
                Input.mousePosition,
                canv.worldCamera,
                out movePos);
            lr.SetPosition(0, offset);
            lr.SetPosition(1, canv.transform.TransformPoint(movePos));
        }
        else
        {
            if (!completed)
            {
                lr.SetPosition(0, Vector3.zero);
                lr.SetPosition(1, Vector3.zero);
            }
        }

        bool hovered = RectTransformUtility.RectangleContainsScreenPoint(
            transform as RectTransform, 
            Input.mousePosition,
            canv.worldCamera);
        if (hovered)
        {
            wt.CurrentHoveredWire = this;
        }
    }

    public void SetColor(Color color)
    {
        wireImage.color = color;
        lr.startColor = color;
        lr.endColor = color;
        selectedColor = color;
    }


    public void OnDrag(PointerEventData eventData)
    {

    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (leftWire && !completed)
        {
            started = true;
            wt.CurrentDraggedWire = this;
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if(wt.CurrentHoveredWire != null)
        {
            if(wt.CurrentHoveredWire.selectedColor == selectedColor &&
                !wt.CurrentHoveredWire.leftWire)
            {
                completed = true;
                wt.CurrentHoveredWire.completed = true;
            }
        }
        started = false;
        wt.CurrentDraggedWire = null;
    }
}