using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileManager : Singleton<ProjectileManager>
{
    [SerializeField] GameObject projectilePrefab;

    private List<Projectile> projectiles = new List<Projectile>();

    public void ShootProjectile(Tower source, Enemy target) {
        var go = Instantiate(projectilePrefab, source.structure.position, Quaternion.identity, transform);
        var projectile = new Projectile(source, target, source.Damage);
        go.GetComponent<ProjectileView>().Init(projectile);
        projectiles.Add(projectile);
    }

    private void Update() {
        projectiles.ForEach(p => p.Tick());
        projectiles.RemoveAll(p => p.IsMarkedForRemoval);
    }
}
