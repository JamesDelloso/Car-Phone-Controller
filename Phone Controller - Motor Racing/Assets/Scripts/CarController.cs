using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CarController : MonoBehaviourPunCallbacks
{
    [SerializeField]
    private GameObject panel;
    [SerializeField]
    private GameObject InputTrackerPrefab;

    // Start is called before the first frame update
    void Start()
    {
        PhotonNetwork.ConnectUsingSettings();
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void joinRoom(TMP_InputField code)
    {
        PhotonNetwork.JoinRoom(code.text);
        code.text = "";
    }

    public override void OnConnectedToMaster()
    {
        PhotonNetwork.JoinLobby();
    }

    public override void OnJoinedRoom()
    {
        panel.SetActive(false);
        PhotonNetwork.Instantiate(InputTrackerPrefab.name, Vector3.zero, Quaternion.identity);
    }

    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        PhotonNetwork.LeaveRoom();
        panel.SetActive(true);
    }
}
