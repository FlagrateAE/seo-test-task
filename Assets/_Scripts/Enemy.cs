using UnityEngine;

public class Enemy : MonoBehaviour
{
    public void Hit(GameObject attacker)
    {
        Debug.Log("Enemy hit by: " + attacker.name);
    }
}