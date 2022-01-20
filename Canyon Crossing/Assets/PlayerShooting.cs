using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    public Camera fpsCamera;
    public GameObject projectile;
    public Transform firePoint;
    public float projectileSpeed = 20f;
    private Vector3 destination;

    // Fire if no projectiles currently exist
    void Update()
    {
        if (Input.GetButtonDown("Fire1") && GameObject.FindGameObjectsWithTag("Projectile").Length == 0)
        {
            ShootProjectile();
        }
    }

    // Get destination of projectile
    void ShootProjectile()
    {
        Ray ray = fpsCamera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            destination = hit.point;
        }
        else
        {
            destination = ray.GetPoint(500);
        }

        InstantiateProjectile(firePoint);
    }

    // Render the projectile
    void InstantiateProjectile(Transform firePOint)
    {
        var projectileObj = Instantiate(projectile, firePoint.position, Quaternion.identity) as GameObject;
        projectileObj.GetComponent<Rigidbody>().velocity = (destination - firePoint.position).normalized * projectileSpeed;
    }
}
