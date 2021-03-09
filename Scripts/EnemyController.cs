using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyController : MonoBehaviour
{
    private float moveSpeed = 3.5f;
    private float rotSpeed = 90f;

    private bool isWandering = false;
    private bool isRotatingLeft = false;
    private bool isRotatingRight = false;
    private bool isWalking = false;

    private Transform player;
    private float chaseSpeed = 10f;
    private float maxAngle = 60f;
    private float maxRadius = 4f;

    private float loseRadius = 1f;
    private float enemyDist;
    public GameObject WinText;
    private Text win;
    
    public GameObject LoseText;
    private Text lose;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        win = WinText.GetComponent<Text>();
        lose = LoseText.GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        // if player is detected, chase player
        if (FOVDetection.inFOV(transform, player, maxAngle, maxRadius)) {
            transform.LookAt(player);
            GetComponent<Rigidbody>().AddForce(transform.forward * chaseSpeed);
            
            if (!win.enabled && !lose.enabled) {
                // check for lose state
                enemyDist = Vector3.Distance(player.position, transform.position);
                if (enemyDist <= loseRadius){
                    lose.enabled = true;
                }
            }


        // otherwise continue wandering
        } else {
            if (isWandering == false)
            {
                StartCoroutine(Wander());
            } else {
                if (isRotatingRight == true)
                {
                    transform.Rotate(transform.up * Time.deltaTime * rotSpeed);
                }
                if (isRotatingLeft == true)
                {
                    transform.Rotate(transform.up * Time.deltaTime * -rotSpeed);
                }
                if (isWalking == true)
                {
                    transform.position += transform.forward * moveSpeed * Time.deltaTime;
                } 
            }
        }
    }

    IEnumerator Wander()
    {
        int rotTime = Random.Range(1,2);
        int rotateWait = Random.Range(1, 2);
        int rotateLorR = Random.Range(0, 3);
        int walkWait = Random.Range(1, 2);
        int walkTime = Random.Range(1, 2);

        isWandering = true;

        yield return new WaitForSeconds(walkWait);
        isWalking = true;
        yield return new WaitForSeconds(walkTime);
        isWalking = false;
        yield return new WaitForSeconds(rotateWait);
        if (rotateLorR == 1)
        {
            isRotatingRight = true;
            yield return new WaitForSeconds(rotTime);
            isRotatingRight = false;
        }
        if (rotateLorR == 2)
        {
            isRotatingRight = true;
            yield return new WaitForSeconds(rotTime);
            isRotatingRight = false;   
        }
        isWandering = false;
    }
}
