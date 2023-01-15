using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class RegisterMobileButtons : MonoBehaviour
{
    //public
    [SerializeField] EventTrigger btn_MoveRight;
    [SerializeField] EventTrigger btn_MoveLeft;
    [SerializeField] Button btn_Jump;

    //private
    EventTrigger.Entry MoveRight;
    EventTrigger.Entry MoveLeft;
    EventTrigger.Entry Idle;



    private void Start()
    {
        if (GameObject.FindGameObjectWithTag("Right Button") != null && GameObject.FindGameObjectWithTag("Left Button") != null && GameObject.FindGameObjectWithTag("Jump Button") != null)
        {
            btn_MoveRight = GameObject.FindGameObjectWithTag("Right Button").GetComponent<EventTrigger>();
            btn_MoveLeft = GameObject.FindGameObjectWithTag("Left Button").GetComponent<EventTrigger>();
            btn_Jump = GameObject.FindGameObjectWithTag("Jump Button").GetComponent<Button>();

            // move right
            MoveRight = new EventTrigger.Entry();
            MoveRight.eventID = EventTriggerType.PointerDown;
            MoveRight.callback.AddListener((fun) => { OnPointerDownMoveRight((PointerEventData)fun); });
            btn_MoveRight.triggers.Add(MoveRight);

            //move left
            MoveLeft = new EventTrigger.Entry();
            MoveLeft.eventID = EventTriggerType.PointerDown;
            MoveLeft.callback.AddListener((fun1) => { OnPointerDownMoveLeft((PointerEventData)fun1); });
            btn_MoveLeft.triggers.Add(MoveLeft);

            // set idle
            Idle = new EventTrigger.Entry();
            Idle.eventID = EventTriggerType.PointerUp;
            Idle.callback.AddListener((fun) => { OnPointerUp((PointerEventData)fun); });
            btn_MoveRight.triggers.Add(Idle);
            btn_MoveLeft.triggers.Add(Idle);

            // Jump
            btn_Jump.onClick.AddListener(this.GetComponent<Clone_Script>().Jump);
        }
    }

    private void OnDestroy()
    {
        if (GameObject.FindGameObjectWithTag("Right Button") != null && GameObject.FindGameObjectWithTag("Left Button") != null && GameObject.FindGameObjectWithTag("Jump Button") != null)
        {
            // unsubscribe events
            btn_MoveRight.triggers.Remove(MoveRight);
            btn_MoveLeft.triggers.Remove(MoveLeft);
            btn_MoveRight.triggers.Remove(Idle);
            btn_MoveLeft.triggers.Remove(Idle);
            btn_Jump.onClick.RemoveListener(this.GetComponent<Clone_Script>().Jump);
        }
    }

    public void OnPointerDownMoveRight(PointerEventData data)
    {
        this.GetComponent<Clone_Script>().SetWalkingToTheRight();
    }

    public void OnPointerDownMoveLeft(PointerEventData data)
    {
        this.GetComponent<Clone_Script>().SetWalkingToTheLeft();
    }
    public void OnPointerUp(PointerEventData data)
    {
        this.GetComponent<Clone_Script>().SetIdle();
    }


}
