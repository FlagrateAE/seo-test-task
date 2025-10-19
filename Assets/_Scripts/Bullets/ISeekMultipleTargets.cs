using System.Collections.Generic;
using UnityEngine;

public interface ISeekMultipleEnemies
{
    public const int Max_Hit_Colliders = 50;
    public Collider[] HitColliders { get; }
}

public static class SeekerUtils
{
    private static readonly LayerMask _enemyLayer = LayerMask.GetMask("Enemy");
    private static readonly List<Transform> _scanned = new();

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
            if (collider != null)
            {
                var enemyTransform = collider.transform.root;
                if (_scanned.Contains(enemyTransform))
                {
                    continue;
                }

                if (enemyTransform.TryGetComponent<Enemy>(out var enemy))
                {
                    _scanned.Add(enemyTransform);
                    enemies.Add(enemy);
                }
                else
                {
                    Debug.LogError($"No Enemy component on {enemyTransform.name}.");
                }
            }
        }
        seeker.HitColliders.Initialize();
        _scanned.Clear();
    }
}