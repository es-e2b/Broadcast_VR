using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Broadcast.JES
{
    public class PlayerManager : MonoBehaviourPunCallbacks
    {
        #region Public Fields

        [Tooltip("�� ���� �÷��̾� ������")]
        public static PlayerManager LocalPlayerInstance;
        public Camera LocalPlayerCamera;

        #endregion
        #region Private Fields

        [Tooltip("�÷��̾� UI �������� ����")]
        [SerializeField]
        private GameObject playerUiPrefab;

        [Tooltip("����ê UI �������� ����")]
        [SerializeField]
        private GameObject superChatUiPrefab;

        GameObject _superChatUiGo;
        #endregion

        [PunRPC]
        public void SuperChat(string text, int sender)
        {
            Debug.Log("����ê RPC ȣ��" + text + " " + sender);
            if (this.photonView.ViewID == sender && !photonView.IsMine)
            {
                _superChatUiGo.GetComponent<SuperChatUI>().SuperChat(text);
            }
        }

        #region MonoBehaviour Callbacks
        public void Awake()
        {
            // �÷��̾� �ν��Ͻ��� �� �ν��Ͻ��� ��, LoaclPlayerInstance ������ ���� �� ī�޶� ������ ����+
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
            // �÷��̾� �ν��Ͻ��� ������ �� �г��� UI ����. ��, �� �÷��̾� ��ü�� �������� ����.
            if (playerUiPrefab != null && !photonView.IsMine)
            {
                Debug.Log("UI ����");

                // Player UI ����. ��Ʈ��ũ�� �����ϴ� ���� �ƴ϶�, �� ���� ȭ�鿡���� �����մϴ�.
                GameObject _uiGo = Instantiate(playerUiPrefab);
                // PlayerUI �ν��Ͻ��� SetTarget(this) �Լ��� �����ϰ� ����ϴ�. SetTarget(this) �޼���� �� �ν��Ͻ��� Ÿ������ �����ϰ� ����ϴ�.
                // ���� UI �ν��Ͻ��� Ÿ���� �������� ȭ�鿡 �������մϴ�.
                _uiGo.SendMessage("SetTarget", this, SendMessageOptions.RequireReceiver);
            }

            // �÷��̾� �ν��Ͻ��� ������ �� ����ê UI ����. ��, �� �÷��̾� ��ü�� �������� ����.
            if (superChatUiPrefab != null)
            {
                Debug.Log("UI ����");

                // ����ê UI ����. ��Ʈ��ũ�� �����ϴ� ���� �ƴ϶�, �� ���� ȭ�鿡���� �����մϴ�.
                _superChatUiGo = Instantiate(superChatUiPrefab);
                // ����ê �ν��Ͻ��� SetTarget(this) �Լ��� �����ϰ� ����ϴ�. SetTarget(this) �޼���� �� �ν��Ͻ��� Ÿ������ �����ϰ� ����ϴ�.
                // ���� UI �ν��Ͻ��� Ÿ���� �������� ȭ�鿡 �������մϴ�.
                _superChatUiGo.SendMessage("SetTarget", this, SendMessageOptions.RequireReceiver);
            }
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}