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
        #endregion

        #region MonoBehaviour Callbacks
        public void Awake()
        {
            // �÷��̾� �ν��Ͻ��� �� �ν��Ͻ��� ��, LoaclPlayerInstance ������ ���� �� ī�޶� ������ ����+
            if (photonView.IsMine)
            {
                LocalPlayerInstance = this;
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
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}