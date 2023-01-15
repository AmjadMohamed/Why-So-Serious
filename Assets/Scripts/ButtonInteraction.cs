using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonInteraction : MonoBehaviour
{
    [SerializeField] Sprite Box_Default;
    [SerializeField] Sprite Box_Pressed;


    private void OnCollisionEnter2D(Collision2D collision)
    {
        GameManager.Instance.PressedBoxesCounter++;
        this.GetComponent<SpriteRenderer>().sprite = Box_Pressed;
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (!collision.transform.CompareTag("Knife"))
        {
            GameManager.Instance.PressedBoxesCounter --;
            this.GetComponent<SpriteRenderer>().sprite = Box_Default;
        }
    }
}
