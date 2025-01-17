using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class HandLights : MonoBehaviour
{
    [Header("���C�g�̓����蔻��"), SerializeField]
    private BoxCollider handlight;

    [Header("���C�g�̌�"), SerializeField]
    private GameObject spotlight;

    public bool lightOn;
   
    [Header("�o�b�e���[�e��")]
    public float batteries_mag;
    [Header("�o�b�e���[�ő�e��")]
    public float batteries_maxmag;

    [Header("�o�b�e���[�c�ʕ\��"), SerializeField]
    private Slider slider;

    [SerializeField]    //slider�̃I���E�I�t�p
    private GameObject bar;

    private Coroutine restoreCoroutine; // �o�b�e���[�񕜃R���[�`���̎Q�Ƃ�ێ�

    // Start is called before the first frame update
    void Start()
    {
        spotlight.SetActive(false);
        bar.SetActive(true);
        batteries_mag = batteries_maxmag ; // �o�b�e���[���ő�l�ɏ�����
        //StartBatteryRestore(50f, 5f);
    }

    // Update is called once per frame
    void Update()
    {
        // ���̃I�u�W�F�N�g�ɐe�������
        if (this.transform.parent)
        {
            // Collider���g���K�[��
            handlight.enabled = false;
        }
        else
        {
            handlight.enabled = true;
        }
        BatteriesCount();
    }

    // ���C�g�̃I���E�I�t
    public void TurnOn()
    {
        Debug.Log("���C�g��_�����܂�");
        spotlight.SetActive(true);

        if (batteries_mag > 0)
        {
            batteries_mag -= 0.5f * Time.deltaTime; // �o�b�e���[�����炷
            if (batteries_mag <= 0)
            {
                TurnOff();
            }
        }
    }

    public void TurnOff()
    {
        Debug.Log("���C�g���������܂�");
        spotlight.SetActive(false);
    }


    // �o�b�e���[�v�Z
    private void BatteriesCount()
    {
        if (transform.parent != null)
        {
            bar.SetActive(true);
        }

        slider.value = batteries_mag / batteries_maxmag; // �X���C�_�[�Ɋ����𔽉f
        Debug.Log("slider.value : " + slider.value);
    }

    /// <summary>
    /// �o�b�e���[�񕜂��J�n
    /// </summary>
    /// <param name="amount">�񕜗�</param>
    /// <param name="duration">�񕜂ɂ����鎞�� (�b)</param>
    public void StartBatteryRestore(float amount, float duration)
    {
        if (restoreCoroutine != null)
        {
            StopCoroutine(restoreCoroutine);
            Debug.Log("�����̃o�b�e���[�񕜃R���[�`�����~");
        }
        Debug.Log($"�o�b�e���[�񕜂��J�n: {amount}�A���v����: {duration}");
        restoreCoroutine = StartCoroutine(RestoreBatteryOverTime(amount, duration));
    }

    /// <summary>
    /// �o�b�e���[�����Ԍo�߂ŉ�
    /// </summary>
    /// <param name="amount">�񕜗�</param>
    /// <param name="duration">�񕜂ɂ����鎞�� (�b)</param>
    /// <returns>IEnumerator</returns>
    private IEnumerator RestoreBatteryOverTime(float amount, float duration)
    {
        float initialBattery = batteries_mag;
        float targetBattery = Mathf.Clamp(batteries_mag + amount, 0, batteries_maxmag);
        float elapsedTime = 0f;

        Debug.Log($"�o�b�e���[�񕜊J�n: �����l = {initialBattery}, �ڕW�l = {targetBattery}");

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            batteries_mag = Mathf.Lerp(initialBattery, targetBattery, elapsedTime / duration);
            slider.value = batteries_mag / batteries_maxmag; // UI�X���C�_�[�̍X�V
            Debug.Log($"�񕜒�: �o�b�e���[�c�� = {batteries_mag}");
            yield return null;
        }

        batteries_mag = targetBattery;
        Debug.Log($"�o�b�e���[�񕜊���: �ŏI�l = {batteries_mag}");
        restoreCoroutine = null;
    }

}
