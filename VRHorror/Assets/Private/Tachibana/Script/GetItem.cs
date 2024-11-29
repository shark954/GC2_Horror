using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class GetItem : MonoBehaviour
{
    [Header("�A�C�e��")]
    protected Transform m_item;
    [Header("�A�C�e���ނƎ���Ȃ����")]
    public Item itemchain;
    [Header("�p�����[�^")]
    public Parameta parameta;
    [Header("��̓����蔻��")]
    public Collider getitem;
    [Header("true�E,false��")]    //��̍��E����
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
            //A�{�^���ŃA�C�e���𗣂�
            if (OVRInput.Get(OVRInput.RawButton.A) || Input.GetKeyDown(KeyCode.F) && hand)
            {
                Rigidbody RD = m_item.GetComponent<Rigidbody>();
                RD.useGravity = true;
                RD.constraints = RigidbodyConstraints.None;

                itemchain = null;
                m_item.transform.parent = null;
                m_item = null;
            }
            //X�{�^���ŃA�C�e���𗣂�
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

    //�A�C�e�����E��
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

        //���Transform�ɍ��킹��
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

    //���C�g�����点��
    public void FlashMove()
    {
        if (!m_item)
            return;
        if (!itemchain)
            return;
        //�_��
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
        //����
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
