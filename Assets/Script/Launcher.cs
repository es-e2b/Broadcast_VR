using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;
using Unity.XR.CoreUtils;

namespace Broadcast.JES
{
    public class Launcher : MonoBehaviourPunCallbacks
    {
        #region Private Serializable Fields
        [Tooltip("최대 참여 인원")]
        [SerializeField]
        private byte maxPlayersPerRoom = 10;

        [Tooltip("첫 번째 선택 화면")]
        [SerializeField]
        private GameObject firstPanel;
        [Tooltip("방송 시작 선택 화면")]
        [SerializeField]
        private GameObject createPanel;
        [Tooltip("방송 입장 선택 화면")]
        [SerializeField]
        private GameObject joinPanel;

        [Tooltip("방송 제목 패널")]
        [SerializeField]
        private GameObject titlePanel;

        #endregion

        #region Private Fields

        string gameVersion = "1";

        bool isConnecting; // 현재 상태 출력

        #endregion

        #region Photon CallBacks
        /*
        public override void OnConnectedToMaster()
        {
            if (isConnecting)
            {
                Debug.Log("PUN Basics Tutorial/Launcher: OnConnectedToMaster() was called by PUN");
                PhotonNetwork.JoinRandomRoom();
            }
        }
        */
        public override void OnDisconnected(DisconnectCause cause)
        {
            Debug.LogWarningFormat("연결이 끊겼습니다.", cause);
            OpenFirstCanvas();
            isConnecting = false;
        }
        public override void OnJoinRandomFailed(short returnCode, string message)
        {
            Debug.Log("랜덤 방 입장에 실패하였습니다.");
            OpenFirstCanvas();
            isConnecting = false;
        }

        public override void OnJoinedRoom()
        {
            if (PhotonNetwork.CurrentRoom.PlayerCount == 1)
            {
                Debug.Log("방송 룸을 생성하였습니다.");

                PhotonNetwork.LoadLevel("Room");
            }
            else
                Debug.Log("방송 룸에 입장하였습니다.");
        }
        #endregion

        #region MonoBehaviour CallBacks

        private void Awake() // 게임 시작시 포톤 네트워크 접속.
        {
            PhotonNetwork.GameVersion = gameVersion;
            PhotonNetwork.ConnectUsingSettings();
            PhotonNetwork.AutomaticallySyncScene = true;
        }
        void Start() // 첫 UI 패널 생성
        {
            OpenFirstCanvas();
        }

        #endregion

        #region Public Methods

        public void OpenFirstCanvas()
        {
            firstPanel.SetActive(true);
            joinPanel.SetActive(false);
            createPanel.SetActive(false);
            titlePanel.SetActive(false);
        }
        public void OpenCreateCanvas()
        {
            firstPanel.SetActive(false);
            createPanel.SetActive(true);
            titlePanel.SetActive(true);
        }
        public void OpenJoinCanvas()
        {
            firstPanel.SetActive(false);
            joinPanel.SetActive(true);
            titlePanel.SetActive(true);
        }
        public void JoinBroadCasting()
        {
            isConnecting = true;
            if (PhotonNetwork.IsConnected)
            {
                joinPanel.SetActive(false);
                titlePanel.SetActive(false);
                PhotonNetwork.JoinRoom(GetInputText());
            }  
        }
        public void StartBroadcasting()
        {
            isConnecting = true;
            if (PhotonNetwork.IsConnected)
            {
                createPanel.SetActive(false);
                titlePanel.SetActive(false);
                PhotonNetwork.CreateRoom(GetInputText(), new RoomOptions { MaxPlayers = maxPlayersPerRoom });
            }

        }
        public void JoinRandomBroadcasting()
        {
            isConnecting = true;
            if (PhotonNetwork.IsConnected)
            {
                joinPanel.SetActive(false);
                titlePanel.SetActive(false);
                PhotonNetwork.JoinRandomRoom();
            }
        }

        #endregion

        #region Private Method

        public string GetInputText()
        {
            string roomTitle = titlePanel.GetComponent<InputField>().text;
            return roomTitle;
        }

        #endregion
    }

}

