using UnityEngine;
using TestTask.Actors;
using TestTask.Utilities;

namespace TestTask.Bullets
{
    [RequireComponent(typeof(Collider), typeof(Rigidbody))]
    public class Bullet : MonoBehaviour
    {
        [SerializeField] protected float speed = 20f;
        [SerializeField] protected float lifeTime = 5f;

        public Collider Collider { get; private set; }
        protected Rigidbody rb;
        protected Coroutine _destroyCoroutine;

        protected virtual void Start()
        {
            Collider = GetComponent<Collider>();
            if (Collider == null)
            {
                Debug.LogError("Collider component is not assigned to the bullet.");
            }

            rb = GetComponent<Rigidbody>();
            rb.velocity = transform.forward * speed;
            _destroyCoroutine = CoroutineManager.InvokeLaterCancellable(
                () => Destroy(gameObject),
                lifeTime
            );
        }

        protected virtual void OnCollisionEnter(Collision collision)
        {
            if (IsEnemy(collision.gameObject, out var enemy))
            {
                enemy.Hit(this);
            }
            Destroy(gameObject);
        }

        protected bool IsEnemy(GameObject target, out Enemy enemy)
        {
            if (target.transform.root.TryGetComponent(out enemy))
            {
                return true;
            }
            return false;
        }

        protected virtual void OnDestroy()
        {
            CoroutineManager.Cancel(_destroyCoroutine);
        }
    }
}