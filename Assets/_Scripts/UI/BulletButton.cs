using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

[RequireComponent(typeof(Image), typeof(Button))]
public class BulletButton : MonoBehaviour
{
    [SerializeField] private RectTransform carette;
    [SerializeField] private float caretteSpeed = 0.2f;
    [SerializeField] private Vector2 caretteOffset = new(0, -10);


    public void Select()
    {
        carette.DOMove(((Vector2)transform.position) + caretteOffset, caretteSpeed);
    }
}