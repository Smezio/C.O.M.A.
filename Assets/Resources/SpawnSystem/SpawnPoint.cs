//using System;
using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    public GameObject[] enemies;
    private GameObject enemy;
    private int index;

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

        /* INTERMITTENZA DA SOSTITUIRE CON ANIMAZIONE */
        SpriteRenderer renderer = GetComponent<SpriteRenderer>();
        if (frameCount % 15 == 0)
            renderer.enabled = !renderer.enabled;

        if (frameCount >= 120)
        {
            gameObject.SetActive(false);
            frameCount = 0;

            EnemySpawning();
        }
    }

    public void EnemySpawning()
    {
        index = Random.Range(0, enemies.Length - 1);
        enemy = Instantiate(enemies[index], transform.position, transform.rotation);

        if (enemy.name.Contains("Circle"))
            enemy.GetComponentInChildren<EnemyCircleClass>().Facing(gameObject.name.Substring(gameObject.name.Length - 2));
        else if (enemy.name.Contains("Line"))
            enemy.GetComponentInChildren<EnemyLineClass>().Facing(gameObject.name.Substring(gameObject.name.Length - 2));
        else if (enemy.name.Contains("Dash"))
            enemy.GetComponentInChildren<EnemyDashClass>().Facing(gameObject.name.Substring(gameObject.name.Length - 2));
    }
}
