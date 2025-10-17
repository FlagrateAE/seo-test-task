using UnityEngine;

[RequireComponent(typeof(Collider), typeof(Rigidbody))]
public class Bullet : MonoBehaviour
{
    [SerializeField] private float speed = 20f;
    [SerializeField] private float lifeTime = 5f;

    public virtual void OnHit(Enemy enemy)
    {
        enemy.Hit(gameObject);
    }

    private void Start()
    {
        GetComponent<Rigidbody>().linearVelocity = transform.forward * speed;

        Invoke(nameof(DestroySelf), lifeTime);
    }

    private void DestroySelf()
    {
        Destroy(gameObject);
    }
}