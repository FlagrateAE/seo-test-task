using UnityEngine;
using System.Collections;
using System;

public class CoroutineManager : MonoBehaviour
{
    private static CoroutineManager _instance;

    public static CoroutineManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<CoroutineManager>();
            }
            if (_instance == null)
            {
                var singletonObject = new GameObject(nameof(CoroutineManager));
                _instance = singletonObject.AddComponent<CoroutineManager>();
            }
            return _instance;
        }
    }

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
            return;
        }
        _instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public static void InvokeLater(Action action, float delay)
    {
        Instance.StartCoroutine(InvokeRoutine(action, delay));
    }

    public static Coroutine InvokeLaterCancellable(Action action, float delay)
    {
        return Instance.StartCoroutine(InvokeRoutine(action, delay));
    }

    public static void Cancel(Coroutine coroutineToCancel)
    {
        if (coroutineToCancel != null && Instance != null)
        {
            Instance.StopCoroutine(coroutineToCancel);
        }
    }

    private static IEnumerator InvokeRoutine(Action action, float delay)
    {
        yield return new WaitForSeconds(delay);
        action?.Invoke();
    }
}