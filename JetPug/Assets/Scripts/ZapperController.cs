using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZapperController : MonoBehaviour
{
    public float rotationSpeed = 50.0f;

    // Start is called before the first frame update
    void Start()
    {
        this.transform.rotation=Quaternion.Euler(new Vector3(0.0f, 0.0f, Random.Range(0.0f, 360.0f)));
    }
}
