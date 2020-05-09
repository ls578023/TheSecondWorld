using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate : MonoBehaviour
{
    void Update()
    {
        transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles + Vector3.up * Time.deltaTime * 10);
    }
}
