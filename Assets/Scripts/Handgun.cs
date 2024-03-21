using UnityEngine;

public class Handgun : Weapon
{
    public override void OnAttack()
    {
        Debug.Log("fired shot");
    }
}
