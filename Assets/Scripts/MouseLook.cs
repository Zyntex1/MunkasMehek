using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour
{
    public float m_yaw = 0.022f;

    public Transform body;

    public bool locked = true;
    public Dictionary<MonoBehaviour, bool> locker = new Dictionary<MonoBehaviour, bool>();

    float xRot = 0.0f;

    void Start()
    {
        LockCursor(this);
    }

    void Update()
    {
        if (!locked)
            return;

        float mx = Input.GetAxis("Mouse X") * Settings.sensitivity * m_yaw * 10000.0f * Time.deltaTime;
        float my = Input.GetAxis("Mouse Y") * Settings.sensitivity * m_yaw * 10000.0f * Time.deltaTime;

        xRot -= my;
        xRot = Mathf.Clamp(xRot, -90.0f, 90.0f);

        transform.localRotation = Quaternion.Euler(xRot, 0.0f, 0.0f);
        body.Rotate(Vector3.up * mx);
    }

    public void SetCursorLockState(bool active, MonoBehaviour caller)
    {
        if (active) LockCursor(caller);
        else UnlockCursor(caller);
    }

    public void LockCursor(MonoBehaviour caller)
    {
        locker[caller] = true;

        bool _lock = true;
        foreach (KeyValuePair<MonoBehaviour, bool> kv in locker)
        {
            if (!kv.Value)
                _lock = false;
        }

        locked = _lock;

        if (locked)
            Cursor.lockState = CursorLockMode.Locked;
    }

    public void UnlockCursor(MonoBehaviour caller)
    {
        locker[caller] = false;
        locked = false;
        Cursor.lockState = CursorLockMode.None;
    }

    public void ToggleCursor(MonoBehaviour caller)
    {
        if (locked)
            UnlockCursor(caller);
        else
            LockCursor(caller);
    }
}
