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
        // 이 변수는 아직 제가 설명을 할 정도로 이해를 못했습니다. 대략 프로그램을 다회 실행할 때, 이전에 사용했던 값을 로드하는 변수인 것 같습니다.
        const string playerNamePrefKey = "PlayerName";
        #endregion

        #region MonoBehaviour CallBacks
        void Start()
        {
            // 닉네임 입력 칸의 디폴트 값을 설정해줍니다. 이전에 저장한 값이 있다면 그것을 디폴트로 설정해줍니다.
            string defaultName = string.Empty;
            InputField _inputField = GetComponent<InputField>();
            if (_inputField != null)
            {
                // 만약 이전에 프로그램을 실행했을 때 입력한 값이 있다면 받아옵니다.
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

        // 메세드 이름이 곧 내용(메곧내)입니다.
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