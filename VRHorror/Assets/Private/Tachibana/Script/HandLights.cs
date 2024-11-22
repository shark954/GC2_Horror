using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandLights : MonoBehaviour
{
    [Header("ライトの当たり判定"), SerializeField]
    private BoxCollider handlight;

    [Header("ライトの光"),SerializeField]
    private GameObject spotligth;

    public Item item;

    // Start is called before the first frame update
    void Start()
    {
        spotligth.SetActive(false);
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
    }

    public void FlashOwder()
    {
        if (!item)
            return;
        if (item.trggerOn)
        {
            Flash();
        }

    }

    public void Flash()
    {
        spotligth.SetActive(true);
    }
}
