using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileManager : Singleton<ProjectileManager>
{
    [SerializeField] GameObject projectilePrefab;

    public void ShootProjectile(Tower source, Enemy target) {
        var go = Instantiate(projectilePrefab, new Vector3(), Quaternion.identity, transform);
    }

}
