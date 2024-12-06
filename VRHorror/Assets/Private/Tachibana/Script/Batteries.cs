using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Batteries : MonoBehaviour
{
    [Header("�d�r�̓����蔻��"), SerializeField]
    private BoxCollider batterie;

    [Header("�o�b�e���[�񕜗�")]
    public float batteryRestoreAmount = 20f;

    [Tooltip("�񕜂ɂ����鎞�� (�b)")]
    public float restoreDuration = 5f;

    


    private void Update()
    {
        if (this.transform.parent)
        {
            batterie.isTrigger = true;
        }
        else
        {
            batterie.isTrigger = false;
        }
    }
    
}
