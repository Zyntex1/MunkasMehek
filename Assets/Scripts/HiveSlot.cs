using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HiveSlot : MonoBehaviour
{
    private void OnMouseDown()
    {
        if (Cursor.lockState != CursorLockMode.None)
            return;

        int index = Hive.instance.GetSlot(transform);

        if (Hive.instance.usingRoyalJelly)
            Hive.instance.UseRoyalJellyOn(index);

        if (Hive.instance.usingCookie)
            Hive.instance.UseCookieOn(index);

        Hive.instance.DisplayUI(index);
    }
}
