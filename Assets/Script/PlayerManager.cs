using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Broadcast.JES
{
    public class PlayerManager : MonoBehaviourPunCallbacks
    {
        #region Public Fields

        [Tooltip("내 고유 플레이어 프리팹")]
        public static PlayerManager LocalPlayerInstance;
        public Camera LocalPlayerCamera;

        #endregion
        #region Private Fields

        [Tooltip("플레이어 UI 프리팹을 삽입")]
        [SerializeField]
        private GameObject playerUiPrefab;

        [Tooltip("슈퍼챗 UI 프리팹을 삽입")]
        [SerializeField]
        private GameObject superChatUiPrefab;

        GameObject _superChatUiGo;
        #endregion

        [PunRPC]
        public void SuperChat(string text, int sender)
        {
            Debug.Log("수퍼챗 RPC 호출" + text + " " + sender);
            if (this.photonView.ViewID == sender && !photonView.IsMine)
            {
                _superChatUiGo.GetComponent<SuperChatUI>().SuperChat(text);
            }
        }

        #region MonoBehaviour Callbacks
        public void Awake()
        {
            // 플레이어 인스턴스가 내 인스턴스일 때, LoaclPlayerInstance 변수에 저장 및 카메라 렌더링 순위+
            if (photonView.IsMine)
            {
                LocalPlayerInstance = this;
                ChatManager.LocalPlayerInstance = this;
                LocalPlayerCamera.depth = 1;
            }
        }
        #endregion
        void Start()
        {
            // 플레이어 인스턴스가 생성될 때 닉네임 UI 생성. 단, 내 플레이어 객체는 생성하지 않음.
            if (playerUiPrefab != null && !photonView.IsMine)
            {
                Debug.Log("UI 생성");

                // Player UI 생성. 네트워크에 생성하는 것이 아니라, 내 게임 화면에서만 생성합니다.
                GameObject _uiGo = Instantiate(playerUiPrefab);
                // PlayerUI 인스턴스에 SetTarget(this) 함수를 실행하게 만듭니다. SetTarget(this) 메서드는 이 인스턴스를 타겟으로 설정하게 만듭니다.
                // 이후 UI 인스턴스는 타겟을 기준으로 화면에 렌더링합니다.
                _uiGo.SendMessage("SetTarget", this, SendMessageOptions.RequireReceiver);
            }

            // 플레이어 인스턴스가 생성될 때 슈퍼챗 UI 생성. 단, 내 플레이어 객체는 생성하지 않음.
            if (superChatUiPrefab != null)
            {
                Debug.Log("UI 생성");

                // 슈퍼챗 UI 생성. 네트워크에 생성하는 것이 아니라, 내 게임 화면에서만 생성합니다.
                _superChatUiGo = Instantiate(superChatUiPrefab);
                // 슈퍼챗 인스턴스에 SetTarget(this) 함수를 실행하게 만듭니다. SetTarget(this) 메서드는 이 인스턴스를 타겟으로 설정하게 만듭니다.
                // 이후 UI 인스턴스는 타겟을 기준으로 화면에 렌더링합니다.
                _superChatUiGo.SendMessage("SetTarget", this, SendMessageOptions.RequireReceiver);
            }
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}