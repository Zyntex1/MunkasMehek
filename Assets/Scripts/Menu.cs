using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    [SerializeField] GameObject itemsPanel;
    [SerializeField] GameObject questsPanel;

    [SerializeField] Image royalJellyImage;
    [SerializeField] Image cookieImage;
    [SerializeField] Text royalJellyText;
    [SerializeField] Text cookieText;

    [SerializeField] Text quest1Text;
    [SerializeField] Text quest2Text;
    [SerializeField] Text quest3Text;
    [SerializeField] Text quest4Text;
    [SerializeField] Text quest5Text;
    [SerializeField] Text quest6Text;
    [SerializeField] Slider quest1Progress;
    [SerializeField] Slider quest2Progress;
    [SerializeField] Slider quest3Progress;
    [SerializeField] Slider quest4Progress;
    [SerializeField] Slider quest5Progress;
    [SerializeField] Slider quest6Progress;

    void Update()
    {
        UpdateItems();
        UpdateQuests();
    }

    void UpdateItems()
    {
        if (Player.instance.royalJellies == 0)
            royalJellyImage.color = new Color32(255, 255, 255, 64);
        else
            royalJellyImage.color = new Color32(255, 255, 255, 255);

        if (Player.instance.cookies == 0)
            cookieImage.color = new Color32(255, 255, 255, 64);
        else
            cookieImage.color = new Color32(255, 255, 255, 255);

        royalJellyText.text = $"{Player.instance.royalJellies}x";
        cookieText.text = $"{Player.instance.cookies}x";
    }

    void UpdateQuests()
    {
        if (Player.instance.HasQuest())
        {
            quest1Progress.gameObject.SetActive(true);
            quest2Progress.gameObject.SetActive(true);
            quest3Progress.gameObject.SetActive(true);
            quest4Progress.gameObject.SetActive(true);
            quest5Progress.gameObject.SetActive(true);
            quest6Progress.gameObject.SetActive(true);

            Quest quest1 = Player.instance.questGroup.quests[0];
            Quest quest2 = Player.instance.questGroup.quests[1];
            Quest quest3 = Player.instance.questGroup.quests[2];
            Quest quest4 = Player.instance.questGroup.quests[3];
            Quest quest5 = Player.instance.questGroup.quests[4];
            Quest quest6 = Player.instance.questGroup.quests[5];

            quest1Text.text = $"{quest1.description} ({quest1.goal.currentAmount}/{quest1.goal.requiredAmount})";
            quest2Text.text = $"{quest2.description} ({quest2.goal.currentAmount}/{quest2.goal.requiredAmount})";
            quest3Text.text = $"{quest3.description} ({quest3.goal.currentAmount}/{quest3.goal.requiredAmount})";
            quest4Text.text = $"{quest4.description} ({quest4.goal.currentAmount}/{quest4.goal.requiredAmount})";
            quest5Text.text = $"{quest5.description} ({quest5.goal.currentAmount}/{quest5.goal.requiredAmount})";
            quest6Text.text = $"{quest6.description} ({quest6.goal.currentAmount}/{quest6.goal.requiredAmount})";

            quest1Progress.value = (float)quest1.goal.currentAmount / (float)quest1.goal.requiredAmount;
            quest2Progress.value = (float)quest2.goal.currentAmount / (float)quest2.goal.requiredAmount;
            quest3Progress.value = (float)quest3.goal.currentAmount / (float)quest3.goal.requiredAmount;
            quest4Progress.value = (float)quest4.goal.currentAmount / (float)quest4.goal.requiredAmount;
            quest5Progress.value = (float)quest5.goal.currentAmount / (float)quest5.goal.requiredAmount;
            quest6Progress.value = (float)quest6.goal.currentAmount / (float)quest6.goal.requiredAmount;
        }
        else
        {
            quest1Progress.gameObject.SetActive(false);
            quest2Progress.gameObject.SetActive(false);
            quest3Progress.gameObject.SetActive(false);
            quest4Progress.gameObject.SetActive(false);
            quest5Progress.gameObject.SetActive(false);
            quest6Progress.gameObject.SetActive(false);
        }
    }

    public void BackToMainMenu()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }

    public void Items()
    {
        itemsPanel.SetActive(true);
        questsPanel.SetActive(false);
    }

    public void Quests()
    {
        itemsPanel.SetActive(false);
        questsPanel.SetActive(true);
    }
}
