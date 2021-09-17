using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    //Ground
    private GameObject ground;
    Vector3 nextFloorPos;
    private List<GameObject> activeTiles = new List<GameObject>();
    public int tileCount = 5;
    private GameObject finalGround;

    //Enemy
    public GameObject enemy;


    public Animator playerAnim;
    public AnimationCurve animCurv;

    //UI Process
    public Text scoreText;
    public GameObject endPanel;
    public GameObject finishPanel;
    public GameObject startPanel;
    public bool isGameStarted;
    public bool GameOver;


    Enemy enemyProcess;
    void Start()
    {     
        endPanel = GameObject.Find("endPanel");
        endPanel.SetActive(false);
        finishPanel = GameObject.Find("finishPanel");
        finishPanel.SetActive(false);
        startPanel = GameObject.Find("startPanel");
        scoreText = GameObject.Find("score").GetComponent<Text>();
        PlayerPrefs.SetInt("score", 0);
        ground = GameObject.Find("zeminn");
        finalGround = GameObject.Find("finalZeminn");
        nextFloorPos.z = 7;
        playerAnim = GameObject.Find("PLAYER").transform.GetChild(0).GetComponent<Animator>();

        for (int i = 0; i < 20; i++)
        {
            TileSpawner();


        }
    }

    void Update()
    {
        if (isGameStarted == false && PlayerPrefs.GetFloat("score") == 0)
        {
            startTheGame();
        }

        scoreText.text = PlayerPrefs.GetFloat("score").ToString();

    }

    public void TileSpawner()
    {
        GameObject go = Instantiate(ground, nextFloorPos, Quaternion.identity);
        activeTiles.Add(go);
        nextFloorPos.z += 7;
        tileCount++;
    }

    public void DeleteTiles()
    {
        Destroy(activeTiles[0], 100f);
        activeTiles.RemoveAt(0);
    }



    public void FinalTileSpawn()
    {
        Instantiate(finalGround, nextFloorPos, Quaternion.identity);
        nextFloorPos.z += 7;

    }

    public void BackToLevelMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void OpenLevel()
    {
        PlayerPrefs.SetInt("chosenlvl", int.Parse(GameObject.Find(EventSystem.current.currentSelectedGameObject.name).GetComponent<Button>().GetComponentInChildren<Text>().text));
        SceneManager.LoadScene(PlayerPrefs.GetInt("chosenlvl"));

    }

    public void startTheGame()
    {
        if (Input.GetMouseButtonDown(0) && isGameStarted == false)
        {
            playerAnim.SetBool("start", true);
            isGameStarted = true;
            startPanel.SetActive(false);
        }
    }

    

}
