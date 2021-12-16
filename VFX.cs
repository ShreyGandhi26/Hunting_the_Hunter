using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VFX : MonoBehaviour
{
    [SerializeField]
    private float timer = 0.1f;
    private void Start()
    {
        Destroy(gameObject, timer);
    }
}
