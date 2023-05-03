using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour
{
    public float m_yaw = 0.022f;

    public Transform body;

    public bool locked = true;

    float xRot = 0.0f;

    void Start()
    {
        LockCursor();
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

    public void SetCursorLockState(bool active)
    {
        if (active) LockCursor();
        else UnlockCursor();
    }

    public void LockCursor()
    {
        locked = true;
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void UnlockCursor()
    {
        locked = false;
        Cursor.lockState = CursorLockMode.None;
    }

    public void ToggleCursor()
    {
        if (locked)
            UnlockCursor();
        else
            LockCursor();
    }
}
