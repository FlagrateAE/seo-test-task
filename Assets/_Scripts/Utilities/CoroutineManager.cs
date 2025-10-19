using UnityEngine;
using System.Collections;
using System;

[DefaultExecutionOrder(int.MinValue)]
public class CoroutineManager : MonoBehaviour
{
    private static CoroutineManager _instance;
    private static bool _isQuitting = false;

    public static CoroutineManager Instance
    {
        get
        {
            if (_isQuitting)
            {
                return null;
            }

            if (_instance == null)
            {
                _instance = FindObjectOfType<CoroutineManager>();

                if (_instance == null)
                {
                    var singletonObject = new GameObject(nameof(CoroutineManager));
                    _instance = singletonObject.AddComponent<CoroutineManager>();
                }
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

    private void OnDestroy()
    {
        _isQuitting = true;
    }

    public static void InvokeLater(Action action, float delay)
    {
        if (Instance != null)
        {
            Instance.StartCoroutine(InvokeRoutine(action, delay));
        }
    }

    public static Coroutine InvokeLaterCancellable(Action action, float delay)
    {
        if (Instance != null)
        {
            return Instance.StartCoroutine(InvokeRoutine(action, delay));
        }
        return null;
    }

    public static void Cancel(Coroutine coroutineToCancel)
    {
        if (_instance != null && coroutineToCancel != null)
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