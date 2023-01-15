using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapsInteraction : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(!collision.transform.CompareTag("Knife"))
        {
            this.GetComponent<Animator>().enabled = false;
            this.GetComponent<Collider2D>().enabled = false;
            Destroy(collision.gameObject);
        }

        if(collision.transform.CompareTag("Knife"))
        {
            Destroy(collision.gameObject);
        }

        if(collision.transform.CompareTag("Player"))
        {
            GameManager.Instance.LoseCause.text = "You Lost Because You Died!!";
            GameManager.Instance.LostGame();
        }
    }
}
