using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class MagnetProcess : MonoBehaviour
{
    public GameObject player;
    public GameObject bag;
    public int point;
    public int score;
    public int piv;
    private void Start()
    {
        player = GameObject.Find("PLAYER");
        bag = GameObject.Find("Bag");
    }


    private void Update()
    {


    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "goldbar")
        {
            PlayerPrefs.SetFloat("score", PlayerPrefs.GetFloat("score") + 0.1f);
            other.transform.SetParent(bag.transform);
            //player.transform.GetChild(0).GetChild(1).GetComponent<Renderer>().material.color = new Color(piv++, 178, 0);


        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "goldbar" && other.transform.position.y > 0.01f)
        {
            StartCoroutine(Collect(other));

        }
    }

    IEnumerator Collect(Collider other)
    {
        other.transform.DOMove(new Vector3(player.transform.position.x, player.transform.position.y + 0.5f, player.transform.position.z), 0.5f);
        yield return new WaitForSeconds(0.5f);

        other.gameObject.SetActive(false);

    }

}
