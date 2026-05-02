using GoveKits.Runtime.Core;
using UnityEngine;

public class FloatTextManager : MonoSingleton<FloatTextManager>
{
    private GameObject floatTextPrefab;


    public void Show(string text, Vector3 position, Color color)
    {
        if (floatTextPrefab == null)
        {
            floatTextPrefab = SpawnManager.Instance.GetFloatTextPrefab();
        }

        GameObject floatTextObj = PoolCore.Get(floatTextPrefab);
        FloatText floatText = floatTextObj.GetComponent<FloatText>();
        floatText.Setup(text, position, color);
    }
}
