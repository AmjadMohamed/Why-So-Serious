using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyObject : MonoBehaviour
{
    public GameObject DestroyParticles;
    public float DestroyAfter ;


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
    }
}
