using System.Collections.Generic;
using UnityEngine;

public class ExplosiveBullet : Bullet, ISeekMultipleEnemies
{
    [SerializeField] private float explosionRadius = 5f;
    [SerializeField] private GameObject explosionPrefab;
    [SerializeField] private float explosionDuration = 4f;

    private readonly Collider[] _hitColliders = new Collider[ISeekMultipleEnemies.Max_Hit_Colliders];
    public Collider[] HitColliders => _hitColliders;
    private GameObject _explosionInstance;

    protected override void OnCollisionEnter(Collision collision)
    {
        Explode();

        this.SeekEnemies(
            transform.position,
            explosionRadius,
            out List<Enemy> enemies
        );
        
        foreach (var enemy in enemies)
        {
            enemy.Hit(this);
        }
    }

    private void Explode()
    {
        gameObject.SetActive(false);

        _explosionInstance = Instantiate(
            explosionPrefab,
            transform.position,
            Quaternion.identity
        );

        Invoke(nameof(DestroySelf), explosionDuration);
    }

    private void OnDestroy()
    {
        if (_explosionInstance != null)
        {
            Destroy(_explosionInstance);
        }
    }

    private void DrawDebugSphere()
    {
        for (int i = 0; i <= 360; i += 10)
        {
            float rad = i * Mathf.Deg2Rad;
            Vector3 point = new(
                transform.position.x + Mathf.Cos(rad) * explosionRadius,
                transform.position.y,
                transform.position.z + Mathf.Sin(rad) * explosionRadius
            );
            Debug.DrawLine(transform.position, point, Color.red, 1f);
        }
    }
}
