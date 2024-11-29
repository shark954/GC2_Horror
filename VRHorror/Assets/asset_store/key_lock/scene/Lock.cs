using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lock : MonoBehaviour
{
    public bool isLocked = true; // 鍵がかかっているかどうかのフラグ
    public string requiredKey = "GoldenKey"; // 必要な鍵の名前

    // プレイヤーが扉のトリガーゾーンに入った時に呼び出されるメソッド
    void OnTriggerEnter(Collider other)
    {
       /* // トリガーに入ったオブジェクトがプレイヤーかどうかをチェック
        if (other.CompareTag("Player"))
        {
            // プレイヤーのインベントリを取得
            PlayerInventory playerInventory = other.GetComponent<PlayerInventory>();
            // インベントリが存在し、必要な鍵を持っているかどうかを確認
            if (playerInventory != null && playerInventory.HasKey(requiredKey))
            {
                // 鍵がある場合、扉を開ける
                UnlockDoor();
            }
            else
            {
                // 鍵がない場合のメッセージをログに表示
                Debug.Log("この扉を開けるには" + requiredKey + "が必要です。");
            }
        }*/
    }

    // 扉を開けるメソッド
    void UnlockDoor()
    {
        isLocked = false;
        // 扉を開けるアニメーションや動作を追加
        Debug.Log("扉が開きました！");
    }
}



