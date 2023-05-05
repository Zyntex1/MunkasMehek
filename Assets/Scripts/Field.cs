using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Field : MonoBehaviour
{
    public FieldType fieldType;

    [SerializeField] GameObject[] flowers;
    [SerializeField] float subdivisions = 1.0f;
    [SerializeField] float offset;

    [SerializeField] GameObject cookie;

    float nextDrop = 5.0f;

    void Start()
    {
        for (int ix = 0; ix < transform.localScale.x * subdivisions; ix++)
        {
            for (int iz = 0; iz < transform.localScale.z * subdivisions; iz++)
            {
                float x = (float)ix / subdivisions - transform.localScale.x / 2 + 0.5f / subdivisions;
                float z = (float)iz / subdivisions - transform.localScale.z / 2 + 0.5f / subdivisions;

                GameObject flower = flowers[Random.Range(0, flowers.Length)];
                GameObject f = Instantiate(flower, new Vector3(transform.position.x + x, transform.position.y + offset, transform.position.z + z), flower.transform.rotation);
                f.transform.parent = transform;
            }
        }
    }

    void Update()
    {
        if (!Player.instance.IsOnField())
            return;

        if (Player.instance.GetFieldType() != fieldType)
            return;

        if (nextDrop < Time.time)
        {
            float rand = Random.Range(0.0f, 1.0f);

            if (rand <= 0.01f)
            {
                Player.instance.royalJellies++;
            }
            else
            {
                float x = Random.Range(transform.localScale.x / 2, -(transform.localScale.x / 2));
                float z = Random.Range(transform.localScale.z / 2, -(transform.localScale.z / 2));

                Instantiate(cookie, new Vector3(transform.position.x + x, transform.position.y, transform.position.z + z), Quaternion.identity);

                nextDrop = Time.time + 5.0f;
            }
        }
    }
}

public enum FieldType
{
    Mushroom,
    Pumpkin,
    Sunflower,
    Flower
}