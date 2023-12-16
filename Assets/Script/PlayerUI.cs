using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Broadcast.JES
{
    public class PlayerUI : MonoBehaviour
    {
        #region Private Fields

        [Tooltip("UI의 오프셋을 설정해주세요.")]
        [SerializeField]
        private Vector3 screenOffset = new Vector3(0f, 15f, 0f);

        [Tooltip("UI 프리팹의 playerNameText(플레이어 닉네임)를 넣어주세요.")]
        [SerializeField]
        private Text playerNameText;

        // 타겟을, 생성된 플레이어 인스턴스의 플레이어 매니저로 설정합니다.
        PlayerManager target;

        // 타겟의 캐릭터 컨트롤러의 높이를 기준으로 더 높이기 위해 선언된 변수입니다.
        float characterControllerHeight;

        // 타겟의 트랜스폼 및 포지션을 받아줍니다.
        Transform targetTransform;
        Vector3 targetPosition;

        // 이 부분은 다른 예제를 받아온 것이라 잘 모르겠습니다.
        Renderer targetRenderer;

        // 위와 같습니다.
        CanvasGroup _canvasGroup;

        #endregion

        #region MonoBehaviour Messages

        void Awake()
        {

            _canvasGroup = this.GetComponent<CanvasGroup>();

            this.transform.SetParent(GameObject.Find("Canvas").GetComponent<Transform>(), false);
        }

        void Update()
        {
            // 만약 타겟이 없어졌을 시(대체로 플레이어가 룸에서 나갔을 경우) 이 UI를 파괴합니다.
            if (target == null)
            {
                Destroy(this.gameObject);
                return;
            }
        }

        void LateUpdate()
        {
            if (targetRenderer != null)
            {
                this._canvasGroup.alpha = targetRenderer.isVisible ? 1f : 0f;
            }

            // 타겟의 현재 포지션을 따라 화면에 송출하기 위한 부분입니다.
            if (targetTransform != null)
            {
                // 타겟의 포지션을 LateUpdate()가 호출될 때마다 새로 설정해주고,
                targetPosition = targetTransform.position;
                // 그 포지션에 타겟의 캐릭터 컨트롤러의 높이만큼 더 더해줍니다. 이때 타겟의 스케일에 따라 더 높이 올려줍니다.
                // 시청자 스케일 = 1, 스트리머 스케일 = 5
                targetPosition.y += characterControllerHeight*targetTransform.localScale.y;

                // LocalPlayerCamera는 내 플레이어 인스턴스의 카메라입니다. 이 카메라에 UI를 띄웁니다.
                // WorldToScreenPoint() 함수는 현재 UI의 포지션을 카메라에 렌더링 해주는 부분입니다.
                this.transform.position = PlayerManager.currentCamera.WorldToScreenPoint(targetPosition) + screenOffset;
            }

        }




        #endregion

        #region Public Methods

        /// <param name="target">Target.</param>
        // SetTarget(PlayerManager _target) 함수는 플레이어 매니저가 생성될 때 호출되며, 해당 플레이어 매니저를 타겟으로 설정하도록 해주는 함수입니다.
        public void SetTarget(PlayerManager _target)
        {

            // 타겟이 제대로 설정되지 않았을 시 오류 메세지 처리입니다.
            if (_target == null)
            {
                Debug.LogError("<Color=Red><b>Missing</b></Color> PlayMakerManager target for PlayerUI.SetTarget.", this);
                return;
            }

            // 이 스크립트의 target 변수에 이 타겟을 넣어줍니다. 그 밑은 그 타겟의 트랜스폼과 렌더러 컴포넌트를 받습니다.
            this.target = _target;
            targetTransform = this.target.GetComponent<Transform>();
            targetRenderer = this.target.GetComponentInChildren<Renderer>();

            // 타겟의 캐릭터 컨트롤러 컴포넌트를 받습니다.
            CharacterController _characterController = this.target.GetComponent<CharacterController>();

            // 최종적으로 캐릭터 컨트롤러의 높이 또한 받아줍니다.
            if (_characterController != null)
            {
                characterControllerHeight = _characterController.height;
            }

            // UI의 텍스트 부분을 해당 플레이어의 닉네임으로 삽입합니다.
            // photonView.Owner.Nickname은 해당 플레이어 인스턴스의 주인의 닉네임을 받는다는 의미입니다.
            // 방으로 입장하기 전, 닉네임을 입력받습니다. 
            if (playerNameText != null)
            {
                playerNameText.text = this.target.photonView.Owner.NickName;
            }
        }

        #endregion

    }
}
