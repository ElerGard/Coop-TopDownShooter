using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class CharacterMovement : NetworkBehaviour
{
    private bool 
        stopMoveUp = false,
        stopMoveLeft = false, 
        stopMoveRight = false,
        stopMoveDown = false;


    [Header("Movement")]
    public float movementSpeed = 20f;

    public float rotationOffset = 270;

    private void Awake()
    {

    }

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

    private void FollowMouse()
    {
        Vector3 mousePos = Input.mousePosition;
        mousePos = Camera.main.ScreenToWorldPoint(mousePos);

        Vector3 direction = new Vector3(mousePos.x - transform.position.x, mousePos.y - transform.position.y);

        transform.up = direction;
    }

    private void Update()
    {

        if (!stopMoveUp && Input.GetKey("w"))
        {
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
    }


}
