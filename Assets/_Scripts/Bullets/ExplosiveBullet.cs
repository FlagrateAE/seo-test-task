using UnityEngine;

public class ExplosiveBullet : BaseBullet
{
    [SerializeField] private float explosionRadius = 5f;

    private readonly Collider[] _hitColliders = new Collider[3];

    public override void OnCollisionEnter(Collision collision)
    {
        Physics.OverlapSphereNonAlloc(
            collision.contacts[0].point,
            explosionRadius,
            _hitColliders
        );

        foreach (var hitCollider in _hitColliders)
        {
            Hit(hitCollider.gameObject);
        }
        _hitColliders.Initialize();

        DestroySelf();
    }
}
