using Photon.Pun.Demo.PunBasics;
using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Realtime;
using UnityEngine.SceneManagement;

namespace Broadcast.JES
{
    public class GameManager : MonoBehaviourPunCallbacks
    {
        #region Public Variables region
        // ������ �÷��̾� �������� �ۺ� ������ �޾��ݴϴ�.
        [Tooltip("�÷��̾� ������")]
        public GameObject StreamerPrefab;
        public GameObject ViewerPrefab;
        #endregion
        #region Photon CallBacks

        // �濡�� ������ �κ� ���� �ҷ��ɴϴ�.
        public override void OnLeftRoom()
        {
            SceneManager.LoadScene(0); // �濡�� ������ �κ� �� �ε�
        }

        // �濡 ����� �� �÷��̾� �ν��Ͻ��� ���ٸ� �������ݴϴ�.
        public override void OnJoinedRoom()
        {
            //base.OnJoinedRoom();
            if (PhotonNetwork.InRoom && PlayerManager.LocalPlayerInstance == null)
            {
                Debug.Log("OnJoined Instance");
                createLocalPlayer();
            }
            else
                Debug.Log("�濡 ������� �ʾҽ��ϴ�.");
        }

        // Ÿ �÷��̾� ����� �α׸� ���
        public override void OnPlayerEnteredRoom(Player other)
        {
            Debug.LogFormat("OnPlayerEnteredRoom() {0}", other.NickName);

            if (PhotonNetwork.IsMasterClient) // ������ ��
            {
                Debug.LogFormat("OnPlayerEnteredRoom IsMasterClient {0}", PhotonNetwork.IsMasterClient);
            }
        }

        // Ÿ �÷��̾� ����� �α� ���
        public override void OnPlayerLeftRoom(Player other)
        {
            Debug.LogFormat("OnPlayerLeftRoom() {0}", other.NickName);
        }

        #endregion
        #region Private Fields

        #endregion
        #region Public Fields
        public static GameManager instance; // �ٸ� ��ũ��Ʈ���� Ȱ���ϱ� ����
        #endregion
        #region MonoBehaviour CallBakcs
        // �� ���� �Ǵ� ����� ���� �Ŵ��� ������Ʈ�� ��������� �÷��̾� ��ü�� �����մϴ�.
        // ���� �ε� �Ǵ� �������� �濡 ������ ������ ���� �ʾ��� �� OnJoinedRoom �Լ����� �ѹ� �� Ȯ���մϴ�.
        private void Start()
        {
            instance = this;
            if(PhotonNetwork.InRoom && PlayerManager.LocalPlayerInstance == null)
            {
                Debug.Log("Start Instance");
                createLocalPlayer();
            }
        }
        #endregion
        #region Private Methods
        // ���� �뿡 �� �÷��̾� ��ü�� �����մϴ�. ������ Ŭ���̾�Ʈ(��Ʈ����)�� ��� ��Ʈ���� �ν��Ͻ��� �����ϰ�, �� �ܿ��� ��û�� �ν��Ͻ��� �����մϴ�.
        public void createLocalPlayer()
        {
            if (PhotonNetwork.IsMasterClient)
                PhotonNetwork.Instantiate(this.StreamerPrefab.name, new Vector3(0f, 2.5f, 12.5f), Quaternion.identity, 0);
            else
                PhotonNetwork.Instantiate(this.ViewerPrefab.name, new Vector3(0f, 5f, 22.5f), Quaternion.Euler(0f, 180f, 0f), 0);
        }
        #endregion
        #region Public Methods
        // ��� �뿡�� ���� �� ������ �Լ���, ���� ��Ʈ��ũ�� ���� ���� �����ϴ�. ���� �κ� ���� �ε��մϴ�.
        public void LeaveRoom()
        {
            PhotonNetwork.LeaveRoom();
            UnityEngine.SceneManagement.SceneManager.LoadScene(0);
        }

        #endregion
    }
}