using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterWeapon : MonoBehaviour
{
    public Transform firePoint;
    public Rigidbody2D bulletPrefab;
    public float bulletSpeed = 10f;
    private void Fire()
    {
        Vector3 mousePos = Input.mousePosition;
        mousePos = Camera.main.ScreenToWorldPoint(mousePos);
        mousePos.z = 0;

        Rigidbody2D bul = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        bul.AddForce(firePoint.up * bulletSpeed, ForceMode2D.Impulse);


    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Fire();
        }
    }
}
