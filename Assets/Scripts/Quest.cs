using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Quest
{
    public string description;

    public QuestGoal goal;
}

[System.Serializable]
public class QuestGroup
{
    public List<Quest> quests = new List<Quest>();

    public int reward;

    public QuestGiver next;
}