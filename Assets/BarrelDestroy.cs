using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarrelDestroy : MonoBehaviour
{
    // when destroy barrel, spawn 4 gem objects
    public GameObject gem;
    public GameObject gem2;
    public GameObject gem3;
    public GameObject gem4;

    void OnDestroy()
    {
        // spawn 4 gems at differnt positions within the barrel in a horizontal line
        Instantiate(gem, transform.position + new Vector3(0.2f, 0, 0), Quaternion.identity);
        Instantiate(gem2, transform.position + new Vector3(0, 0.2f, 0), Quaternion.identity);
        Instantiate(gem3, transform.position + new Vector3(-0.2f, 0, 0), Quaternion.identity);
        Instantiate(gem4, transform.position + new Vector3(0, -0.2f, 0), Quaternion.identity);

    }
}
