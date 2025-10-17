using UnityEngine;

public class BouncyBullet : Bullet
{
    [SerializeField] private int maxBounces = 3;

    public override void OnHit(Enemy enemy)
    {
        enemy.Hit(gameObject);
        if (maxBounces <= 0) DestroySelf();
    }

    public override void OnCollisionEnter(Collision collision)
    {
        maxBounces--;
        base.OnCollisionEnter(collision);
    }
}