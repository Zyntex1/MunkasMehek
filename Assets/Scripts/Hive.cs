using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Hive : MonoBehaviour
{
    public static Hive instance { get; private set; }

    public List<Bee> bees = new List<Bee>();

    [SerializeField] GameObject UIPanel;
    [SerializeField] Image UIImage;
    [SerializeField] Text UITitle;
    [SerializeField] Text UITier;
    [SerializeField] Text UILevel;
    [SerializeField] Text UIXP;

    public bool usingRoyalJelly;
    public bool usingCookie;

    bool converting;

    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        Display(0);
    }

    void Update()
    {
        if (converting)
        {
            for (int i = 0; i < bees.Count; i++)
            {
                Transform t = GetSlot(i).GetChild(0);
                t.Rotate(0.0f, 0.5f, 0.0f, Space.Self);
            }
        }
        else
        {
            for (int i = 0; i < bees.Count; i++)
            {
                Transform t = GetSlot(i).GetChild(0);
                t.rotation = Quaternion.identity;
            }
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            UIPanel.SetActive(false);
            Player.instance.mouseLook.LockCursor(this);
        }
    }

    public void Display(int index)
    {
        GameObject display = Instantiate(bees[index].gameObject, GetSlot(index));

        Destroy(display.GetComponent<Bee>());

        display.transform.localPosition = Vector3.zero;
        display.transform.localRotation = Quaternion.identity;
        display.transform.localScale = Vector3.one;
    }

    public void DisplayUI(int index)
    {
        if (bees.Count <= index)
            return;

        UIPanel.SetActive(true);

        Bee bee = bees[index];

        UIImage.color = bee.color;
        UITitle.color = bee.color;
        UITitle.text = $"{bee.tier} méhecske";
        UITier.text = $"Rang: {bee.tier}";
        UILevel.text = $"Szint: {bee.level}";

        if (bee.level < 5)
            UIXP.text = $"XP: {bee.xp}/{bee.neededXp}";
        else
            UIXP.text = $"XP: MAX";
    }

    public BeeTier GenerateTier()
    {
        float rand = Random.Range(0.0f, 1.0f);

        if (rand <= 0.025f)
            return BeeTier.Mitikus;
        else if (rand <= 0.05f)
            return BeeTier.Legendás;
        else if (rand <= 0.25f)
            return BeeTier.Ritka;
        else
            return BeeTier.Gyakori;
    }

    public void UseRoyalJelly()
    {
        if (Player.instance.royalJellies == 0)
            return;

        usingRoyalJelly = true;

        GameInstructor.instance.Popup("B Klikk", "Kattints valamelyik méhre");
    }

    public void UseRoyalJellyOn(int index)
    {
        bees[index].tier = (BeeTier)Mathf.Clamp((int)GenerateTier() + 1, 0, 3);

        usingRoyalJelly = false;

        Player.instance.royalJellies--;

        if (Player.instance.HasQuest())
        {
            foreach (Quest quest in Player.instance.questGroup.quests)
                quest.goal.OnBeeGiven(bees[index].tier);
        }

        GameInstructor.instance.Close();
    }

    public void UseCookie()
    {
        if (Player.instance.cookies == 0)
            return;

        usingCookie = true;

        GameInstructor.instance.Popup("B Klikk", "Kattints valamelyik méhre");
    }

    public void UseCookieOn(int index)
    {
        Bee bee = bees[index];

        bee.xp++;

        if (bee.xp >= bee.neededXp)
        {
            if (bee.level < 5)
            {
                bee.level++;
                bee.xp = 0;
                bee.neededXp += 10;

                if (Player.instance.HasQuest())
                {
                    foreach (Quest quest in Player.instance.questGroup.quests)
                        quest.goal.OnBeeLeveled(bee.level);
                }
            }
        }

        usingCookie = false;

        Player.instance.cookies--;

        GameInstructor.instance.Close();
    }

    public int GetSlot(Transform t)
    {
        return t.GetSiblingIndex();
    }

    public Transform GetSlot(int index)
    {
        return transform.GetChild(index).GetChild(0);
    }

    IEnumerator Convert()
    {
        converting = true;

        while (Player.instance.pollen > 0)
        {
            int level = 0;
            foreach (Bee bee in bees)
                level += Random.Range(-5, 5) + bee.level * 30 * ((int)bee.tier + 1);

            if (Player.instance.pollen - level < 0)
                level = Player.instance.pollen;

            Player.instance.pollen -= level;
            Player.instance.honey += level;

            yield return new WaitForSeconds(1.0f);
        }

        converting = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        GameInstructor.instance.Popup("E", "Méz készítése");
    }

    private void OnTriggerStay(Collider other)
    {
        if (Input.GetKey(KeyCode.E))
        {
            if (!converting)
                StartCoroutine(Convert());
        }
    }

    private void OnTriggerExit(Collider other)
    {
        StopAllCoroutines();

        GameInstructor.instance.Close();
    }
}
