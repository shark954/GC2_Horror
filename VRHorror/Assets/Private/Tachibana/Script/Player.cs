using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    private float m_moveSpeed = 2.0f;
    //1秒間で回転する角度
    [SerializeField]
    private float m_rotateSpeed = 90.0f;
    //カメラのTransform
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
        if (inputL.sqrMagnitude > 0f) //入力されたら
        {
            //カメラからの横と正面を取得
            Vector3 side = m_camTF.right;
            side.y = 0f;
            side.Normalize();
            Vector3 forword = m_camTF.forward;
            forword.y = 0f;
            forword.Normalize();
            //移動ベクトル(横＋正面)
            Vector3 move = side * inputL.x + forword * inputL.y;
            //移動後の座標（現在地＋移動方向＊移動量）
            Vector3 pos = transform.position + move * m_moveSpeed * Time.fixedDeltaTime;
            //Rigidbodyに移動後の座標を設定
            m_rb.MovePosition(pos);
        }

        //右アナログスティックの入力値
        Vector2 inputR = OVRInput.Get(OVRInput.RawAxis2D.RThumbstick);
        //入力値に合わせてプレイヤーを回転
        transform.Rotate(0f, inputR.x * m_rotateSpeed * Time.fixedDeltaTime, 0f);
    }
}
