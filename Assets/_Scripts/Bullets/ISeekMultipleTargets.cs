using System.Collections.Generic;
using UnityEngine;

public interface ISeekMultipleEnemies
{
    public const int Max_Hit_Colliders = 10;
    public Collider[] HitColliders { get; }
}

public static class SeekerUtils
{
    private static LayerMask _enemyLayer = LayerMask.GetMask("Enemy");

    public static void SeekEnemies(
        this ISeekMultipleEnemies seeker,
        Vector3 position,
        float radius,
        out List<Enemy> enemies
    )
    {
        Physics.OverlapSphereNonAlloc(
            position,
            radius,
            seeker.HitColliders,
            _enemyLayer
        );

        enemies = new List<Enemy>();
        foreach (var collider in seeker.HitColliders)
        {
            if (
                collider != null &&
                collider.gameObject.TryGetComponent<Enemy>(out var enemy)
            )
            {
                enemies.Add(enemy);
            }
        }
        seeker.HitColliders.Initialize();
    }
}