using UnityEngine;

[RequireComponent(typeof(Collider), typeof(Rigidbody))]
public class Bullet : MonoBehaviour
{
    [SerializeField] protected float speed = 20f;
    [SerializeField] protected float lifeTime = 5f;

    protected Rigidbody rb;

    public Collider Collider {get; private set; }

    protected virtual void Start()
    {
        Collider = GetComponent<Collider>();
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
        if (target.transform.root.TryGetComponent<Enemy>(out var enemy))
        {
            enemy.Hit(this);
            return enemy;
        }
        else
        {

            Debug.LogError($"No Enemy component on {target.name}.");
            return null;
        }
    }

    protected void DestroySelf() => Destroy(gameObject);
}