using UnityEngine;
using UnityEngine.InputSystem;

public class Gun : MonoBehaviour
{
    [SerializeField] private InputActionReference shootAction;
    [SerializeField] private Transform muzzle;

    [SerializeField] private float cooldown = 1f;

    private GameObject _selectedBulletPrefab;
    private float _lastShotTime;

    public void SelectBullet(GameObject prefab)
    {
        Debug.Log("Selected bullet: " + prefab.name);
        _selectedBulletPrefab = prefab;
    }

    private void OnEnable()
    {
        shootAction.action.performed += (ctx) => TryShoot();
    }

    private void TryShoot()
    {
        Debug.Log("Trying to shoot");

        if (Time.time - _lastShotTime < cooldown)
            return;

        Shoot();
        _lastShotTime = Time.time;
    }

    private void Shoot()
    {
        Instantiate(_selectedBulletPrefab, muzzle.position, muzzle.rotation);
    }
}