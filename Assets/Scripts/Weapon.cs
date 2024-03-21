using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    public float baseDamage = 5;

    public float fireRate = 2.5f;
    private float nextFire = 0f;

    public bool isAutomatic;

    private bool isAttacking = false;

    private void Update()
    {
        if (isAttacking)
            ProcessAttack();
    }
    public void StartAttack()
    {
        Debug.Log("start");
        isAttacking = true;
    }
    public void EndAttack()
    {
        Debug.Log("end");
        isAttacking = false;
    }
    public void ProcessAttack()
    {
        if(Time.time >= nextFire)
        {
            nextFire = Time.time + 1f / fireRate; // manages firerate

            OnAttack();
            
            if (!isAutomatic) // doesn't continue firing unless checked
                isAttacking = false;
        }
    }
    public abstract void OnAttack(); 

}
