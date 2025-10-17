using UnityEngine;

public class ExplosiveBullet : Bullet
{
    [SerializeField] private float explosionRadius = 5f;

    private readonly Collider[] _hitColliders = new Collider[3];

    public override void OnCollisionEnter(Collision collision)
    {
        if (!collision.gameObject.CompareTag("Enemy"))
            return;

        Physics.OverlapSphereNonAlloc(
            collision.contacts[0].point,
            explosionRadius,
            _hitColliders
        );

        foreach (var hitCollider in _hitColliders)
        {
            if (hitCollider == null || !hitCollider.CompareTag("Enemy"))
                continue;

            OnHit(hitCollider.GetComponent<Enemy>());
        }
    }
}
