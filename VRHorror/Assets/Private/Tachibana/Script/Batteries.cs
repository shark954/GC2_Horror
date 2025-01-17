using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Batteries : MonoBehaviour
{
    [Header("電池の当たり判定"), SerializeField]
    private BoxCollider batterie;

    [Header("バッテリー回復量")]
    public float batteryRestoreAmount = 20f;

    [Header("回復にかかる時間 (秒)")]
    public float restoreDuration = 5f;

    


    private void Update()
    {
        if (this.transform.parent)
        {
            batterie.enabled = false;
        }
        else
        {
            batterie.enabled= true;
        }
    }
    
}
