using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class GameServer : NetworkBehaviour
{
    private GameObject obj;
    public GameObject enemyPref;

    [ServerRpc]
    public void SpawnEnemyServerRpc()
    {
        obj = Instantiate(enemyPref, Vector3.zero, Quaternion.identity);
        obj.GetComponent<NetworkObject>().Spawn(true);
    }
    // Start is called before the first frame update
    void Start()
    {
        if (!IsOwnedByServer)
        {
            Debug.Log(IsOwnedByServer);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
