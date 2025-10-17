using UnityEngine;

[RequireComponent(typeof(Collider), typeof(Rigidbody))]
public abstract class BaseBullet : MonoBehaviour
{
    [SerializeField] protected float speed = 20f;
    [SerializeField] protected float lifeTime = 5f;

    private const string Enemy_Tag = "Enemy";

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
        if (!target.CompareTag(Enemy_Tag))
            return;

        target.GetComponent<Enemy>().Hit();
    }

    protected void DestroySelf() => Destroy(gameObject);
}