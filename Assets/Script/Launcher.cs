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

        [Tooltip("닉네임 패널")]
        [SerializeField]
        private GameObject nicknamePanel;

        #endregion

        #region Private Fields

        string gameVersion = "1";

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

        // 연결이 끊겼을 경우 첫 번째 UI 화면을 띄웁니다.
        public override void OnDisconnected(DisconnectCause cause)
        {
            Debug.LogWarningFormat("연결이 끊겼습니다.", cause);
            OpenFirstCanvas();
        }

        // 방 입장에 실패했을 경우 첫 번째 UI 화면을 띄웁니다.
        public override void OnJoinRandomFailed(short returnCode, string message)
        {
            Debug.Log("랜덤 방 입장에 실패하였습니다.");
            OpenFirstCanvas();
        }

        // 룸에 입장할 경우 로그를 기록합니다.
        public override void OnJoinedRoom()
        {
            // 방에 처음으로 입장하는 경우는 방장이므로 룸을 생성했다는 로그를 띄웁니다.
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
            nicknamePanel.SetActive(false);
        }
        public void OpenCreateCanvas()
        {
            firstPanel.SetActive(false);
            createPanel.SetActive(true);
            titlePanel.SetActive(true);
            nicknamePanel.SetActive(true);
        }
        public void OpenJoinCanvas()
        {
            firstPanel.SetActive(false);
            joinPanel.SetActive(true);
            titlePanel.SetActive(true);
            nicknamePanel.SetActive(true);
        }
        public void JoinBroadCasting()
        {
            if (PhotonNetwork.IsConnected)
            {
                joinPanel.SetActive(false);
                titlePanel.SetActive(false);
                nicknamePanel.SetActive(false);
                PhotonNetwork.JoinRoom(GetInputText());
            }  
        }
        public void StartBroadcasting()
        {
            if (PhotonNetwork.IsConnected)
            {
                createPanel.SetActive(false);
                titlePanel.SetActive(false);
                nicknamePanel.SetActive(false);
                PhotonNetwork.CreateRoom(GetInputText(), new RoomOptions { MaxPlayers = maxPlayersPerRoom });
            }

        }
        public void JoinRandomBroadcasting()
        {
            if (PhotonNetwork.IsConnected)
            {
                joinPanel.SetActive(false);
                titlePanel.SetActive(false);
                nicknamePanel.SetActive(false);
                PhotonNetwork.JoinRandomRoom();
            }
        }

        #endregion

        #region Private Method

        // 현재는 방송 제목을 입력하여 방송에 참여하거나, 방송을 생성합니다.
        public string GetInputText()
        {
            // 내 닉네임 설정
            string nickname = nicknamePanel.GetComponent<InputField>().text;
            PhotonNetwork.NickName = nickname;

            // 방송 제목 리턴
            string roomTitle = titlePanel.GetComponent<InputField>().text;
            return roomTitle;
        }

        #endregion
    }

}

