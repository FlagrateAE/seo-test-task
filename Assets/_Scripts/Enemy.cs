using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private List<Collider> _colliders;
    [SerializeField] private float hitCooldown = 0.1f;

    private const string Shot_Trigger = "Shot";
    private readonly int ShotHash = Animator.StringToHash(Shot_Trigger);

    private Collider _ignoredCollider;

    private void Start()
    {
        if (animator == null)
        {
            Debug.LogError("Animator component is not assigned in the Enemy script.");
        }
    }

    public void Hit(Bullet bullet)
    {
        animator.TrySetTrigger(ShotHash);

        if (bullet is BouncyBullet)
        {
            IgnoreCollision(bullet.Collider);
            Invoke(nameof(ResetCollisionIgnore), hitCooldown);
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

    [ContextMenu("Collect Colliders")]
    private void CollectColliders()
    {
        _colliders = new List<Collider>(GetComponentsInChildren<Collider>());

#if UNITY_EDITOR
        UnityEditor.EditorUtility.SetDirty(this);
#endif
    }
}