using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyObject : MonoBehaviour
{
    [SerializeField] GameObject DestroyParticles;
    [SerializeField] float DestroyAfter ;


    void Update()
    {
        Destroy(this.gameObject, DestroyAfter);

        if(transform.tag == "Box" || transform.tag == "Clone")
        {
            StartCoroutine("DestroyObj");
        }
        
    }

    IEnumerator DestroyObj()
    {
        // the -.02 to instantiate almost 1 or 2 particles before getting fully destroyed.
        yield return new WaitForSeconds(DestroyAfter - .02f);

        Instantiate(DestroyParticles, this.gameObject.transform.position , Quaternion.identity);
        this.GetComponent<Collider2D>().enabled = false; // this will do the trick to call the oncollisionexit for the buttons
    }
}
