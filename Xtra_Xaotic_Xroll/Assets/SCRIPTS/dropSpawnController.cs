using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dropSpawnController : MonoBehaviour
{
    public float dropForce;
    // Start is called before the first frame update
    void Start()
    {
        int a = Random.value < 0.5f ? -1 : 1;
        StartCoroutine(dropSpawn());
        gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2((dropForce/2) * a, dropForce));
        Debug.Log("a");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator dropSpawn()
    {
        gameObject.GetComponent<CircleCollider2D>().enabled = false;
        yield return new WaitForSeconds(0.5f);
        gameObject.GetComponent<CircleCollider2D>().enabled = true;
    }
}
