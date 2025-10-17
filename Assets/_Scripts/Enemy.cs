using UnityEngine;

public class Enemy : MonoBehaviour
{
    public void Hit(GameObject attacker)
    {
        Debug.Log($"{name} hit!");
    }
}