using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class BulletButton : Button
{
    protected override void Start()
    {
        base.Start();

        onClick.AddListener(() =>
        {
            SelectCarette.Select(this);
        });
    }
}