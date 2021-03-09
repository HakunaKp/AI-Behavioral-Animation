using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    private float moveSpeed;

    private Rigidbody myRigidbody;

    private Vector3 moveInput;
    private Vector3 moveVelocity;

    private Camera mainCamera;

    public GunController GUN;
    
    // player can toggle wall transparency to move through it
    public GameObject Wall;
    private BoxCollider ghostWall;

    // players Fov will detect for coins and automatically collect
    public GameObject GoldCoins;
    private Transform coins;
    private float fovRadius = 2f;
    private float coinDist;

    public GameObject WinText;
    private Text win;
    
    public GameObject LoseText;
    private Text lose;
    
    // Start is called before the first frame update
    void Start()
    {
        myRigidbody = GetComponent<Rigidbody>();
        mainCamera = FindObjectOfType<Camera>();
        ghostWall = Wall.GetComponent<BoxCollider>();
        coins = GoldCoins.GetComponent<Transform>();
        moveSpeed = 6f;
        win = WinText.GetComponent<Text>();
        lose = LoseText.GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!win.enabled && !lose.enabled) {
            // check for win state
            coinDist = Vector3.Distance(coins.position, transform.position);
            if (coinDist <= fovRadius) {
                win.enabled = true;
                Destroy(GoldCoins);
            }

        }

        if (!win.enabled && !lose.enabled) {
            coinDist = Vector3.Distance(coins.position, transform.position);
            if (coinDist <= fovRadius) {
                win.enabled = true;
                Destroy(GoldCoins);
            }
        }

        moveInput = new Vector3(Input.GetAxisRaw("Horizontal"), 0f, Input.GetAxisRaw("Vertical"));
        moveVelocity = moveInput * moveSpeed;

        Ray cameraRay = mainCamera.ScreenPointToRay(Input.mousePosition);
        Plane groundPlane = new Plane(Vector3.up, Vector3.zero);
        float rayLength;

        if (groundPlane.Raycast(cameraRay, out rayLength))
        {
            Vector3 pointToLook = cameraRay.GetPoint(rayLength);
            transform.LookAt(new Vector3(pointToLook.x, pointToLook.y, pointToLook.z));
        }

        if (Input.GetMouseButtonDown(0))
            GUN.isFiring = true;

        if (Input.GetMouseButtonUp(0))
            GUN.isFiring = false;

        if (Input.GetKeyDown(KeyCode.Space)){
            if (ghostWall.enabled) {
                ghostWall.enabled = false;
            } else ghostWall.enabled = true;
        }
    }

    void FixedUpdate() {
        myRigidbody.velocity = moveVelocity;
    }

    public void AdjustSpeed(float newSpeed) {
        moveSpeed = newSpeed;
    }
}
