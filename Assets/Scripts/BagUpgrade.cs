using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BagUpgrade : MonoBehaviour
{
    bool open;

    [SerializeField] Bag bag;

    [SerializeField] GameObject UIPanel;
    [SerializeField] Image UIImage;
    [SerializeField] Text UIPrice;

    [SerializeField] Sprite sprite;

    int price = 1000;

    public void Buy()
    {
        if (!open)
            return;

        if (Player.instance.honey < price)
            return;

        bag.capacity = price;

        Player.instance.honey -= price;

        price = price * 2;

        CloseMenu();
    }

    void OpenMenu()
    {
        open = true;

        UIPanel.SetActive(true);

        UIImage.sprite = sprite;

        UIPrice.text = $"{price} Méz";

        Player.instance.mouseLook.UnlockCursor();
    }

    void CloseMenu()
    {
        open = false;

        UIPanel.SetActive(false);
        Player.instance.mouseLook.LockCursor();
    }

    private void OnTriggerEnter(Collider other)
    {
        GameInstructor.instance.Popup("E", "Táska vásárlása");
    }

    private void OnTriggerStay(Collider other)
    {
        if (Input.GetKey(KeyCode.E))
        {
            OpenMenu();

            GameInstructor.instance.Close();
        }

        if (Input.GetKey(KeyCode.Escape))
            CloseMenu();
    }

    private void OnTriggerExit(Collider other)
    {
        CloseMenu();

        GameInstructor.instance.Close();
    }
}
