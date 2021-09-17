using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    GameManager GM;
    public bool isRun;
    public bool hitting;
    public bool touchable;
    public bool isFall;
    public bool rotate;
    public bool isShaking;
    public bool runEnem;
    public bool firstTake;


    public static int isHolding = 1;
    Vector3 dir;

    public Animator enemyAnim;
    public GameObject player;

    public GameObject sword;
    public Transform a;
    public int shakeSens;

    bool pivot1;
    public Rigidbody currentSwordRB;
    Tween tween;

    //health process
    public int currentHealth;
    public int maxHealth;
    public GameObject healthBarUI;
    public Slider sliderBar;


    //Collect item process
    public GameObject goldIgnot;
    void Start()
    {
        Vector3 piv = new Vector3(transform.position.x, transform.position.y + 2, transform.position.z);

        //goldIgnot.SetActive(false);
        currentHealth = 100;
        maxHealth = 100;
        sliderBar = transform.GetChild(1).GetChild(0).GetComponent<Slider>();
        healthBarUI = transform.GetChild(1).gameObject;
        sword = transform.GetChild(0).GetChild(0).GetChild(0).GetChild(2).GetChild(0).GetChild(0).GetChild(2).GetChild(0).GetChild(0).GetChild(0).GetChild(1).GetChild(0).GetChild(0).GetChild(1).gameObject;
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
            transform.position += Vector3.down * Time.deltaTime;
        }

        if (rotate)
        {
            sword.transform.LookAt(transform);

        }

        if (runEnem)
        {
            transform.position -= Vector3.back * 3 * Time.deltaTime;

        }

    }

    private float CalculateHealth()
    {
        return currentHealth / maxHealth;
    }

    public float distPlayerSword()
    {
        return Vector3.Distance(player.transform.position, sword.transform.position);
    }

    public float distPlayerEnemy()
    {
        return Vector3.Distance(player.transform.position, transform.position);
    }

    public Vector3 relativePosEnemySword()
    {
        return transform.position - sword.transform.position;
    }

    public Vector3 relativePosPlayerEnemy()
    {
        return player.transform.position - transform.position;
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Force" && !isShaking)
        {
            tween = sword.transform.DOShakeRotation(0.1f, 10, 1);
        }
        
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Electric" && !firstTake)
        {
            firstTake = true;
            tween.Pause();
            isShaking = true;
            rotate = true;
            currentSwordRB = sword.GetComponent<Rigidbody>();
            enemyRun();
            isRun = true;
            touchable = false;
            if (isHolding % 2 == 0)
            {
                sword.transform.DOMove(new Vector3(player.transform.position.x , player.transform.position.y + 0.5F, player.transform.position.z+1f), 0.2f).SetEase(GM.animCurv);
                isHolding++;
            }
            else
            {
                sword.transform.DOMove(new Vector3(player.transform.position.x, player.transform.position.y + 0.5F, player.transform.position.z +1f), 0.2f).SetEase(GM.animCurv);
                isHolding++;

            }

            sword.transform.parent = player.transform;


        }


        if (other.tag == "Sword" && touchable)
        {
            Debug.Log("girdi");
            isFall = true;
            enemyAnim.SetBool("isDeath", true);
            sword.SetActive(false);
            currentHealth = 0;
            transform.DOMove(new Vector3(-dir.x * 2f, transform.position.y - 0.5f, transform.position.z + 5f), 1f);
            getGold();

        }
        
    }



    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Electric")
        {
            currentSwordRB.AddForce(relativePosEnemySword() * 200f);
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
            runEnem = true;

        }
    }

    public void getGold()
    {
        goldIgnot = Instantiate(goldIgnot, new Vector3(transform.position.x,transform.position.y ,transform.position.z + 2f), Quaternion.identity);

    }

    public void GameİsOver()
    {
        int timePiv;
        GM.GameOver = true;
        GM.playerAnim.SetTrigger("PlayerDeath");

        player.transform.DOMove(new Vector3(player.transform.position.x, transform.position.y -1f, transform.position.z - 5f), 1f);
    }

}
