using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Balloon : MonoBehaviour
{
    public ParticleSystem confetti;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(this.transform.parent == null)
        {
            transform.Translate(Vector3.up * Time.deltaTime * 0.5f);
            StartCoroutine("Explode");
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            StartCoroutine("Explode");
        }
    }

    IEnumerator Explode()
    {
        yield return new WaitForSeconds(5f);
        Instantiate(confetti, this.transform.position, Quaternion.identity);
        Destroy(this.gameObject);
    }
}
