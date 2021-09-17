using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRange : MonoBehaviour
{
    GameObject currentEnemy;
    GameManager GM;
    private void Start()
    {
        currentEnemy = transform.parent.root.gameObject;
        GM = GameObject.Find("GAMEMANAGER").GetComponent<GameManager>();

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" && !currentEnemy.GetComponent<Enemy>().firstTake)
        {
            currentEnemy.transform.GetComponent<Enemy>().enemyAnim.SetTrigger("enemyAtack");
            currentEnemy.GetComponent<Enemy>().GameİsOver();
        }
    }
}
