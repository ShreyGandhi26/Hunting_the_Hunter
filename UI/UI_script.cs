using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_script : MonoBehaviour
{
    [SerializeField]
    private RectTransform fuel;

    public GameObject fueldata;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    public void setFuel(float _cur, float _max)
    {
        float _value = (float)_cur / _max;
        fuel.localScale = new Vector3(_value * 2, fuel.localScale.y, fuel.localScale.z);
    }
}
