using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class GameServer : MonoBehaviour
{

    public NetworkObject CreateBulletServerRpc(Transform firePoint, GameObject bulletPrefab, float bulletSpeed)
    {
        GameObject bul = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        return bul.GetComponent<NetworkObject>();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
