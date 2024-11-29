using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parameta : MonoBehaviour
{
    [Header("�̗�")]
    public int hp;
    [Header("���_�����t���O")]
    public bool death = false;
   
    [Header("�G�t�F�N�g")]
    public GameObject effect;
    [Header("�G�t�F�N�g���Ŏ���")]
    public float effectdel;

    private void Start()
    {
        
    }


    private void Update()
    {
        
    }
    //�_���[�W�v�Z
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
                Debug.Log("<color=blue> HP��0�ɂȂ�����</color>");
            }
        }
        return flag;
    }
    //���S
    public void Die(float destroyTime)
    {
        Destroy(this.gameObject, destroyTime);
        Debug.Log("������");
    }

    //�I�u�W�F�N�g�̔j�󎞂ɌĂяo��
    private void OnDestroy()
    {
        if (!effect)
            return;
        GameObject Dummy = Instantiate(effect, transform.position, transform.rotation);
        Debug.Log("�G�t�F�N�g");
        Destroy(Dummy, effectdel);

    }
}
