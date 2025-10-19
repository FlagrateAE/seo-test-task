using System.Collections.Generic;
using UnityEngine;
using TestTask.Bullets;
using TestTask.Utilities;

namespace TestTask.Actors
{
    public class Enemy : MonoBehaviour
    {
        [SerializeField] private Animator animator;
        [SerializeField] private float collisionResetDelay = 0.1f;
        [SerializeField] private List<Collider> _colliders;

        private const string Shot_Trigger = "Shot";
        private readonly int ShotHash = Animator.StringToHash(Shot_Trigger);

        private Collider _ignoredCollider;

        private void Start()
        {
            if (animator == null)
            {
                Debug.LogError("Animator component is not assigned in the Enemy script.");
            }

            if (transform.root != transform)
            {
                Debug.LogError("Enemy script is not on the root GameObject");
            }
        }

        public void Hit(Bullet bullet)
        {
            animator.SetTrigger(ShotHash);

            if (bullet is BouncyBullet)
            {
                IgnoreCollision(bullet.Collider);
                CoroutineManager.InvokeLater(ResetCollisionIgnore, collisionResetDelay);
            }
        }

        private void ResetCollisionIgnore()
        {
            if (_ignoredCollider != null)
            {
                IgnoreCollision(_ignoredCollider, ignore: false);
                _ignoredCollider = null;
            }
        }

        private void IgnoreCollision(Collider other, bool ignore = true)
        {
            _ignoredCollider = other;
            foreach (var collider in _colliders)
            {
                Physics.IgnoreCollision(collider, other, ignore);
            }
        }

        [ContextMenu("Register all colliders")]
        private void CollectColliders()
        {
            _colliders = new List<Collider>(GetComponentsInChildren<Collider>());

#if UNITY_EDITOR
            UnityEditor.EditorUtility.SetDirty(this);
#endif
        }
    }
}