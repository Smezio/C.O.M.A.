using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    public GameObject bullet;
    public Transform rightBulletSpawn;
    public Transform leftBulletSpawn;

    private PlayerMovement pm;

    private bool NextFire;
    private float cooldown;
    private float time;

    private AudioSource audioShoot;

    // Start is called before the first frame update
    void Start()
    {
        time = 2f;
        pm = GetComponent<PlayerMovement>();
        audioShoot = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        Shoot();
    }

    private void Shoot()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (Time.time > cooldown)
                NextFire = true;

            if (NextFire && Time.time > cooldown)
            {
                GetComponent<Animator>().SetBool("ShootClick", true);

                cooldown = Time.time + time;
            }
        }
    }

    /* Aggiungere evento nell'animazione */
    protected void DisableShootAnimation()
    {
        GetComponent<Animator>().SetBool("ShootClick", false);
    }

    protected void InstantiateBullet()
    {
        if (NextFire)
        {
            // Spawn Bullet
            var trans = pm.GetFacing() ? leftBulletSpawn : rightBulletSpawn;
            GameObject obj = Instantiate(bullet, trans.position, trans.rotation);

            // Flip bullet
            obj.GetComponent<SpriteRenderer>().flipX = pm.GetFacing();

            NextFire = false;

            audioShoot.Play();
        }
    }
}
