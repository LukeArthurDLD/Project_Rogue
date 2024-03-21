using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    public float damage = 5;
    public float range = 25;


    [Header("Fire Rate")]
    public float fireRate = 2.5f;
    private float nextFire = 0f;
    public bool isAutomatic;

    private bool isAttacking = false;

    [Header("Effects")]
    public Rigidbody shell;
    public Transform shellEject;
    public GameObject muzzleFlash;
    public LineRenderer tracer;

    [Header("Raycasts")]
    public Transform weaponOrigin;
    public Transform rayOrigin;
    private RaycastHit hit;
    private Ray ray;

    private void Awake()
    {
        if (rayOrigin == null)
            rayOrigin = weaponOrigin;
    }
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


            // set up rays for tracers & debug            
            ray.origin = weaponOrigin.position;
            ray.direction = weaponOrigin.forward;

            // debug tracer
            if (Physics.Raycast(ray, out hit))
                Debug.DrawLine(ray.origin, hit.point, Color.red, 1.0f);
            else
                Debug.DrawLine(ray.origin, ray.GetPoint(range), Color.red, 1.0f);

            if (Physics.Raycast(rayOrigin.position, rayOrigin.forward, out hit, range)) // fire ray
            {
                Debug.Log("target hit, target: " + hit.collider.name);

                // do damage //

                if (tracer)
                    SpawnTrail(weaponOrigin, hit.point); // spawn tracer when traget hit
            }
            else if (tracer)
                SpawnTrail(weaponOrigin, ray.GetPoint(range)); // if no target hit spawn tracer to max range
            
            
            // OnAttack(weaponOrigin, rayOrigin);
            
            if (!isAutomatic) // doesn't continue firing unless checked
                isAttacking = false;
        }
    }
    public void SpawnTrail(Transform origin, Vector3 hitPoint)
    {
        GameObject bulletTrailEffect = Instantiate(tracer.gameObject, origin.position, Quaternion.identity); // creates trail

        LineRenderer line = bulletTrailEffect.GetComponent<LineRenderer>();
               
        line.SetPosition(0, origin.position);
        line.SetPosition(1, hitPoint);

        Destroy(bulletTrailEffect, 1f);
    }
    public abstract void OnAttack(Transform weaponOrigin, Transform rayOrigin); 

}
