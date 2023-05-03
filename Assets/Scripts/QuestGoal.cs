using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class QuestGoal
{
    public GoalType goalType;

    public int currentAmount;
    public int requiredAmount;

    public FieldType requiredFieldType;
    public int requiredBeeLevel;
    public BeeTier requiredBeeTier;

    void Clamp()
    {
        currentAmount = Mathf.Clamp(currentAmount, 0, requiredAmount);
    }

    public bool IsReached()
    {
        return (currentAmount >= requiredAmount);
    }

    public void OnPollenGathered(int amount, FieldType fieldType)
    {
        if (goalType == GoalType.PollenOnField && fieldType == requiredFieldType)
            currentAmount += amount;
        else if (goalType == GoalType.Pollen)
            currentAmount += amount;

        Clamp();
    }

    public void OnBeeLeveled(int level)
    {
        if (goalType == GoalType.BeeLevel && level >= requiredBeeLevel)
            currentAmount++;

        Clamp();
    }

    public void OnBeeGiven(BeeTier tier)
    {
        if (goalType == GoalType.BeeTier && tier == requiredBeeTier)
            currentAmount++;

        Clamp();
    }
}

public enum GoalType
{
    Pollen,
    PollenOnField,
    BeeLevel,
    BeeTier
}