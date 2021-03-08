using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HotkeyManager
{
    public event EventHandler OnAbilityListChanged;
    public enum AbilityType
    {
        Pickaxe,
        Axe,
        Sword,
        Fists,
        HealthPotion
    }
    private PlayerWeapons userWeapon;
    private List<HotkeyAbility> hotkeyAbilityList;
    public HotkeyManager(PlayerWeapons userWeapon)
    {
        this.userWeapon = userWeapon;
        hotkeyAbilityList = new List<HotkeyAbility>();
        hotkeyAbilityList.Add(new HotkeyAbility { abilityType = AbilityType.Pickaxe, activateAbilityAction = () => userWeapon.SetWeaponType(PlayerWeapons.WeaponType.Pickaxe) });
        hotkeyAbilityList.Add(new HotkeyAbility { abilityType = AbilityType.Axe, activateAbilityAction = () => userWeapon.SetWeaponType(PlayerWeapons.WeaponType.Axe) });
        hotkeyAbilityList.Add(new HotkeyAbility { abilityType = AbilityType.Sword, activateAbilityAction = () => userWeapon.SetWeaponType(PlayerWeapons.WeaponType.Sword) });
        hotkeyAbilityList.Add(new HotkeyAbility { abilityType = AbilityType.Fists, activateAbilityAction = () => userWeapon.SetWeaponType(PlayerWeapons.WeaponType.Fists) });
        hotkeyAbilityList.Add(new HotkeyAbility { abilityType = AbilityType.HealthPotion, activateAbilityAction = () => userWeapon.DrinkHealthPotion() });
    }
    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            hotkeyAbilityList[0].activateAbilityAction();
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            hotkeyAbilityList[1].activateAbilityAction();
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            hotkeyAbilityList[2].activateAbilityAction();
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            hotkeyAbilityList[3].activateAbilityAction();
        }
        if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            hotkeyAbilityList[4].activateAbilityAction();
        }
    }
    public List<HotkeyAbility> GetHotkeyAbilityList()
    {
        return hotkeyAbilityList;
    }
    
    public void SwapAbility(int index1, int index2)
    {
        HotkeyAbility hotkeyAbility = hotkeyAbilityList[index1];
        hotkeyAbilityList[index1] = hotkeyAbilityList[index2];
        hotkeyAbilityList[index2] = hotkeyAbility;
        OnAbilityListChanged?.Invoke(this,EventArgs.Empty);

    }
    public class HotkeyAbility
    {
        public AbilityType abilityType;
        public Action activateAbilityAction;

        public Sprite GetSprite()
        {
            switch (abilityType)
            {
                default:
                case AbilityType.Fists: return UIManager.Instance.fistsSprite;
                case AbilityType.Pickaxe: return UIManager.Instance.pickaxeSprite;
                case AbilityType.Axe: return UIManager.Instance.axeSprite;
                case AbilityType.Sword: return UIManager.Instance.swordSprite;
                case AbilityType.HealthPotion: return UIManager.Instance.healthPotionSprite;
            }
        }
    }
}
