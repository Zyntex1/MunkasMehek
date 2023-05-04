using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Item : MonoBehaviour
{
    bool open;

    [SerializeField] GameObject prefab;

    [SerializeField] GameObject UIPanel;
    [SerializeField] Image UIImage;
    [SerializeField] Text UIPrice;

    [SerializeField] Sprite sprite;

    public int price;

    public void Buy()
    {
        if (!open)
            return;

        if (Player.instance.honey < price)
            return;

        foreach (Transform t in Player.instance.harvestingTool.GetComponent<Transform>())
            Destroy(t.gameObject);

        Instantiate(prefab, Player.instance.harvestingTool.transform);

        Player.instance.honey -= price;

        CloseMenu();

        Destroy(this);
    }

    void OpenMenu()
    {
        open = true;

        UIPanel.SetActive(true);

        UIImage.sprite = sprite;
        UIPrice.text = $"{price} Méz";

        Player.instance.mouseLook.UnlockCursor(this);
    }

    void CloseMenu()
    {
        open = false;

        UIPanel.SetActive(false);
        Player.instance.mouseLook.LockCursor(this);
    }

    private void OnTriggerEnter(Collider other)
    {
        GameInstructor.instance.Popup("E", "Eszköz vásárlása");
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
