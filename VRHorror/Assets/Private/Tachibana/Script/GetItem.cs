using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class GetItem : MonoBehaviour
{

    public Collider getitem = null;

    [Header("true�E,false��")]    //��̍��E����
    public bool hand = false;

    [Header("�E��A�j���[�V����")]
    public HandAnimation handAnim_R;

  
    [SerializeField]
    private GameManager gameManager;

    [SerializeField]
    private HandLights handLights = null;

    private void Start()
    {
      
    }

    private void Update()
    {
        if (!gameManager.gameOver)
        {
            FlashMove();
        }
           

        HandMotion();
    }

    //�A�C�e�����E��
    private void OnTriggerStay(Collider other)
    {
        // �o�b�e���[���E������
        if (other.CompareTag("Batteries"))
        {
            Batteries batteries = other.GetComponent<Batteries>();
            if (batteries != null)
            {
                    handLights.StartBatteryRestore(batteries.batteryRestoreAmount, batteries.restoreDuration);
                    Debug.Log($"�o�b�e���[��K�p: {batteries.batteryRestoreAmount}�A���v����: {batteries.restoreDuration} �b");
                    Destroy(other.gameObject); // �o�b�e���[���폜
            }
        }

        //�����E��
        if (other.CompareTag("GoalKey"))
        {
            gameManager.goalKey = true;
            Destroy(other.gameObject);
        }

        //�Q�[���N���A
        if (other.CompareTag("GoalDoor") && gameManager.goalKey)
        {
            gameManager.gameClear = true;
        }

    }

    //���C�g�����点��
    public void FlashMove()
    {
        if (OVRInput.GetDown(OVRInput.RawButton.LIndexTrigger))
        {
            if (!handLights.lightOn)
            {
                handLights.TurnOn();
            }
            else if (handLights.lightOn)
            {
                handLights.TurnOff();
            }
        }
    }

    

    private void HandMotion()
    {

        if (handAnim_R != null)
        {
            bool isGrabR = OVRInput.Get(OVRInput.RawButton.RHandTrigger) && hand || Input.GetMouseButton(0) && hand;
            handAnim_R.animator.SetBool("IsGrabR", isGrabR);
        }

    }

}
