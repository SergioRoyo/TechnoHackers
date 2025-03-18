using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card_mecanics : MonoBehaviour
{
    public float speed = 5f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += Vector3.left * speed * Time.deltaTime;
        StartCoroutine(DestroyCard());
        
    }
    IEnumerator DestroyCard ()
    {
       yield return new WaitForSeconds(2f);
        Destroy(gameObject);
    }
}
