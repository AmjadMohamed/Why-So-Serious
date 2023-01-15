using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorInteraction : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.CompareTag("Player") || collision.transform.CompareTag("Clone"))
        {
            print("You Won");
            GameManager.Instance.WonGame();
        }
    }
}
