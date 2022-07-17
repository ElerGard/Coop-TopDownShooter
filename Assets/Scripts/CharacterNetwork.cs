using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;


public class CharacterNetwork : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        if (!GetComponent<NetworkObject>().IsLocalPlayer)
        {
            GetComponent<CharacterMovement>().enabled = false;
            GetComponent<CharacterWeapon>().enabled = false;
        }
    }
}
