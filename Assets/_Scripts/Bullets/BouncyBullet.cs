using System.Collections.Generic;
using UnityEngine;

public class BouncyBullet : Bullet, ISeekMultipleEnemies
{
    [SerializeField] private int maxBounces = 3;
    [SerializeField] private float seekRadius = 5f;

    private readonly Collider[] _hitColliders = new Collider[ISeekMultipleEnemies.Max_Hit_Colliders];
    public Collider[] HitColliders => _hitColliders;

    private int _leftBounces;
    private Collider _collider;

    protected override void Start()
    {
        base.Start();
        _leftBounces = maxBounces;
        _collider = GetComponent<Collider>();
    }

    protected override void OnCollisionEnter(Collision collision)
    {
        _leftBounces--;
        Enemy hit = TryHit(collision.gameObject);
        Debug.Log($"Bullet hit {hit.gameObject.name}");

        if (_leftBounces <= 0) DestroySelf();

        var nextEnemy = SelectNextEnemy(exclude: hit);
        if (nextEnemy != null)
        {
            TargetEnemy(nextEnemy);
        }
        else
        {
            DestroySelf();
        }
    }

    private Enemy SelectNextEnemy(Enemy exclude)
    {
        this.SeekEnemies(
            transform.position,
            seekRadius,
            out List<Enemy> enemies
        );

        if (enemies.Count == 0) return null;

        enemies.Remove(exclude);
        return enemies.SelectRandom();
    }

    private void TargetEnemy(Enemy target)
    {
        transform.LookAt(target.transform);
        rb.velocity = speed * transform.forward;
    }
}