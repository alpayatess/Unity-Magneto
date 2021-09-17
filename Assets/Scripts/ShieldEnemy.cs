using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using System;

public class ShieldEnemy : MonoBehaviour
{
    GameManager GM;

    public bool isRun;
    public bool hitting;
    public bool touchable;
    public bool isFall;

    public bool rotate;

    public bool isShaking;

    public static int isHolding = 1;
    Vector3 dir;

    public Animator enemyAnim;
    public GameObject player;

    public GameObject shield;
    public int shakeSens;

    bool pivot1;
    public Rigidbody currentSwordRB;
    Tween tween;


    //health process
    public int currentHealth;
    public int maxHealth;
    public GameObject healthBarUI;
    public Slider sliderBar;



    void Start()
    {
        currentHealth = 100;
        maxHealth = 100;
        sliderBar = transform.GetChild(1).GetChild(0).GetComponent<Slider>();
        healthBarUI = transform.GetChild(1).gameObject;
        shield = transform.GetChild(0).GetChild(0).GetChild(0).GetChild(2).GetChild(0).GetChild(0).GetChild(0).GetChild(0).GetChild(1).gameObject;
        GM = GameObject.Find("GAMEMANAGER").GetComponent<GameManager>();
        enemyAnim = transform.GetChild(0).GetComponent<Animator>();
        player = GameObject.Find("PLAYER");


    }



    void Update()
    {
        dir = player.transform.position - transform.position;
        sliderBar.value = CalculateHealth();



        if (transform.position.x > 3.5f || transform.position.x < -3.5f)
        {
            transform.position += Vector3.down * 2 * Time.deltaTime;
        }

        if (rotate)
        {
            shield.transform.LookAt(transform);

        }


    }

    private float CalculateHealth()
    {
        return currentHealth / maxHealth;
    }


    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Force" && !isShaking)
        {
            tween = shield.transform.DOShakeRotation(0.1f, 10, 1);
        }

        if (other.tag == "Electric")
        {
            Camera.main.DOShakeRotation(0.1f, 0.1f);
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Electric")
        {
            tween.Pause();
            isShaking = true;
            rotate = true;
            currentSwordRB = shield.GetComponent<Rigidbody>();
            enemyRun();
            isRun = true;
            touchable = false;
            if (isHolding % 2 == 0)
            {
                shield.transform.DOMove(new Vector3(player.transform.position.x, player.transform.position.y + 0.5F, player.transform.position.z + 1f), 0.2f).SetEase(GM.animCurv);
                Enemy.isHolding++;
                Debug.Log(isHolding);
            }
            else
            {
                shield.transform.DOMove(new Vector3(player.transform.position.x, player.transform.position.y + 0.5F, player.transform.position.z + 1f), 0.2f).SetEase(GM.animCurv);
                Enemy.isHolding++;

            }
            shield.transform.parent = player.transform;

        }


        if (other.tag == "Sword" && touchable)
        {
            isFall = true;
            enemyAnim.SetBool("isDeath", true);
            shield.SetActive(false);
            currentHealth = 0;
            transform.DOMove(new Vector3(-dir.x * 2f, transform.position.y - 0.5f, transform.position.z + 5f), 1f);
            
        }
    }



    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Electric")
        {
            currentSwordRB.AddForce(relativePosEnemySword() * 300f);
            touchable = true;
            rotate = false;
        }
    }

    public void enemyRun()
    {
        transform.GetChild(0).GetComponent<Animator>().SetTrigger("NonSword");
        transform.position -= Vector3.back * Time.deltaTime;
        StartCoroutine(TimeDelay1());
    }


    IEnumerator TimeDelay1()
    {

        yield return new WaitForSeconds(1.40f);
        if (!isFall)
        {
            transform.rotation = new Quaternion(transform.rotation.x, 180f, transform.rotation.z, transform.rotation.w);

        }
    }

    public float distPlayerSword()
    {
        return Vector3.Distance(player.transform.position, shield.transform.position);
    }

    public float distPlayerEnemy()
    {
        return Vector3.Distance(player.transform.position, transform.position);
    }

    public Vector3 relativePosEnemySword()
    {
        return transform.position - shield.transform.position;
    }

    public Vector3 relativePosPlayerEnemy()
    {
        return player.transform.position - transform.position;
    }
}
