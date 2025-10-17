using UnityEngine;

[RequireComponent(typeof(Collider), typeof(Rigidbody))]
public class Bullet : MonoBehaviour
{
    [SerializeField] private float speed = 20f;
    [SerializeField] private float lifeTime = 5f;

    public virtual void OnHit(Enemy enemy)
    {
        enemy.Hit(gameObject);
        DestroySelf();
    }

    public virtual void OnCollisionEnter(Collision collision)
    {
        if (!collision.gameObject.CompareTag("Enemy"))
            return;

        OnHit(collision.gameObject.GetComponent<Enemy>());
    }

    protected void DestroySelf()
    {
        Destroy(gameObject);
    }

    private void Start()
    {
        GetComponent<Rigidbody>().linearVelocity = transform.forward * speed;

        Invoke(nameof(DestroySelf), lifeTime);
    }
}