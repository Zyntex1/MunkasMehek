using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bee : MonoBehaviour
{
    Material mat;

    public BeeTier tier = 0;
    public int level = 1;
    public int xp = 0;
    public int neededXp = 10;
    public Color color;

    float nextHarvest;

    Vector2 offset;

    Vector3 playerPos;

    public Bee(BeeTier _tier, int _level, Color _color)
    {
        tier = _tier;
        level = _level;
        color = _color;
    }

    void Awake()
    {
        mat = GetComponentInChildren<Renderer>().materials[1];
        mat.color = color;

        offset = Random.insideUnitCircle.normalized * 2.0f;
    }

    void Update()
    {
        mat.color = color;

        playerPos = Player.instance.transform.position;

        Move();

        Animate();

        Harvest();
    }

    void Move()
    {
        transform.position = new Vector3(playerPos.x + offset.x, 1.3f, playerPos.z + offset.y);
        transform.rotation = Player.instance.transform.rotation;
    }

    void Animate()
    {
        transform.Rotate(0.0f, 0.0f, Mathf.Sin(Time.time * 20.0f) * 5.0f, Space.Self);
    }

    void Harvest()
    {
        if (!Player.instance.IsOnField())
            return;

        transform.position = Vector3.Lerp(transform.position, new Vector3(transform.position.x, transform.position.y - 1.0f, transform.position.z), Time.deltaTime);

        if (nextHarvest < Time.time)
        {
            int pollen = Mathf.Clamp(Random.Range(-5, 5) + level * 10 * ((int)tier + 1), 1, int.MaxValue);

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

            nextHarvest = Time.time + 1.0f;
        }
    }

    IEnumerator HarvestFlower(GameObject flower)
    {
        flower.SetActive(false);

        yield return new WaitForSeconds(5.0f);

        flower.SetActive(true);
    }
}

public enum BeeTier
{
    Gyakori,
    Ritka,
    Legendás,
    Mitikus
}