using System.Collections.Generic;
using UnityEngine;

public class BouncyBullet : Bullet, ISeekMultipleEnemies
{
    [SerializeField] private int maxBounces = 3;
    [SerializeField] private float seekRadius = 5f;

    private readonly Collider[] _hitColliders = new Collider[ISeekMultipleEnemies.Max_Hit_Colliders];
    public Collider[] HitColliders => _hitColliders;

    private int _leftBounces;
    private Enemy _lastHitEnemy;

    protected override void Start()
    {
        base.Start();
        _leftBounces = maxBounces;
    }

    protected override void OnCollisionEnter(Collision collision)
    {
        if (
            !IsEnemy(collision.gameObject, out var hitEnemy) ||
            hitEnemy == _lastHitEnemy
        ) return;

        _leftBounces--;
        _lastHitEnemy = hitEnemy;
        hitEnemy.Hit(this);

        if (_leftBounces <= 0)
        {
            Destroy(gameObject);
            return;
        }

        var nextEnemy = SelectNextEnemy(exclude: hitEnemy);

        if (nextEnemy != null)
        {
            TargetEnemy(nextEnemy);
        }
        else
        {
            Destroy(gameObject);
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