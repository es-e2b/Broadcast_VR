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

        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}