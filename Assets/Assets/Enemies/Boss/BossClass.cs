using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossClass : EnemyClass
{
    Vector3 direction;
    bool isEntrer;
    bool isLeft;
    bool isRight;
    bool canRush;
    Vector3 leftStartPosition;
    Vector3 rightStartPosition;

    float rushCooldown;
    float resetTimeRush;
    Vector3 screenDimension;

    private Vector3 pos;
    private float t;
    private float frequency;
    private float amplitude;

    private float rushSpeed;
    private float rushFrequency;

    private float pauseStartRush;
    private float pauseFinishRush;

    // Start is called before the first frame update
    void Start()
    {
        enemyType = "Boss";
        bulletType = "Direct";
        immune = false;
        score = 100;
        cooldown = 4;

        canMove = true;
        canShoot = false;
        isLeft = false;
        isRight = true;
        canRush = false;
        direction = Vector3.zero;

        rushCooldown = 5f;
        resetTimeRush = Time.time + rushCooldown;
        pauseStartRush = 0;
        pauseFinishRush = 0;

        frequency = 0.8f;
        amplitude = 0.5f;

        screenDimension = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height));
        leftStartPosition = new Vector3(-screenDimension.x - 0.5f, 0, -1.2f);
        rightStartPosition = new Vector3(screenDimension.x + 0.5f, 0, -1.2f);

        PositionFromOut();
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
        Entrance();
        //Rush();

        Debug.Log(canMove);

        if (canShoot)
            Shoot();
    }

    /* Posizionamento del boss quando deve entrare in scena (inizio e dopo dash) */
    protected void PositionFromOut()
    {
        if (isRight)
        {
            transform.parent.position = rightStartPosition;
            transform.parent.rotation = new Quaternion(0f, 0f, 0f, 0f);
            transform.GetChild(0).localRotation = new Quaternion(0f, 0f, 0f, 0f);
        }
        else if (isLeft)
        {
            transform.parent.position = leftStartPosition;
            transform.parent.rotation = new Quaternion(0f, 180f, 0f, 0f);
            transform.GetChild(0).localRotation = new Quaternion(0f, 180f, 0f, 0f);
        }

        direction = Vector3.up;
        isEntrer = false;
    }

    /* Movimento d'ingresso del boss */
    private void Entrance()
    {
        if (canMove && !isEntrer)
        {
            if (isRight)
            {
                transform.parent.Translate(((new Vector3(screenDimension.x, 0, -1.2f)) - rightStartPosition).normalized * Time.deltaTime * 0.6f);

                if (transform.parent.position.x <= screenDimension.x)
                {
                    pos = transform.parent.position;
                    isEntrer = true;
                    canShoot = true;
                    canMove = true;
                }
            }
            else if (isLeft)
            {
                transform.parent.Translate(((new Vector3(-screenDimension.x, 0, -1.2f)) - leftStartPosition).normalized * Time.deltaTime * -0.6f);

                if (transform.parent.position.x >= -screenDimension.x)
                {
                    pos = transform.parent.position;
                    isEntrer = true;
                    canShoot = true;
                    canMove = true;
                }
            }

            resetTimeRush = Time.time + rushCooldown;
        }
    }


    protected void Rush ()
    {
        
        if (!canRush)
        {
            if ((Time.time - (pauseFinish - pauseStart)) > resetTimeRush)
            {
                pauseStart = 0;
                pauseFinish = 0;

                canRush = true;
            }
        }
        else
        {
            GetComponent<Animator>().SetBool("RushClick", true);
            canShoot = false;
            canMove = false;
            immune = true;

            transform.parent.transform.Translate(Vector3.left * 1.5f * Time.deltaTime);
            pos.y = transform.parent.position.y + 0.5f * Mathf.Sin(t);
            pos.x = transform.position.x;
            t += Time.deltaTime * 3f;
            transform.position = pos;

            if (transform.parent.position.x > rightStartPosition.x || transform.parent.position.x < leftStartPosition.x)
            {
                isLeft = !isLeft;
                isRight = !isRight;
                canRush = false;
                GetComponent<Animator>().SetBool("RushClick", false);
                PositionFromOut();
                Entrance();
            }
        }
    }

    protected void Movement ()
    {
        if (canMove && isEntrer)
        {
            if (healthPoint > 0)
            {
                if (!canRush)
                {
                    /* Movimento principale in fase di shooting,
                    * con andamento verticale e sinusoidale*/
                    pos.y = transform.parent.position.y + amplitude * Mathf.Sin(t);
                    pos.x = transform.position.x;
                    t += Time.deltaTime * frequency;
                    transform.position = pos;

                    if ((Time.time - (pauseFinishRush - pauseStartRush)) > resetTimeRush)
                    {
                        pauseStartRush = 0;
                        pauseFinishRush = 0;

                        canRush = true;
                    }
                }
                else
                {
                    /* Movimento del dash, sinusoidale con traslazione 
                     * lungo l'asse orizzontale dello schermo */
                    GetComponent<Animator>().SetBool("RushClick", true);
                    canShoot = false;
                    immune = true;

                    transform.parent.transform.Translate(Vector3.left * rushSpeed * Time.deltaTime);
                    pos.y = transform.parent.position.y + 0.5f * Mathf.Sin(t);
                    pos.x = transform.position.x;
                    t += Time.deltaTime * rushFrequency;
                    transform.position = pos;

                    if (transform.parent.position.x > rightStartPosition.x || transform.parent.position.x < leftStartPosition.x)
                    {
                        isLeft = !isLeft;
                        isRight = !isRight;
                        canRush = false;
                        GetComponent<Animator>().SetBool("RushClick", false);
                        PositionFromOut();
                        Entrance();
                    }
                }
                
            }
        }
    }

    protected void InstantiateBullet()
    {
        if (nextFire)
        {
            GameObject bullet = Instantiate(Resources.Load("Enemies/Bullets/DirectBullet"), transform.GetChild(0).position, transform.GetChild(0).rotation) as GameObject;
            bullet.GetComponent<BulletClass>().Shooter = gameObject;
            nextFire = false;
        }
    }

    override public void PauseOn()
    {
        canShoot = false;
        canMove = false;
        isEntrer = !isEntrer;
        pauseStart = Time.time;
        pauseStartRush = Time.time;
        GetComponent<Animator>().speed = 0f;
    }

    override public void PauseOff()
    {
        canShoot = true;
        canMove = true;
        isEntrer = !isEntrer;
        pauseFinish = Time.time;
        pauseFinishRush = Time.time;
        GetComponent<Animator>().speed = 1f;
    }

    public bool IsLeft
    {
        get { return isLeft; }
    }
    public bool IsRight
    {
        get { return isRight; }
    }
    public float Frequency { get => frequency; set => frequency = value; }
    public float RushFrequency { get => rushFrequency; set => rushFrequency = value; }
    public float RushSpeed { get => rushSpeed; set => rushSpeed = value; }
    public float RushCooldown { get => rushCooldown; set => rushCooldown = value; }
}
