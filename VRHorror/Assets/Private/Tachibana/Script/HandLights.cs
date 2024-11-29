using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HandLights : MonoBehaviour
{
    [Header("ライトの当たり判定"), SerializeField]
    private BoxCollider handlight;

    [Header("ライトの光"), SerializeField]
    private GameObject spotligth;

    public Item item;

   

    [Header("バッテリー容量")]
    public float batteries_mag;
    [Header("バッテリー最大容量")]
    public float batteries_maxmag;

    [Header("バッテリー残量表示"),SerializeField]
    private Slider slider;

    [SerializeField]    //sliderのオン・オフ用
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

    //ライトのオン・オフ
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

    //点灯
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

    //消灯
    public void Turnoff()
    {
        spotligth.SetActive(false);
    }

    //バッテリー計算
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
