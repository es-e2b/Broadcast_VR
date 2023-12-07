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
        // 생성할 플레이어 프리팹을 퍼블릭 변수로 받아줍니다.
        [Tooltip("플레이어 프리팹")]
        public GameObject StreamerPrefab;
        public GameObject ViewerPrefab;
        #endregion
        #region Photon CallBacks

        // 방에서 나가면 로비 씬을 불러옵니다.
        public override void OnLeftRoom()
        {
            SceneManager.LoadScene(0); // 방에서 나가면 로비 씬 로드
        }

        // 방에 입장시 내 플레이어 인스턴스가 없다면 생성해줍니다.
        public override void OnJoinedRoom()
        {
            //base.OnJoinedRoom();
            if (PhotonNetwork.InRoom && PlayerManager.LocalPlayerInstance == null)
            {
                Debug.Log("OnJoined Instance");
                createLocalPlayer();
            }
            else
                Debug.Log("방에 연결되지 않았습니다.");
        }

        // 타 플레이어 입장시 로그를 기록
        public override void OnPlayerEnteredRoom(Player other)
        {
            Debug.LogFormat("OnPlayerEnteredRoom() {0}", other.NickName);

            if (PhotonNetwork.IsMasterClient) // 방장일 시
            {
                Debug.LogFormat("OnPlayerEnteredRoom IsMasterClient {0}", PhotonNetwork.IsMasterClient);
            }
        }

        // 타 플레이어 퇴장시 로그 기록
        public override void OnPlayerLeftRoom(Player other)
        {
            Debug.LogFormat("OnPlayerLeftRoom() {0}", other.NickName);
        }

        #endregion
        #region Private Fields

        #endregion
        #region Public Fields
        public static GameManager instance; // 다른 스크립트에서 활용하기 위함
        #endregion
        #region MonoBehaviour CallBakcs
        // 방 생성 또는 입장시 게임 매니저 오브젝트가 만들어지며 플레이어 객체를 생성합니다.
        // 씬이 로드 되는 과정에서 방에 완전히 접속이 되지 않았을 시 OnJoinedRoom 함수에서 한번 더 확인합니다.
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
        // 포톤 룸에 내 플레이어 객체를 생성합니다. 마스터 클라이언트(스트리머)일 경우 스트리머 인스턴스를 생성하고, 그 외에는 시청자 인스턴스를 생성합니다.
        public void createLocalPlayer()
        {
            if (PhotonNetwork.IsMasterClient)
                PhotonNetwork.Instantiate(this.StreamerPrefab.name, new Vector3(0f, 2.5f, 12.5f), Quaternion.identity, 0);
            else
                PhotonNetwork.Instantiate(this.ViewerPrefab.name, new Vector3(0f, 5f, 22.5f), Quaternion.Euler(0f, 180f, 0f), 0);
        }
        #endregion
        #region Public Methods
        // 방송 룸에서 나갈 때 실행할 함수로, 포톤 네트워크를 통해 룸을 나갑니다. 이후 로비 씬을 로드합니다.
        public void LeaveRoom()
        {
            PhotonNetwork.LeaveRoom();
            UnityEngine.SceneManagement.SceneManager.LoadScene(0);
        }

        #endregion
    }
}