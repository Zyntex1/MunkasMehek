using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Egg : MonoBehaviour
{
    bool open;

    [SerializeField] GameObject prefab;

    [SerializeField] GameObject UIPanel;
    [SerializeField] Image UIImage;
    [SerializeField] Text UIPrice;

    [SerializeField] Sprite sprite;

    int price;

    public void Buy()
    {
        if (!open)
            return;

        if (Hive.instance.bees.Count >= 25)
            return;

        if (Player.instance.honey < price)
            return;

        Give();

        Player.instance.honey -= price;

        CloseMenu();

        if (Hive.instance.bees.Count >= 25)
            Destroy(this);
    }

    void Give()
    {
        float r = Random.Range(0.0f, 1.0f);
        float g = Random.Range(0.0f, 1.0f);
        float b = Random.Range(0.0f, 1.0f);

        Bee bee = Instantiate(prefab, Vector3.zero, Quaternion.identity).GetComponent<Bee>();
        bee.tier = Hive.instance.GenerateTier();
        bee.color = new Color(r, g, b);

        Hive.instance.bees.Add(bee);
        Hive.instance.Display(Hive.instance.bees.Count - 1);

        if (Player.instance.HasQuest())
        {
            foreach (Quest quest in Player.instance.questGroup.quests)
                quest.goal.OnBeeGiven(bee.tier);
        }
    }

    void OpenMenu()
    {
        open = true;

        UIPanel.SetActive(true);

        UIImage.sprite = sprite;

        price = Hive.instance.bees.Count * 1000;

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
        GameInstructor.instance.Popup("E", "Tojás vásárlása");
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
