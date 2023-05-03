using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameInstructor : MonoBehaviour
{
    public static GameInstructor instance { get; private set; }

    [SerializeField] GameObject panel;
    [SerializeField] Text UIKey;
    [SerializeField] Text UIDescription;

    void Awake()
    {
        instance = this;
    }

    public void Popup(string key, string description)
    {
        panel.SetActive(true);

        UIKey.text = key;
        UIDescription.text = description;
    }

    public void Close()
    {
        panel.SetActive(false);
    }
}
