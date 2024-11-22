using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class GetItem : MonoBehaviour
{
    [Header("アイテム")]
    public Transform m_item;
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
        
    }

    private void LateUpdate()
    {
        if (m_item && itemchain)
        {
            if (OVRInput.Get(OVRInput.RawButton.A) || Input.GetKeyDown(KeyCode.F) && hand)
            {
                Rigidbody RD = m_item.GetComponent<Rigidbody>();
                RD.useGravity = true;
                RD.constraints = RigidbodyConstraints.None;


                itemchain = null;
                m_item.transform.parent = null;
                m_item = null;
            }
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

        if (other.GetComponent<Item>())
        {
            itemchain = other.GetComponent<Item>();
            Rigidbody RD = other.GetComponent<Rigidbody>();
            RD.useGravity = false;
            RD.constraints = RigidbodyConstraints.FreezeAll;
            other.transform.parent = this.transform;
            other.transform.position = this.transform.position;
            m_item = other.transform;
        }
    }
}
