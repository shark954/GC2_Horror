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

    private void Start()
    {

    }

    private void Update()
    {
        if (parameta.hp > 0)
            FlashMove();
    }

    private void LateUpdate()
    {
        if (m_item && itemchain)
        {
            //Aボタンでアイテムを離す
            if (OVRInput.Get(OVRInput.RawButton.A) || Input.GetKeyDown(KeyCode.F) && hand)
            {
                Rigidbody RD = m_item.GetComponent<Rigidbody>();
                RD.useGravity = true;
                RD.constraints = RigidbodyConstraints.None;

                itemchain = null;
                m_item.transform.parent = null;
                m_item = null;
            }
            //Xボタンでアイテムを離す
            if (OVRInput.Get(OVRInput.RawButton.X) || Input.GetKeyDown(KeyCode.F) && !hand)
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

        if (OVRInput.Get(OVRInput.RawButton.RHandTrigger) || Input.GetKeyDown(KeyCode.E) && hand)
            trigeron = true;
        if (OVRInput.Get(OVRInput.RawButton.LHandTrigger) || Input.GetKeyDown(KeyCode.E) && !hand)
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

            if (other.GetComponent<HandLights>())
            {
                this.transform.rotation = Quaternion.Euler(-15, 0, 0);
                other.transform.rotation = this.transform.rotation;
            }
            else
            {
                other.transform.rotation = this.transform.rotation;
            }

            if (other.GetComponent<Batteries>())
            {

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
            if (OVRInput.GetDown(OVRInput.RawButton.RIndexTrigger) || Input.GetMouseButtonDown(0) && hand)
                trigeron = true;
            if (OVRInput.GetDown(OVRInput.RawButton.LIndexTrigger) || Input.GetMouseButtonDown(0) && !hand)
                trigeron = true;
            if (!trigeron)
                return;
            itemchain.trggerOn = true;
        }
        //消灯
        else
        {
            bool trigeron = false;
            if (OVRInput.GetDown(OVRInput.RawButton.RIndexTrigger) || Input.GetMouseButtonDown(0) && hand)
                trigeron = true;
            if (OVRInput.GetDown(OVRInput.RawButton.LIndexTrigger) || Input.GetMouseButtonDown(0) && !hand)
                trigeron = true;
            if (!trigeron)
                return;
            itemchain.trggerOn = false;
        }
    }
}
