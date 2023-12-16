using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

namespace Broadcast.JES
{
    public class ChatManager : MonoBehaviourPunCallbacks
    {
        #region Public Fields
        public static ChatManager instance;
        public Text[] ChatText;
        public InputField ChatInput;
        public GameObject chatPanel;
        public static PlayerManager LocalPlayerInstance;
        public GameObject closeButton;
        public GameObject openButton;
        #endregion
        #region Private Fields
        private bool chatVisible = false;
        #endregion
        #region MonoBehaviour Callbacks
        private void Start()
        {
            instance = this;
            chatPanel.SetActive(chatVisible);
            closeButton.SetActive(chatVisible);
            openButton.SetActive(!chatVisible);

            for (int i = 0; i < ChatText.Length; i++)
                ChatText[i].text = "";
            ChatInput.text = "";
        }
        private void Update()
        {

            if (Input.GetKeyDown(KeyCode.Return))
            {
                if (chatVisible == false)
                {
                    chatUIControl();
                    return;
                }

                string text=ChatInput.text;
                if (text.Substring(0,1).Equals("/"))
                {
                    text = ChatInput.text.Substring(1);
                    SuperChat(text);
                }
                SendMsg(text);
            }
        }
        #endregion
        #region Pun Callbacks

        public override void OnJoinedRoom()
        {
            ChatInput.text = "";
            for (int i = 0; i < ChatText.Length; i++) ChatText[i].text = "";

        }

        public override void OnPlayerEnteredRoom(Player newPlayer)
        {
            photonView.RPC("ChatRPC", RpcTarget.All, "<color=yellow>" + newPlayer.NickName + "¥‘¿Ã ¬¸∞°«œºÃΩ¿¥œ¥Ÿ</color>");
        }

        public override void OnPlayerLeftRoom(Player otherPlayer)
        {
            photonView.RPC("ChatRPC", RpcTarget.All, "<color=yellow>" + otherPlayer.NickName + "¥‘¿Ã ≈¿Â«œºÃΩ¿¥œ¥Ÿ</color>");
        }

        public void SendMsg(string text)
        {
            string msg = PhotonNetwork.NickName + ": "+text;
            while (msg.Length > 0)
            {
                if (msg.Length < 30)
                {
                    photonView.RPC("ChatRPC", RpcTarget.All, msg);
                    msg = "";
                }
                else
                {
                    photonView.RPC("ChatRPC", RpcTarget.All, msg.Substring(0,30));
                    msg= msg.Substring(30);
                }
            }
            ChatInput.text = "";
        }
        public void SuperChat(string text)
        {
            string msg = text;
            while (msg.Length > 0)
            {
                if (msg.Length < 10)
                {
                    LocalPlayerInstance.photonView.RPC("SuperChat", RpcTarget.All, msg, LocalPlayerInstance.photonView.ViewID);
                    msg = "";
                }
                else
                {
                    LocalPlayerInstance.photonView.RPC("SuperChat", RpcTarget.All, msg.Substring(0,10), LocalPlayerInstance.photonView.ViewID);
                    msg = msg.Substring(10);
                }
            }
        }

        [PunRPC]
        void ChatRPC(string msg)
        {
                for (int i = ChatText.Length - 1; i > 0; i--)
                {
                    ChatText[i].text = ChatText[i - 1].text;
                }
                ChatText[0].text = msg;
        }

        #endregion
        #region Public Method
        public void chatUIControl()
        {
            Debug.Log("tab button");
            chatVisible = !chatVisible;
            chatPanel.SetActive(chatVisible);
            closeButton.SetActive(chatVisible);
            openButton.SetActive(!chatVisible);
        }
        #endregion
    }
}