using UnityEngine;
using UnityEngine.InputSystem;

public class Gun : MonoBehaviour
{
    [SerializeField] private InputActionReference shootAction;
    [SerializeField] private Transform muzzle;

    [SerializeField] private Animator playerAnimator;
    private const string Shoot_Trigger = "Shoot";

    [SerializeField] private float cooldown = 1f;
    [SerializeField] private float spread = 5f;
    [SerializeField] private GameObject selectedBulletPrefab;
    private float _lastShotTime;

    public void SelectBullet(GameObject prefab)
    {
        selectedBulletPrefab = prefab;
    }

    private void OnEnable()
    {
        shootAction.action.started += (ctx) => TryShoot();
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
        Instantiate(selectedBulletPrefab, muzzle.position, GetSpreadRotation());
        playerAnimator.SetTrigger(Shoot_Trigger);
    }

    private Quaternion GetSpreadRotation()
    {
        float spreadY = Random.Range(-spread / 2f, spread / 2f);
        return Quaternion.Euler(0, spreadY + 180, 0);
    }
}