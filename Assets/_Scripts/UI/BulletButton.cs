using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image), typeof(Button))]
public class BulletButton : MonoBehaviour
{
    [SerializeField] private GameObject bulletPrefab;
}