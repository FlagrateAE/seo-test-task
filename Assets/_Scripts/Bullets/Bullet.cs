using UnityEngine;

public class Bullet : BaseBullet
{
    public override void Hit(GameObject target)
    {
        base.Hit(target);
        DestroySelf();
    }
}