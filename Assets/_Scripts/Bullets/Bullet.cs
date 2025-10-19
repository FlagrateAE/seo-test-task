using UnityEngine;

[RequireComponent(typeof(Collider), typeof(Rigidbody))]
public class Bullet : MonoBehaviour
{
    [SerializeField] protected float speed = 20f;
    [SerializeField] protected float lifeTime = 5f;

    protected Rigidbody rb;

    protected virtual void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.velocity = transform.forward * speed;
        Invoke(nameof(DestroySelf), lifeTime);
    }

    protected virtual void OnCollisionEnter(Collision collision)
    {        
        TryHit(collision.gameObject);
        DestroySelf();
    }

    protected virtual Enemy TryHit(GameObject target)
    {
        if (target.TryGetComponent<Enemy>(out var enemy))
        {
            enemy.Hit();
            return enemy;
        }
        else
        {
            Debug.LogError($"No Enemy component found on {target.name}.");
            return null;
        }
    }

    protected void DestroySelf() => Destroy(gameObject);
}