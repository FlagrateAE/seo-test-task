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
        if (IsEnemy(collision.gameObject, out var enemy))
        {
            enemy.Hit(this);
        }
        DestroySelf();
    }

    protected bool IsEnemy(GameObject target, out Enemy enemy)
    {
        if (target.transform.root.TryGetComponent(out enemy))
        {
            return true;
        }
        return false;
    }

    protected void DestroySelf() => Destroy(gameObject);
}