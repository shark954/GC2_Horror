using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lock : MonoBehaviour
{
    public bool isLocked = true; // �����������Ă��邩�ǂ����̃t���O
    public string requiredKey = "GoldenKey"; // �K�v�Ȍ��̖��O

    // �v���C���[�����̃g���K�[�]�[���ɓ��������ɌĂяo����郁�\�b�h
    void OnTriggerEnter(Collider other)
    {
       /* // �g���K�[�ɓ������I�u�W�F�N�g���v���C���[���ǂ������`�F�b�N
        if (other.CompareTag("Player"))
        {
            // �v���C���[�̃C���x���g�����擾
            PlayerInventory playerInventory = other.GetComponent<PlayerInventory>();
            // �C���x���g�������݂��A�K�v�Ȍ��������Ă��邩�ǂ������m�F
            if (playerInventory != null && playerInventory.HasKey(requiredKey))
            {
                // ��������ꍇ�A�����J����
                UnlockDoor();
            }
            else
            {
                // �����Ȃ��ꍇ�̃��b�Z�[�W�����O�ɕ\��
                Debug.Log("���̔����J����ɂ�" + requiredKey + "���K�v�ł��B");
            }
        }*/
    }

    // �����J���郁�\�b�h
    void UnlockDoor()
    {
        isLocked = false;
        // �����J����A�j���[�V�����⓮���ǉ�
        Debug.Log("�����J���܂����I");
    }
}



