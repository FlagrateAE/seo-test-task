using UnityEngine;
using System.Collections.Generic;

namespace TestTask.Utilities
{
    public static class ListExtensions
    {
        public static T SelectRandom<T>(this List<T> list)
        {
            if (list == null || list.Count == 0) return default;
            int randomIndex = Random.Range(0, list.Count);
            return list[randomIndex];
        }
    }
}