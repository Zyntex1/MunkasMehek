using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fence : MonoBehaviour
{
    [SerializeField] Transform left;
    [SerializeField] Transform right;

    [SerializeField] GameObject text;

    void Update()
    {
        if (Hive.instance.bees.Count >= 5)
            Open();
    }

    public void Open()
    {
        left.localRotation = Quaternion.Euler(0.0f, -72.0f, 0.0f);
        right.localRotation = Quaternion.Euler(0.0f, 252.0f, 0.0f);

        text.SetActive(false);
    }
}
