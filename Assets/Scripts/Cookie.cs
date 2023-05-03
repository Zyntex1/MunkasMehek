using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cookie : MonoBehaviour
{
    void Start()
    {
        StartCoroutine(Despawn());
    }

    void Update()
    {
        transform.Rotate(0.0f, 0.5f, 0.0f, Space.Self);
        transform.position = new Vector3(transform.position.x, 0.6f + Mathf.Sin(Time.time * 2.0f) * 0.1f, transform.position.z);
    }

    IEnumerator Despawn()
    {
        yield return new WaitForSeconds(10.0f);

        Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        Player.instance.cookies++;

        Destroy(gameObject);
    }
}
