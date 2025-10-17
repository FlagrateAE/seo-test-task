using UnityEngine;

public class BouncyBullet : BaseBullet
{
    [SerializeField] private int maxBounces = 3;

    public override void Hit(GameObject target)
    {
        maxBounces--;
        base.Hit(target);
        if (maxBounces <= 0) DestroySelf();
    }
}