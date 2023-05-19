using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnPlayer : MonoBehaviour
{
    public Vector2 spawnPos;

    // Start is called before the first frame update
    void Start()
    {
        spawnPos = GameObject.Find("Player").transform.position;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
            collision.gameObject.transform.position = spawnPos;
    }
}
