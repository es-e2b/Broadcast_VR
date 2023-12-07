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
        [Tooltip("�ִ� ���� �ο�")]
        [SerializeField]
        private byte maxPlayersPerRoom = 10;

        [Tooltip("ù ��° ���� ȭ��")]
        [SerializeField]
        private GameObject firstPanel;
        [Tooltip("��� ���� ���� ȭ��")]
        [SerializeField]
        private GameObject createPanel;
        [Tooltip("��� ���� ���� ȭ��")]
        [SerializeField]
        private GameObject joinPanel;

        [Tooltip("��� ���� �г�")]
        [SerializeField]
        private GameObject titlePanel;

        [Tooltip("�г��� �г�")]
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

        // ������ ������ ��� ù ��° UI ȭ���� ���ϴ�.
        public override void OnDisconnected(DisconnectCause cause)
        {
            Debug.LogWarningFormat("������ ������ϴ�.", cause);
            OpenFirstCanvas();
        }

        // �� ���忡 �������� ��� ù ��° UI ȭ���� ���ϴ�.
        public override void OnJoinRandomFailed(short returnCode, string message)
        {
            Debug.Log("���� �� ���忡 �����Ͽ����ϴ�.");
            OpenFirstCanvas();
        }

        // �뿡 ������ ��� �α׸� ����մϴ�.
        public override void OnJoinedRoom()
        {
            // �濡 ó������ �����ϴ� ���� �����̹Ƿ� ���� �����ߴٴ� �α׸� ���ϴ�.
            if (PhotonNetwork.CurrentRoom.PlayerCount == 1)
            {
                Debug.Log("��� ���� �����Ͽ����ϴ�.");

                PhotonNetwork.LoadLevel("Room");
            }
            else
                Debug.Log("��� �뿡 �����Ͽ����ϴ�.");
        }
        #endregion

        #region MonoBehaviour CallBacks

        private void Awake() // ���� ���۽� ���� ��Ʈ��ũ ����.
        {
            PhotonNetwork.GameVersion = gameVersion;
            PhotonNetwork.ConnectUsingSettings();
            PhotonNetwork.AutomaticallySyncScene = true;
        }
        void Start() // ù UI �г� ����
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

        // ����� ��� ������ �Է��Ͽ� ��ۿ� �����ϰų�, ����� �����մϴ�.
        public string GetInputText()
        {
            // �� �г��� ����
            string nickname = nicknamePanel.GetComponent<InputField>().text;
            PhotonNetwork.NickName = nickname;

            // ��� ���� ����
            string roomTitle = titlePanel.GetComponent<InputField>().text;
            return roomTitle;
        }

        #endregion
    }

}

