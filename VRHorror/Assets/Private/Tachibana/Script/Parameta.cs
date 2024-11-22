using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parameta : MonoBehaviour
{
    [Header("体力")]
    public int hp;
    [Header("精神汚染フラグ")]
    public bool death = false;
   
    [Header("エフェクト")]
    public GameObject effect;
    [Header("エフェクト消滅時間")]
    public float effectdel;

    private void Start()
    {
        
    }


    private void Update()
    {
        
    }

    public bool Hitdamage(int damage)
    {
        bool flag = false;
        if (!death)
        {
            flag = true;
            hp -= damage;
            if (hp <= 0)
            {
                hp = 0;
                death = true;
                //animator.SetBool("Death", death);
                Debug.Log("HPが0になったよーー");
            }
        }
        return flag;
    }

    public void Die(float destroyTime)
    {
        Destroy(this.gameObject, destroyTime);
        Debug.Log("消えた");
    }

    private void OnDestroy()
    {
        if (!effect)
            return;
        GameObject Dummy = Instantiate(effect, transform.position, transform.rotation);
        Debug.Log("エフェクト");
        Destroy(Dummy, effectdel);

    }
}
