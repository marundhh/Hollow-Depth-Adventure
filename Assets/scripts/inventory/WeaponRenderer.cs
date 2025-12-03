using UnityEngine;

public class WeaponRenderer : MonoBehaviour
{
    public Transform weaponHolder;
    private GameObject currentWeaponObject;

    public void EquipWeapon(ItemData weaponData)
    {
        UnequipWeapon();

        currentWeaponObject = new GameObject("EquippedWeapon");
        SpriteRenderer renderer = currentWeaponObject.AddComponent<SpriteRenderer>();
        renderer.sprite = weaponData.icon;
        renderer.sortingOrder = 20;

        currentWeaponObject.transform.SetParent(weaponHolder);
        currentWeaponObject.transform.localPosition = Vector3.zero;
        currentWeaponObject.transform.localScale = Vector3.one;
    }

    public void UnequipWeapon()
    {
        if (currentWeaponObject != null)
            Destroy(currentWeaponObject);
    }
}
