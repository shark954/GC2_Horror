using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class GetItem : MonoBehaviour
{
    [Header("アイテム")]
    protected Transform m_item;
    [Header("アイテム類と手をつなぐやつ")]
    public Item itemchain;
    [Header("パラメータ")]
    public Parameta parameta;
    [Header("手の当たり判定")]
    public Collider getitem;
    [Header("true右,false左")]    //手の左右判別
    public bool hand = false;

    [Header("右手アニメーション")]
    public HandAnimation handAnim_R;

    [Header("左手アニメーション")]
    public HandAnimation handAnim_L;

    private void Start()
    {

    }

    private void Update()
    {
        if (parameta.hp > 0)
            FlashMove();

        HandMotion();
    }

    private void LateUpdate()
    {
        if (m_item && itemchain)
        {
            //Aボタンでアイテムを離す
            if (OVRInput.GetUp(OVRInput.RawButton.RHandTrigger) && hand || Input.GetKeyDown(KeyCode.F) && hand)
            {
                Rigidbody RD = m_item.GetComponent<Rigidbody>();
                RD.useGravity = true;
                RD.constraints = RigidbodyConstraints.None;

                itemchain = null;
                m_item.transform.parent = null;
                m_item = null;
            }
            //Xボタンでアイテムを離す
            if (OVRInput.GetUp(OVRInput.RawButton.LHandTrigger) && !hand || Input.GetKeyDown(KeyCode.F) && !hand)
            {
                Rigidbody RD = m_item.GetComponent<Rigidbody>();
                RD.useGravity = true;
                RD.constraints = RigidbodyConstraints.None;

                itemchain = null;
                m_item.transform.parent = null;
                m_item = null;
            }
        }
    }

    //アイテムを拾う
    private void OnTriggerStay(Collider other)
    {
        if (m_item)
            return;
        bool trigeron = false;
        bool lightflag = false;

        if (OVRInput.Get(OVRInput.RawButton.RHandTrigger) && hand || Input.GetKey(KeyCode.E) && hand)
            trigeron = true;
        if (OVRInput.Get(OVRInput.RawButton.LHandTrigger) && !hand || Input.GetKey(KeyCode.E) && !hand)
            trigeron = true;
        if (!trigeron)
            return;

        //手のTransformに合わせる
        if (other.GetComponent<Item>())
        {
            itemchain = other.GetComponent<Item>();
            Rigidbody RD = other.GetComponent<Rigidbody>();
            RD.useGravity = false;
            RD.constraints = RigidbodyConstraints.FreezeAll;
            other.transform.parent = this.transform;
            other.transform.position = this.transform.position;


            if (other.CompareTag("HandLights"))
            {
               // this.transform.rotation = Quaternion.Euler(-15, 0, 0);
                other.transform.rotation = this.transform.rotation;
                lightflag = true;
            }
            else
            {
                other.transform.rotation = this.transform.rotation;
            }

            if (other.CompareTag("Batteries"))
            {
                Batteries batteries = other.GetComponent<Batteries>();
                if (batteries != null)
                {
                    HandLights handLights = this.GetComponent<HandLights>(); // 手に持っているライトを直接取得
                    if (handLights != null)
                    {
                        handLights.StartBatteryRestore(batteries.batteryRestoreAmount, batteries.restoreDuration);
                        Debug.Log($"バッテリーを回復: {batteries.batteryRestoreAmount}、所要時間: {batteries.restoreDuration} 秒");
                    }
                    else
                    {
                        Debug.LogError("HandLights が見つかりません");
                    }
                    Destroy(other.gameObject); // バッテリーを削除
                }
            }
            m_item = other.transform;
        }
    }

    //ライトを光らせる
    public void FlashMove()
    {
        if (!m_item)
            return;
        if (!itemchain)
            return;
        //点灯
        if (!itemchain.trggerOn)
        {
            bool trigeron = false;
            if (OVRInput.GetDown(OVRInput.RawButton.RIndexTrigger) && hand || Input.GetMouseButtonDown(0) && hand)
                trigeron = true;
            if (OVRInput.GetDown(OVRInput.RawButton.LIndexTrigger) && !hand || Input.GetMouseButtonDown(0) && !hand)
                trigeron = true;
            if (!trigeron)
                return;
            itemchain.trggerOn = true;

        }
        //消灯
        else
        {
            bool trigeron = false;
            if (OVRInput.GetDown(OVRInput.RawButton.RIndexTrigger) && hand || Input.GetMouseButtonDown(0) && hand)
                trigeron = true;
            if (OVRInput.GetDown(OVRInput.RawButton.LIndexTrigger) && !hand || Input.GetMouseButtonDown(0) && !hand)
                trigeron = true;
            if (!trigeron)
                return;
            itemchain.trggerOn = false;

        }
    }


    private void HandMotion()
    {

        if (handAnim_R != null)
        {
            bool isGrabR = OVRInput.Get(OVRInput.RawButton.RHandTrigger) && hand || Input.GetMouseButton(0) && hand;
            handAnim_R.animator.SetBool("IsGrabR", isGrabR);
        }

        if (handAnim_L != null)
        {
            bool isGrabL = OVRInput.Get(OVRInput.RawButton.LHandTrigger) && !hand || Input.GetMouseButton(0) && !hand;
            handAnim_L.animator.SetBool("IsGrabL", isGrabL);
        }
    }
}
