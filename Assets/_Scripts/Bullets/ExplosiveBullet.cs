using UnityEngine;

public class ExplosiveBullet : BaseBullet
{
    [SerializeField] private float explosionRadius = 5f;
    [SerializeField] private GameObject explosionPrefab;
    [SerializeField] private float explosionDuration = 4f;

    private const int Max_Hit_Colliders = 10;
    private readonly Collider[] _hitColliders = new Collider[Max_Hit_Colliders];
    private GameObject _explosionInstance;

    public override void OnCollisionEnter(Collision collision)
    {
        Explode();

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
}
