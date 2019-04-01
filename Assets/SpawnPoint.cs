//using System;
using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    public GameObject[] enemies;

    private int frameCount;
    private float timer;

    // Start is called before the first frame update
    void Start()
    {
        frameCount = 0;
        timer = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isActiveAndEnabled)
            return;

        frameCount++;

        SpriteRenderer renderer = GetComponent<SpriteRenderer>();
        if (frameCount % 15 == 0)
            renderer.enabled = !renderer.enabled;

        if (frameCount >= 120)
        {
            gameObject.SetActive(false);
            frameCount = 0;

            // TODO: INSTANTIATE ENEMY HERE!!!
            int index = Random.Range(0, enemies.Length - 1);
            Instantiate(enemies[index], transform.position, transform.rotation);
        }
    }
}
