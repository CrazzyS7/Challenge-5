using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyObjectX : MonoBehaviour
{
    private int mTimeSec = 2;

    void Start()
    {
        Destroy(gameObject, mTimeSec); // destroy particle after 2 seconds
    }
}
