using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Game : MonoBehaviourPunCallbacks
{
    [SerializeField]
    private TMP_Text roomCode;

    // Start is called before the first frame update
    void Start()
    {
        PhotonNetwork.ConnectUsingSettings();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public override void OnConnectedToMaster()
    {
        PhotonNetwork.JoinLobby();
    }

    public override void OnJoinedLobby()
    {
        PhotonNetwork.CreateRoom(Random.Range(1000, 9999).ToString());
    }

    public override void OnCreatedRoom()
    {
        PhotonNetwork.CurrentRoom.MaxPlayers = 2;
        roomCode.text = PhotonNetwork.CurrentRoom.Name;
        roomCode.rectTransform.anchoredPosition = new Vector2(5, 5);
        roomCode.rectTransform.sizeDelta = new Vector2(60, 35);
        roomCode.rectTransform.anchorMin = Vector2.zero;
        roomCode.rectTransform.anchorMax = Vector2.zero;
        roomCode.rectTransform.pivot = Vector2.zero;
        roomCode.alignment = TextAlignmentOptions.Left;
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {

    }
}
