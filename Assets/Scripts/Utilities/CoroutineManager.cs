using UnityEngine;
using System.Collections;
using System;

namespace TestTask.Utilities
{
    [DefaultExecutionOrder(int.MinValue)]
    public class CoroutineManager : MonoBehaviour
    {
        private static CoroutineManager _instance;

        private void Awake()
        {
            if (_instance != null && _instance != this)
            {
                Destroy(gameObject);
                return;
            }
            _instance = this;
        }

        public static void InvokeLater(Action action, float delay)
        {
            if (_instance != null)
            {
                _instance.StartCoroutine(InvokeRoutine(action, delay));
            }
        }

        public static Coroutine InvokeLaterCancellable(Action action, float delay)
        {
            if (_instance != null)
            {
                return _instance.StartCoroutine(InvokeRoutine(action, delay));
            }
            return null;
        }

        public static void Cancel(Coroutine coroutineToCancel)
        {
            if (_instance != null && coroutineToCancel != null)
            {
                _instance.StopCoroutine(coroutineToCancel);
            }
        }

        private static IEnumerator InvokeRoutine(Action action, float delay)
        {
            yield return new WaitForSeconds(delay);
            action?.Invoke();
        }
    }
}