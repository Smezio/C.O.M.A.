using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIScript : MonoBehaviour
{
    PlayerStatus player;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStatus>();
    }

    // Update is called once per frame
    void Update()
    {
        if (player.GetHealthPoint() < transform.childCount)
        {
            for (int i = transform.childCount; i > player.GetHealthPoint(); i--)
                transform.GetChild(i-1).gameObject.SetActive(false);
        }
    }
}
