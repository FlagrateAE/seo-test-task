using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

[RequireComponent(typeof(Image))]
public class SelectCarette : MonoBehaviour
{
    private static SelectCarette _instance;

    public float CaretteSpeed = 0.2f;

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
            return;
        }
        _instance = this;
    }

    public static void Select(BulletButton button)
    {
        _instance.transform.DOMove(button.transform.position, _instance.CaretteSpeed);
    }
}