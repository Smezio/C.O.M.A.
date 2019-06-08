//using System;
using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    public GameObject[] enemies;
    private GameObject enemy;
    private int index;
    private SceneControlScript levelControl;

    // Start is called before the first frame update
    void Start()
    {
        levelControl = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<SceneControlScript>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void EnemySpawning()
    {
        GetComponent<Animator>().enabled = false;
        GetComponent<SpriteRenderer>().sprite = null;
        index = Random.Range(0, enemies.Length);
        enemy = Instantiate(enemies[index], transform.position, transform.rotation);

        if (enemy.name.Contains("Circle"))
        {
            enemy.GetComponentInChildren<EnemyCircleClass>().Facing(gameObject.name.Substring(gameObject.name.Length - 2));
            enemy.GetComponentInChildren<EnemyCircleClass>().Speed = levelControl.CurrentLevel.EnemyCircleSpeed;
            enemy.GetComponentInChildren<EnemyCircleClass>().Frequency = levelControl.CurrentLevel.EnemyCircleFrequency;
            enemy.GetComponentInChildren<EnemyCircleClass>().Cooldown = levelControl.CurrentLevel.EnemyCircleCooldown;
        }
        else if (enemy.name.Contains("Line"))
        {
            enemy.GetComponentInChildren<EnemyLineClass>().Facing(gameObject.name.Substring(gameObject.name.Length - 2));
            enemy.GetComponentInChildren<EnemyLineClass>().Speed = levelControl.CurrentLevel.EnemyLineSpeed;
            enemy.GetComponentInChildren<EnemyLineClass>().Cooldown = levelControl.CurrentLevel.EnemyLineCooldown;
        }
        else if (enemy.name.Contains("Dash"))
        {
            enemy.GetComponentInChildren<EnemyDashClass>().Facing(gameObject.name.Substring(gameObject.name.Length - 2));
            enemy.GetComponentInChildren<EnemyDashClass>().Speed = levelControl.CurrentLevel.EnemyDashSpeed;
            enemy.GetComponentInChildren<EnemyDashClass>().Frequency = levelControl.CurrentLevel.EnemyDashFrequency;
            enemy.GetComponentInChildren<EnemyDashClass>().Cooldown = levelControl.CurrentLevel.EnemyDashCooldown;
        }
        else if (enemy.name.Contains("Charger"))
            enemy.GetComponentInChildren<EnemyChargerClass>().Speed = levelControl.CurrentLevel.EnemyDashSpeed;

    }
}
