using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioMaster : MonoBehaviour
{
    void Update()
    {
        AudioListener.volume = Settings.volume;
    }
}
