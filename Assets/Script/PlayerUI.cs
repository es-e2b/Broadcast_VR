using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Broadcast.JES
{
    public class PlayerUI : MonoBehaviour
    {
        #region Private Fields

        [Tooltip("UI�� �������� �������ּ���.")]
        [SerializeField]
        private Vector3 screenOffset = new Vector3(0f, 15f, 0f);

        [Tooltip("UI �������� playerNameText(�÷��̾� �г���)�� �־��ּ���.")]
        [SerializeField]
        private Text playerNameText;

        // Ÿ����, ������ �÷��̾� �ν��Ͻ��� �÷��̾� �Ŵ����� �����մϴ�.
        PlayerManager target;

        // Ÿ���� ĳ���� ��Ʈ�ѷ��� ���̸� �������� �� ���̱� ���� ����� �����Դϴ�.
        float characterControllerHeight;

        // Ÿ���� Ʈ������ �� �������� �޾��ݴϴ�.
        Transform targetTransform;
        Vector3 targetPosition;

        // �� �κ��� �ٸ� ������ �޾ƿ� ���̶� �� �𸣰ڽ��ϴ�.
        Renderer targetRenderer;

        // ���� �����ϴ�.
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
            // ���� Ÿ���� �������� ��(��ü�� �÷��̾ �뿡�� ������ ���) �� UI�� �ı��մϴ�.
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

            // Ÿ���� ���� �������� ���� ȭ�鿡 �����ϱ� ���� �κ��Դϴ�.
            if (targetTransform != null)
            {
                // Ÿ���� �������� LateUpdate()�� ȣ��� ������ ���� �������ְ�,
                targetPosition = targetTransform.position;
                // �� �����ǿ� Ÿ���� ĳ���� ��Ʈ�ѷ��� ���̸�ŭ �� �����ݴϴ�. �̶� Ÿ���� �����Ͽ� ���� �� ���� �÷��ݴϴ�.
                // ��û�� ������ = 1, ��Ʈ���� ������ = 5
                targetPosition.y += characterControllerHeight*targetTransform.localScale.y;

                // LocalPlayerCamera�� �� �÷��̾� �ν��Ͻ��� ī�޶��Դϴ�. �� ī�޶� UI�� ���ϴ�.
                // WorldToScreenPoint() �Լ��� ���� UI�� �������� ī�޶� ������ ���ִ� �κ��Դϴ�.
                this.transform.position = PlayerManager.currentCamera.WorldToScreenPoint(targetPosition) + screenOffset;
            }

        }




        #endregion

        #region Public Methods

        /// <param name="target">Target.</param>
        // SetTarget(PlayerManager _target) �Լ��� �÷��̾� �Ŵ����� ������ �� ȣ��Ǹ�, �ش� �÷��̾� �Ŵ����� Ÿ������ �����ϵ��� ���ִ� �Լ��Դϴ�.
        public void SetTarget(PlayerManager _target)
        {

            // Ÿ���� ����� �������� �ʾ��� �� ���� �޼��� ó���Դϴ�.
            if (_target == null)
            {
                Debug.LogError("<Color=Red><b>Missing</b></Color> PlayMakerManager target for PlayerUI.SetTarget.", this);
                return;
            }

            // �� ��ũ��Ʈ�� target ������ �� Ÿ���� �־��ݴϴ�. �� ���� �� Ÿ���� Ʈ�������� ������ ������Ʈ�� �޽��ϴ�.
            this.target = _target;
            targetTransform = this.target.GetComponent<Transform>();
            targetRenderer = this.target.GetComponentInChildren<Renderer>();

            // Ÿ���� ĳ���� ��Ʈ�ѷ� ������Ʈ�� �޽��ϴ�.
            CharacterController _characterController = this.target.GetComponent<CharacterController>();

            // ���������� ĳ���� ��Ʈ�ѷ��� ���� ���� �޾��ݴϴ�.
            if (_characterController != null)
            {
                characterControllerHeight = _characterController.height;
            }

            // UI�� �ؽ�Ʈ �κ��� �ش� �÷��̾��� �г������� �����մϴ�.
            // photonView.Owner.Nickname�� �ش� �÷��̾� �ν��Ͻ��� ������ �г����� �޴´ٴ� �ǹ��Դϴ�.
            // ������ �����ϱ� ��, �г����� �Է¹޽��ϴ�. 
            if (playerNameText != null)
            {
                playerNameText.text = this.target.photonView.Owner.NickName;
            }
        }

        #endregion

    }
}
