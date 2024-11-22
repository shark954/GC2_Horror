using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // 移動速度
    public float moveSpeed = 3.0f;
    // VRカメラ（HMD）のTransform
    public Transform vrCamera;
    // プレイヤーのRigidbody
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
        // 左スティックの入力を取得
        Vector2 input = OVRInput.Get(OVRInput.RawAxis2D.LThumbstick);

        // 入力をカメラの方向に基づいて変換
        Vector3 forward = vrCamera.forward;
        Vector3 right = vrCamera.right;

        // 水平方向だけ考慮
        forward.y = 0;
        right.y = 0;

        forward.Normalize();
        right.Normalize();

        // 移動ベクトルを計算
        Vector3 movement = (forward * input.y + right * input.x) * moveSpeed;

        // Rigidbodyで移動
        Vector3 newPosition = playerRigidbody.position + movement * Time.fixedDeltaTime;
        playerRigidbody.MovePosition(newPosition);
    }
}
