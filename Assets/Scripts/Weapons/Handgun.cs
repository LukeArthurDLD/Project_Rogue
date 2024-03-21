using UnityEngine;

public class Handgun : Weapon
{
    public override void OnAttack(Transform weaponOrigin, Transform rayOrigin)
    {
        Debug.Log("fired shot");
    }
}
