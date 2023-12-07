using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
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
        #endregion
        #region Private Fields
        private bool chatVisible = false;
        #endregion
        #region MonoBehaviour Callbacks
        private void Start()
        {
            instance = this;
            chatPanel.SetActive(chatVisible);

            for (int i = 0; i < ChatText.Length; i++)
                ChatText[i].text = "";
            ChatInput.text = "";
        }
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Tab))
            {
                chatVisible = !chatVisible;
                chatPanel.SetActive(chatVisible);
            }

            if (Input.GetKeyDown(KeyCode.J))
            {
                if (chatVisible == false)
                    return;
                SendMsg();
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

        public void SendMsg()
        {
            string msg = PhotonNetwork.NickName + ": " + ChatInput.text;
            photonView.RPC("ChatRPC", RpcTarget.All, msg);
            ChatInput.text = "";
        }

        [PunRPC]
        void ChatRPC(string msg)
        {
            for(int i = ChatText.Length-1;i >0; i--)
            {
                ChatText[i].text = ChatText[i-1].text;
            }
            ChatText[0].text=msg;

            //for (int i = 0; i < ChatText.Length; i++)
            //{
            //    if (ChatText[i].text == "")
            //    {
            //        ChatText[i].text = msg;
            //        return;
            //    }

            //}

            //for (int i = 1; i < ChatText.Length; i++) ChatText[i - 1].text = ChatText[i].text;
            //ChatText[ChatText.Length - 1].text = msg;
        }

        #endregion
    }
}