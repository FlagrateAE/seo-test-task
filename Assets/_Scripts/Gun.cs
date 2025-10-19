using UnityEngine;
using UnityEngine.InputSystem;

public class Gun : MonoBehaviour
{
    [SerializeField] private InputActionReference shootAction;
    [SerializeField] private Transform muzzle;

    [SerializeField] private Animator playerAnimator;
    private const string Shoot_Trigger = "Shoot";
    private readonly int ShotHash = Animator.StringToHash(Shoot_Trigger);

    [SerializeField] private float cooldown = 1f;
    [SerializeField] private float spread = 5f;
    [SerializeField] private GameObject selectedBulletPrefab;
    private float _timer;

    public void SelectBullet(GameObject prefab)
    {
        selectedBulletPrefab = prefab;
    }

    private void Start()
    {
        if (playerAnimator == null)
        {
            Debug.LogError("Player Animator is not assigned in Gun script.");
        }
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
        playerAnimator.TrySetTrigger(ShotHash);
    }

    private Quaternion GetSpreadRotation()
    {
        float spreadX = Random.Range(0, spread * 0.5f);
        float spreadY = Random.Range(-spread * 0.5f, spread * 0.5f);
        Quaternion spreadRotation = Quaternion.Euler(spreadX, spreadY, 0);
        return muzzle.rotation * spreadRotation;
    }
}