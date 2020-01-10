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

        yield return new WaitForSeconds(DestroyAfter - .02f);

        Instantiate(DestroyParticles, this.gameObject.transform.position , Quaternion.identity);
    }
}
