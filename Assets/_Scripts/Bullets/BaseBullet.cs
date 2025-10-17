using UnityEngine;

[RequireComponent(typeof(Collider), typeof(Rigidbody))]
public abstract class BaseBullet : MonoBehaviour
{
    [SerializeField] protected float speed = 20f;
    [SerializeField] protected float lifeTime = 5f;

    private void Start()
    {
        GetComponent<Rigidbody>().velocity = transform.forward * speed;

        Invoke(nameof(DestroySelf), lifeTime);
    }

    public virtual void OnCollisionEnter(Collision collision)
    {
        Hit(collision.gameObject);
    }

    public virtual void Hit(GameObject target)
    {
        if (!gameObject.CompareTag("Enemy"))
            return;

        gameObject.GetComponent<Enemy>().Hit(gameObject);
    }

    protected void DestroySelf() => Destroy(gameObject);
}