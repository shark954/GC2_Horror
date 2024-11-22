using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    private float m_moveSpeed = 2.0f;
    //1�b�Ԃŉ�]����p�x
    [SerializeField]
    private float m_rotateSpeed = 90.0f;
    //�J������Transform
    [SerializeField]
    private Transform m_camTF = null;

    private Rigidbody m_rb = null;
    private void Awake()
    {
        m_rb = GetComponent<Rigidbody>();
    }


    #region FixUpdate

    private void FixedUpdate()
    {
        MoveSystem();
    }
    #endregion

    private void MoveSystem()
    {
        Vector2 inputL = OVRInput.Get(OVRInput.RawAxis2D.LThumbstick);
        if (inputL.sqrMagnitude > 0f) //���͂��ꂽ��
        {
            //�J��������̉��Ɛ��ʂ��擾
            Vector3 side = m_camTF.right;
            side.y = 0f;
            side.Normalize();
            Vector3 forword = m_camTF.forward;
            forword.y = 0f;
            forword.Normalize();
            //�ړ��x�N�g��(���{����)
            Vector3 move = side * inputL.x + forword * inputL.y;
            //�ړ���̍��W�i���ݒn�{�ړ��������ړ��ʁj
            Vector3 pos = transform.position + move * m_moveSpeed * Time.fixedDeltaTime;
            //Rigidbody�Ɉړ���̍��W��ݒ�
            m_rb.MovePosition(pos);
        }

        //�E�A�i���O�X�e�B�b�N�̓��͒l
        Vector2 inputR = OVRInput.Get(OVRInput.RawAxis2D.RThumbstick);
        //���͒l�ɍ��킹�ăv���C���[����]
        transform.Rotate(0f, inputR.x * m_rotateSpeed * Time.fixedDeltaTime, 0f);
    }
}
