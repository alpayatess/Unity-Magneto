using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    private float speed = 5.5f;
    private float axisSpeed = 20f;
    public float range = 5.0f;
    GameManager GM;

    Vector2 swipePosFirst;
    Vector2 swipePosSecond;
    Vector2 currentSwipe;

    public GameObject electricField;
    public GameObject electric;
    public GameObject force;




    private void Start()
    {
        GM = GameObject.Find("GAMEMANAGER").GetComponent<GameManager>();
        electricField = GameObject.Find("Electric").GetComponent<MagnetProcess>().gameObject;
        electric = electricField.transform.parent.GetChild(2).gameObject;
        force = GameObject.Find("Force");
    }



    private void Update()
    {
        if (GM.isGameStarted && !GM.GameOver)
        {
            electricField.transform.position = new Vector3(transform.position.x, 0.1f, transform.position.z);
            force.transform.position = new Vector3(transform.position.x, 0.1f, transform.position.z);
            electric.transform.position = new Vector3(transform.position.x, 1f, transform.position.z);

            RegularMove();
            AxisMove();
        }
        
    }

    public void RegularMove()
    {
        transform.position += transform.forward * speed * Time.deltaTime;
        Camera.main.transform.Translate(transform.forward * Time.deltaTime * speed, Space.World);
    }

    public void AxisMove()
    {
        if (Input.GetMouseButton(0))
        {
            swipePosFirst = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
            Camera.main.DOShakeRotation(1f, 0.1f, 5);

            if (swipePosSecond != Vector2.zero)
            {
                currentSwipe = swipePosFirst - swipePosSecond;
                // sağ sol

                if (currentSwipe.x > -10f && currentSwipe.x < 10f)
                {
                    if (currentSwipe.x > 0.8f && transform.position.x < 2.3f)
                    {
                        transform.position += Vector3.right * axisSpeed * Time.deltaTime;
                    }
                    if (currentSwipe.x < -0.8f && -transform.position.x < 2.3f)
                    {
                        transform.position += Vector3.left * axisSpeed * Time.deltaTime;
                    }
                }

                if (electricField.transform.localScale.x < 25f)
                {
                    electricField.transform.localScale += new Vector3(10f, 0.001f, 10f) * Time.deltaTime;
                    electric.transform.localScale += new Vector3(1f, 1f, 1f) * Time.deltaTime;
                    if (force.transform.localScale.x < 45f)
                    {
                        force.transform.localScale += new Vector3(electricField.transform.localScale.x * 3f, 0.001f, electricField.transform.localScale.z * 3f) * Time.deltaTime;
                    }

                }


            }
            swipePosSecond = swipePosFirst;

        }   
        else
        {
            if (electricField.transform.localScale.x > 0.2f)
            {
                electricField.transform.localScale -= new Vector3(100f, 0.001f, 100f) * Time.deltaTime;
                

            }
            if (force.transform.localScale.x > 0.2f)
            {
                force.transform.localScale -= new Vector3(150f, 0.001f, 150f) * Time.deltaTime;

            }
            if (electric.transform.localScale.x > 0.2f)
            {
                electric.transform.localScale -= new Vector3(10f, 10f, 10f) * Time.deltaTime;
            }
        }
        if (Input.GetMouseButtonUp(0))
        {
            swipePosSecond = Vector2.zero;
        }
    }



    private void OnTriggerEnter(Collider other)
    {

        if (other.tag == "block")
        {

        }

        if (other.tag == "Finish")
        {

        }
    }

    

}
