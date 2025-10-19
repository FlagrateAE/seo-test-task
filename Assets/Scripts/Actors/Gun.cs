using UnityEngine;
using UnityEngine.InputSystem;

namespace TestTask.Actors
{
    public class Gun : MonoBehaviour
    {
        [SerializeField] private InputActionReference shootAction;
        [SerializeField] private Transform muzzle;

        [SerializeField] private Animator playerAnimator;
        private const string Shoot_Trigger = "Shoot";
        private readonly int ShotHash = Animator.StringToHash(Shoot_Trigger);

        [SerializeField] private float cooldown = 1f;
        [SerializeField] private Vector2 spreadRangeX = new(0, 5);
        [SerializeField] private Vector2 spreadRangeY = new(-3, 3);
        [SerializeField] private GameObject selectedBulletPrefab;
        private float _timer;

        public void SelectBullet(GameObject prefab)
        {
            selectedBulletPrefab = prefab;
        }

        private void Start()
        {
            if (shootAction == null)
            {
                Debug.LogError("Shoot Action is not assigned in Gun script.");
            }

            if (muzzle == null)
            {
                Debug.LogError("Muzzle is not assigned in Gun script.");
            }

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
            playerAnimator.SetTrigger(ShotHash);
        }

        private Quaternion GetSpreadRotation()
        {
            float spreadX = Random.Range(spreadRangeX.x, spreadRangeX.y);
            float spreadY = Random.Range(this.spreadRangeY.x, this.spreadRangeY.y);
            Quaternion spreadRotation = Quaternion.Euler(spreadX, spreadY, 0);
            return muzzle.rotation * spreadRotation;
        }
    }
}