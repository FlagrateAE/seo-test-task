using UnityEngine;
using UnityEngine.InputSystem;

public class Gun : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private InputActionReference shootAction;
    [SerializeField] private Transform muzzle;

    [Header("Settings")]
    [SerializeField] private float cooldown = 1f;
    [SerializeField] private Bullet _selectedBullet;

    private float _lastShotTime;

    public void SelectBullet(Bullet bullet)
    {
        _selectedBullet = bullet;
    }

    private void OnEnable()
    {
        shootAction.action.performed += (ctx) => TryShoot();
    }

    private void TryShoot()
    {
        if (Time.time - _lastShotTime < cooldown)
            return;

        Shoot();
        _lastShotTime = Time.time;
    }

    private void Shoot()
    {
        Instantiate(_selectedBullet.gameObject, muzzle.position, muzzle.rotation);
    }
}