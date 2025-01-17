using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class GetItem : MonoBehaviour
{

    public Collider getitem = null;

    [Header("true右,false左")]    //手の左右判別
    public bool hand = false;

    [Header("右手アニメーション")]
    public HandAnimation handAnim_R;

  
    [SerializeField]
    private GameManager gameManager;

    [SerializeField]
    private HandLights handLights = null;

    private void Start()
    {
      
    }

    private void Update()
    {
        if (!gameManager.gameOver)
        {
            FlashMove();
        }
           

        HandMotion();
    }

    //アイテムを拾う
    private void OnTriggerStay(Collider other)
    {
        // バッテリーを拾う処理
        if (other.CompareTag("Batteries"))
        {
            Batteries batteries = other.GetComponent<Batteries>();
            if (batteries != null)
            {
                    handLights.StartBatteryRestore(batteries.batteryRestoreAmount, batteries.restoreDuration);
                    Debug.Log($"バッテリーを適用: {batteries.batteryRestoreAmount}、所要時間: {batteries.restoreDuration} 秒");
                    Destroy(other.gameObject); // バッテリーを削除
            }
        }

        //鍵を拾う
        if (other.CompareTag("GoalKey"))
        {
            gameManager.goalKey = true;
            Destroy(other.gameObject);
        }

        //ゲームクリア
        if (other.CompareTag("GoalDoor") && gameManager.goalKey)
        {
            gameManager.gameClear = true;
        }

    }

    //ライトを光らせる
    public void FlashMove()
    {
        if (OVRInput.GetDown(OVRInput.RawButton.LIndexTrigger))
        {
            if (!handLights.lightOn)
            {
                handLights.TurnOn();
            }
            else if (handLights.lightOn)
            {
                handLights.TurnOff();
            }
        }
    }

    

    private void HandMotion()
    {

        if (handAnim_R != null)
        {
            bool isGrabR = OVRInput.Get(OVRInput.RawButton.RHandTrigger) && hand || Input.GetMouseButton(0) && hand;
            handAnim_R.animator.SetBool("IsGrabR", isGrabR);
        }

    }

}
