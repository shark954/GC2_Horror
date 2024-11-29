using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HandLights : MonoBehaviour
{
    [Header("���C�g�̓����蔻��"), SerializeField]
    private BoxCollider handlight;

    [Header("���C�g�̌�"), SerializeField]
    private GameObject spotligth;

    public Item item;

   

    [Header("�o�b�e���[�e��")]
    public float batteries_mag;
    [Header("�o�b�e���[�ő�e��")]
    public float batteries_maxmag;

    [Header("�o�b�e���[�c�ʕ\��"),SerializeField]
    private Slider slider;

    [SerializeField]    //slider�̃I���E�I�t�p
    private GameObject bar;

    // Start is called before the first frame update
    void Start()
    {
        spotligth.SetActive(false);

        bar.SetActive(false);
        batteries_mag = batteries_maxmag;
    }

    // Update is called once per frame
    void Update()
    {
        if (this.transform.parent)
        {
            handlight.isTrigger = true;
        }
        else
        {
            handlight.isTrigger = false;
        }

        FlashOwder();
        BatteriesCount();
    }

    //���C�g�̃I���E�I�t
    public void FlashOwder()
    {
        if (!item)
            return;
        if (item.trggerOn && batteries_mag > 0)
        {
            TurnOn();
        }

        if (!item.trggerOn || batteries_mag <= 0)
        {
            Turnoff();
        }


    }

    //�_��
    public void TurnOn()
    {
        spotligth.SetActive(true);

        if (batteries_mag > 0)
        {
            batteries_mag -= 0.5f;
            if (batteries_mag <= 0)
            {
                Turnoff();
            }
        }
    }

    //����
    public void Turnoff()
    {
        spotligth.SetActive(false);
    }

    //�o�b�e���[�v�Z
    private void BatteriesCount()
    {
          if (transform.parent != null)
          {
            bar.SetActive(true);
          }

        slider.value = batteries_mag ;
        Debug.Log("slider.value : " + slider.value);
    }
}
