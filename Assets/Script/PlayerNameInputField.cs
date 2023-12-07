using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;
using TMPro;

namespace Broadcast.JES
{
    [RequireComponent(typeof(InputField))]
    public class PlayerNameInputField : MonoBehaviour
    {
        #region Private Constants
        // �� ������ ���� ���� ������ �� ������ ���ظ� ���߽��ϴ�. �뷫 ���α׷��� ��ȸ ������ ��, ������ ����ߴ� ���� �ε��ϴ� ������ �� �����ϴ�.
        const string playerNamePrefKey = "PlayerName";
        #endregion

        #region MonoBehaviour CallBacks
        void Start()
        {
            // �г��� �Է� ĭ�� ����Ʈ ���� �������ݴϴ�. ������ ������ ���� �ִٸ� �װ��� ����Ʈ�� �������ݴϴ�.
            string defaultName = string.Empty;
            InputField _inputField = GetComponent<InputField>();
            if (_inputField != null)
            {
                // ���� ������ ���α׷��� �������� �� �Է��� ���� �ִٸ� �޾ƿɴϴ�.
                if(PlayerPrefs.HasKey(playerNamePrefKey))
                {
                    defaultName = PlayerPrefs.GetString(playerNamePrefKey);
                    _inputField.text = defaultName;
                }
            }
            PhotonNetwork.NickName = defaultName;
        }
        #endregion

        #region Public Methods

        // �޼��� �̸��� �� ����(�ް�)�Դϴ�.
        public void SetPlayerName(string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                Debug.LogError("Player Name is null or empty");
                return;
            }
            PhotonNetwork.NickName = value;

            PlayerPrefs.SetString(playerNamePrefKey, value);
        }
        #endregion
    }

}