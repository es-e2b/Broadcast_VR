using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Broadcast.JES
{
    public class PlayerManager : MonoBehaviourPunCallbacks
    {
        #region Public Fields

        [Tooltip("The local player instance. Use this to know if the local player is represented in the Scene")]
        public static GameObject LocalPlayerInstance;

        #endregion
        #region Private Fields
        [SerializeField]
        private Camera LocalPlayerCamera;

        [Tooltip("플레이어 UI 프리팹을 삽입")]
        [SerializeField]
        private GameObject playerUiPrefab;
        #endregion

        #region MonoBehaviour Callbacks
        public void Awake()
        {
            if (photonView.IsMine)
            {
                LocalPlayerInstance = gameObject;
                LocalPlayerCamera.depth = 1;
            }
        }
        #endregion
        void Start()
        {
            if (playerUiPrefab != null)
            {
                Debug.Log("UI 생성");
                GameObject _uiGo = Instantiate(playerUiPrefab);
                _uiGo.SendMessage("SetTarget", this, SendMessageOptions.RequireReceiver);
            }
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}