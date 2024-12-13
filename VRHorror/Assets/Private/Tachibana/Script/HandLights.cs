using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class HandLights : MonoBehaviour
{
    [Header("ライトの当たり判定"), SerializeField]
    private BoxCollider handlight;

    [Header("ライトの光"), SerializeField]
    private GameObject spotlight;

    public Item item;

    [Header("バッテリー容量")]
    public float batteries_mag;
    [Header("バッテリー最大容量")]
    public float batteries_maxmag;

    [Header("バッテリー残量表示"), SerializeField]
    private Slider slider;

    [SerializeField]    //sliderのオン・オフ用
    private GameObject bar;

    private Coroutine restoreCoroutine; // バッテリー回復コルーチンの参照を保持

    // Start is called before the first frame update
    void Start()
    {
        spotlight.SetActive(false);
        bar.SetActive(true);
        batteries_mag = batteries_maxmag ; // バッテリーを最大値に初期化
        //StartBatteryRestore(50f, 5f);
    }

    // Update is called once per frame
    void Update()
    {
        // このオブジェクトに親があれば
        if (this.transform.parent)
        {
            // Colliderをトリガーに
            handlight.isTrigger = true;
        }
        else
        {
            handlight.isTrigger = false;
        }

        FlashOwder();
        BatteriesCount();
    }

    // ライトのオン・オフ
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
            TurnOff();
        }
    }

    // 点灯
    public void TurnOn()
    {
        spotlight.SetActive(true);

        if (batteries_mag > 0)
        {
            batteries_mag -= 0.5f * Time.deltaTime; // バッテリーを減らす
            if (batteries_mag <= 0)
            {
                TurnOff();
            }
        }
    }

    // 消灯
    public void TurnOff()
    {
        spotlight.SetActive(false);
    }

    // バッテリー計算
    private void BatteriesCount()
    {
        if (transform.parent != null)
        {
            bar.SetActive(true);
        }

        slider.value = batteries_mag / batteries_maxmag; // スライダーに割合を反映
        Debug.Log("slider.value : " + slider.value);
    }

    /// <summary>
    /// バッテリー回復を開始
    /// </summary>
    /// <param name="amount">回復量</param>
    /// <param name="duration">回復にかける時間 (秒)</param>
    public void StartBatteryRestore(float amount, float duration)
    {
        if (restoreCoroutine != null)
        {
            StopCoroutine(restoreCoroutine);
            Debug.Log("既存のバッテリー回復コルーチンを停止");
        }
        Debug.Log($"バッテリー回復を開始: {amount}、所要時間: {duration}");
        restoreCoroutine = StartCoroutine(RestoreBatteryOverTime(amount, duration));
    }

    /// <summary>
    /// バッテリーを時間経過で回復
    /// </summary>
    /// <param name="amount">回復量</param>
    /// <param name="duration">回復にかける時間 (秒)</param>
    /// <returns>IEnumerator</returns>
    private IEnumerator RestoreBatteryOverTime(float amount, float duration)
    {
        float initialBattery = batteries_mag;
        float targetBattery = Mathf.Clamp(batteries_mag + amount, 0, batteries_maxmag);
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            batteries_mag = Mathf.Lerp(initialBattery, targetBattery, elapsedTime / duration);
            slider.value = batteries_mag / batteries_maxmag; // UIスライダーの更新
            Debug.Log($"バッテリー残量: {batteries_mag}");
            yield return null;
        }

        batteries_mag = targetBattery;
        restoreCoroutine = null;
    }
}
