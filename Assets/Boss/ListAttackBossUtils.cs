using System.Collections.Generic;
using UnityEngine;

namespace Boss {
    public static class ListAttackBossUtils {
        public static void Shuffle<T>(this IList<T> list) {
            int n = list.Count;
            for (int i = 0; i < n - 1; i++) {
                int j = Random.Range(i, n); // UnityEngine.Random
                (list[i], list[j]) = (list[j], list[i]);
            }
        }
    }
}