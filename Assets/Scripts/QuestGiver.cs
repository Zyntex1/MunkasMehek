using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestGiver : MonoBehaviour
{
    public bool isActive = true;

    public QuestGroup questGroup;

    [SerializeField] GameObject UIPanel;
    [SerializeField] Text UIDescription;
    [SerializeField] Text UIReward;

    public void DisplayPanel()
    {
        UIPanel.SetActive(true);
        Player.instance.mouseLook.UnlockCursor();

        UIDescription.text = string.Empty;

        foreach (Quest quest in questGroup.quests)
            UIDescription.text += quest.description + "\n";

        UIReward.text = $"Jutalom: {questGroup.reward} Méz";
    }

    public void ClosePanel()
    {
        UIPanel.SetActive(false);
        Player.instance.mouseLook.LockCursor();
    }

    public void AcceptQuest()
    {
        if (!isActive)
            return;

        ClosePanel();

        Player.instance.questGroup = questGroup;

        isActive = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!isActive)
            return;

        GameInstructor.instance.Popup("E", "Küldetés kérése");
    }

    private void OnTriggerStay(Collider other)
    {
        if (Input.GetKey(KeyCode.E))
        {
            if (!isActive)
                return;

            DisplayPanel();

            GameInstructor.instance.Close();
        }

        if (Input.GetKey(KeyCode.Escape))
            ClosePanel();
    }

    private void OnTriggerExit(Collider other)
    {
        ClosePanel();

        GameInstructor.instance.Close();
    }
}
