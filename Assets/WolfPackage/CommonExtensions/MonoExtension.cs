using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class MonoExtension
{
    public static void ClearChildren(this Transform parent)
    {
        foreach (Transform child in parent)
        {
            GameObject.Destroy(child.gameObject);
        }
    }
}
