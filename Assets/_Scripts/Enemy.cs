using UnityEngine;

[RequireComponent(typeof(Animator), typeof(Collider))]
public class Enemy : MonoBehaviour
{
    private Animator _animator;
    private const string Shot_Trigger = "Shot";

    private void Start()
    {
        _animator = GetComponent<Animator>();
    }

    public void Hit()
    {
        _animator.SetTrigger(Shot_Trigger);
    }
}