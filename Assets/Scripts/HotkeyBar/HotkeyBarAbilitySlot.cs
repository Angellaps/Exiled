using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class HotkeyBarAbilitySlot : MonoBehaviour, IPointerDownHandler, IDragHandler ,IDropHandler, IBeginDragHandler ,IEndDragHandler
{
    private HotkeyManager.HotkeyAbility hotkeyAbility;
    private HotkeyManager hotkeyManager;
    private int abilityIndex;
    private RectTransform rectTransform;
    private CanvasGroup canvasGroup;
    private Canvas canvas;
    private Vector2 startingPos;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
        //automatically grabs Canvas
        Transform testCanvasTransform=transform;
        do
        {
            testCanvasTransform = testCanvasTransform.parent; //checking every parent till we find a canvas
            canvas = testCanvasTransform.GetComponent<Canvas>();
        } while (canvas == null);
    }

    private void Start()
    {
        startingPos = rectTransform.anchoredPosition;
    }
    
    public int GetAbilityIndex()
    {
        return abilityIndex;
    }
    public void Setup(HotkeyManager hotkeyManager, int abilityIndex,HotkeyManager.HotkeyAbility hotkeyAbility)
    {
        this.hotkeyManager = hotkeyManager;
        this.abilityIndex = abilityIndex;
        this.hotkeyAbility = hotkeyAbility;
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        hotkeyAbility.activateAbilityAction();
    }

    public void OnDrag(PointerEventData eventData)
    {
        rectTransform.anchoredPosition += eventData.delta /canvas.scaleFactor;
    }

    public void OnDrop(PointerEventData eventData)
    {
       if (eventData.pointerDrag != null)//dragging
        {
            HotkeyBarAbilitySlot hotkeyBarAbilitySlot= eventData.pointerDrag.GetComponent<HotkeyBarAbilitySlot>();
            if(hotkeyBarAbilitySlot != null)
            {
                //if it contains an ability slot
                hotkeyManager.SwapAbility(abilityIndex, hotkeyBarAbilitySlot.GetAbilityIndex());
                //when dragging and dropping on an ability slot it, the dragged ability drops on itself, editor canvas group addition + onBeginDrag-onEndDrag
            }
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        rectTransform.anchoredPosition = startingPos;
        canvasGroup.alpha = 1f;
        canvasGroup.blocksRaycasts = true;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        canvasGroup.alpha = 0.6f;
        canvasGroup.blocksRaycasts = false;
        transform.SetAsLastSibling();//in order to appear on top when dragging
    }
}
