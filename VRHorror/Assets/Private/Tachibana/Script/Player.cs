using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // �ړ����x
    public float moveSpeed = 3.0f;
    // VR�J�����iHMD�j��Transform
    public Transform vrCamera;
    // �v���C���[��Rigidbody
    private Rigidbody playerRigidbody;

    private void Awake()
    {
        playerRigidbody = GetComponent<Rigidbody>();
    }


    #region FixUpdate

    private void FixedUpdate()
    {
        MoveSystem();
    }
    #endregion

    private void MoveSystem()
    {
        // ���X�e�B�b�N�̓��͂��擾
        Vector2 input = OVRInput.Get(OVRInput.RawAxis2D.LThumbstick);

        // ���͂��J�����̕����Ɋ�Â��ĕϊ�
        Vector3 forward = vrCamera.forward;
        Vector3 right = vrCamera.right;

        // �������������l��
        forward.y = 0;
        right.y = 0;

        forward.Normalize();
        right.Normalize();

        // �ړ��x�N�g�����v�Z
        Vector3 movement = (forward * input.y + right * input.x) * moveSpeed;

        // Rigidbody�ňړ�
        Vector3 newPosition = playerRigidbody.position + movement * Time.fixedDeltaTime;
        playerRigidbody.MovePosition(newPosition);
    }
}
