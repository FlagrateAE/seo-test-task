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
    private float _timer;

    public void SelectBullet(GameObject prefab)
    {
        selectedBulletPrefab = prefab;
    }

    private void OnEnable()
    {
        shootAction.action.started += TryShoot;
    }

    private void OnDisable()
    {
        shootAction.action.started -= TryShoot;
    }

    private void Update()
    {
        if (_timer > 0f)
            _timer -= Time.deltaTime;
    }

    private void TryShoot(InputAction.CallbackContext _)
    {
        if (_timer > 0f) return;

        _timer = cooldown;
        Shoot();
    }

    private void Shoot()
    {
        Instantiate(selectedBulletPrefab, muzzle.position, GetSpreadRotation());
        playerAnimator.SetTrigger(Shoot_Trigger);
    }

    private Quaternion GetSpreadRotation()
    {
        float spreadY = Random.Range(-spread / 2f, spread / 2f);
        return Quaternion.Euler(0, spreadY + 180, 0); //fix
    }
}