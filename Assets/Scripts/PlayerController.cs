using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
using Unity.Networking;

enum PlayerState
{
    NO_ACTION,
    FIRING
}

public class PlayerController : NetworkBehaviour
{
    private PlayerState playerState = PlayerState.NO_ACTION;

    [Header("Weapon")]
    public Transform firePoint;
    public GameObject bulletPrefab;
    public float bulletSpeed = 10f;
    
    private bool
        stopMoveUp = false,
        stopMoveLeft = false,
        stopMoveRight = false,
        stopMoveDown = false;

    [Header("Movement")]
    [SerializeField] public float movementSpeed = 10f;

    [Header("Network")]
    GameObject net;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == "UpWall")
        {
            stopMoveUp = true;
        }
        if (collision.gameObject.name == "DownWall")
        {
            stopMoveDown = true;
        }
        if (collision.gameObject.name == "LeftWall")
        {
            stopMoveLeft = true;
        }
        if (collision.gameObject.name == "RightWall")
        {
            stopMoveRight = true;
        }
    }

    private void FollowMouse()
    {
        Vector3 mousePos = Input.mousePosition;
        mousePos = Camera.main.ScreenToWorldPoint(mousePos);

        Vector3 direction = new Vector3(mousePos.x - transform.position.x, mousePos.y - transform.position.y);

        transform.up = direction;
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.name == "UpWall")
        {
            stopMoveUp = false;
        }
        if (collision.gameObject.name == "DownWall")
        {
            stopMoveDown = false;
        }
        if (collision.gameObject.name == "LeftWall")
        {
            stopMoveLeft = false;
        }
        if (collision.gameObject.name == "RightWall")
        {
            stopMoveRight = false;
        }

    }

    [ServerRpc]
    private void FireServerRpc()
    {
        Vector3 mousePos = Input.mousePosition;
        mousePos = Camera.main.ScreenToWorldPoint(mousePos);
        mousePos.z = 0;

        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        var bulletRb = bullet.GetComponent<Rigidbody2D>();
        bulletRb.AddForce(firePoint.up * bulletSpeed, ForceMode2D.Impulse);
        bullet.GetComponent<NetworkObject>().Spawn(true);
        Destroy(bullet, 1f);
    }

    private void Awake()
    {
        net = GameObject.Find("NetworkCreator");
    }

    void Start()
    {
        if (!GetComponent<NetworkObject>().IsLocalPlayer)
        {
            GetComponent<PlayerController>().enabled = false;
        }
    }

    void Update()
    {
        if (!stopMoveUp && Input.GetKey("w"))
        {
            Debug.Log("w");

            transform.position += new Vector3(0, Time.deltaTime * movementSpeed, 0);
        }
        if (!stopMoveDown && Input.GetKey("s"))
        {
            transform.position += new Vector3(0, Time.deltaTime * -movementSpeed, 0);
        }
        if (!stopMoveLeft && Input.GetKey("a"))
        {
            transform.position += new Vector3(Time.deltaTime * -movementSpeed, 0, 0);
        }
        if (!stopMoveRight && Input.GetKey("d"))
        {
            transform.position += new Vector3(Time.deltaTime * movementSpeed, 0, 0);
        }

        FollowMouse();

        if (Input.GetMouseButtonDown(0))
        {
            FireServerRpc();
        }
    }
}
