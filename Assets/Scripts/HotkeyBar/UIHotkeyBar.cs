using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class UIHotkeyBar : MonoBehaviour
{
    //getting the reference
    private Transform abilitySlotTemplate;
    private HotkeyManager hotkeyManager;

    private void Awake()
    {
        abilitySlotTemplate = transform.Find("AbilitySlotTemplate");
        abilitySlotTemplate.gameObject.SetActive(false);
    }

    public void SetHotkeyBar(HotkeyManager hotkeyManager)
    {
        this.hotkeyManager = hotkeyManager;
        hotkeyManager.OnAbilityListChanged += HotkeyManager_OnAbilityListChanged;
        UpdateVisual();
    }

    private void HotkeyManager_OnAbilityListChanged(object sender, EventArgs e)
    {
        UpdateVisual();
    }

    private void UpdateVisual()
    {
        //clearing old objects
        foreach(Transform child in transform)
        {
            if (child == abilitySlotTemplate) continue;//Don't destroy the Template
            Destroy(child.gameObject);
        }
        List<HotkeyManager.HotkeyAbility> hotkeyAbilityList = hotkeyManager.GetHotkeyAbilityList();
        for (int i= 0; i < hotkeyAbilityList.Count; i++)
        {
            HotkeyManager.HotkeyAbility hotkeyAbility = hotkeyAbilityList[i];
            Transform abilitySlotTransform = Instantiate(abilitySlotTemplate, transform);
            abilitySlotTransform.gameObject.SetActive(true);
            RectTransform abilitySlotRectTransform= abilitySlotTransform.GetComponent<RectTransform>();
            abilitySlotRectTransform.anchoredPosition = new Vector2(150f * i, 0f);
            abilitySlotTransform.Find("itemImage").GetComponent<Image>().sprite = hotkeyAbility.GetSprite();
            abilitySlotTransform.Find("bindText").GetComponent<TextMeshProUGUI>().SetText((i+1).ToString());

            abilitySlotTransform.GetComponent<HotkeyBarAbilitySlot>().Setup(hotkeyManager, i, hotkeyAbility);

        }
    }

}
