﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameUIScript : MonoBehaviour
{
    PlayerClass player;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerClass>();
    }

    // Update is called once per frame
    void Update()
    {
        HealthUpdate();
    }

    private void HealthUpdate ()
    {
        if (player.HealthPoint >= 0 && player.HealthPoint < transform.GetChild(0).childCount)
        {
            for (int i = transform.GetChild(0).childCount; i > player.HealthPoint; i--)
                transform.GetChild(0).GetChild(i - 1).gameObject.SetActive(false);
        }
    }
}
