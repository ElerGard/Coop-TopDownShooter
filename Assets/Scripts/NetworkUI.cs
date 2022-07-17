using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Unity.Netcode;

public class NetworkUI : MonoBehaviour
{
    public Button HostButton;
    public Button ClientButton;
    public Button ServerButton;
    public GameObject MultiplayerGUI;

    private void Awake()
    {
        HostButton.onClick.AddListener(() =>
        {
            if (NetworkManager.Singleton.StartHost())
            {
                Debug.Log("hosted");
                MultiplayerGUI.SetActive(false);
            }
        });
        ClientButton.onClick.AddListener(() =>
        {
            if (NetworkManager.Singleton.StartClient())
            {
                Debug.Log("Client");
                MultiplayerGUI.SetActive(false);

            }
        });
        ServerButton.onClick.AddListener(() =>
        {
            if (NetworkManager.Singleton.StartServer())
            {
                Debug.Log("Server");
                MultiplayerGUI.SetActive(false);
            }
        });
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
