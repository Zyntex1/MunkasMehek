using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HarvestingTool : MonoBehaviour
{
    public int multiplier = 1;

    float nextHarvest;

    AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        Harvest();
    }

    void Harvest()
    {
        if (!Player.instance.IsOnField())
            return;

        if (Input.GetButton("Fire1"))
        {
            if (nextHarvest < Time.time)
            {
                int pollen = Mathf.Clamp(Random.Range(-5, 5) + multiplier, 1, int.MaxValue);

                if (Player.instance.pollen + pollen > Player.instance.bag.capacity)
                    pollen = Player.instance.bag.capacity - Player.instance.pollen;

                Player.instance.pollen += pollen;

                Collider[] flowers = Physics.OverlapSphere(Player.instance.transform.position, 1.0f, 1 << 11);
                foreach (Collider flower in flowers)
                {
                    if (Random.Range(0, 2) == 1)
                        StartCoroutine(HarvestFlower(flower.gameObject));
                }

                if (Player.instance.HasQuest())
                {
                    foreach (Quest quest in Player.instance.questGroup.quests)
                        quest.goal.OnPollenGathered(pollen, Player.instance.GetFieldType());
                }

                audioSource.Play();

                nextHarvest = Time.time + 1.0f;
            }
        }
    }

    IEnumerator HarvestFlower(GameObject flower)
    {
        flower.SetActive(false);

        yield return new WaitForSeconds(5.0f);

        flower.SetActive(true);
    }
}
