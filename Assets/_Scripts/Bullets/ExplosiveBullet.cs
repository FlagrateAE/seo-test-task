using System.IO.IsolatedStorage;
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
            if (hitCollider == null) continue;
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
