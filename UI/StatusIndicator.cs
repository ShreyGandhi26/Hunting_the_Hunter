using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatusIndicator : MonoBehaviour
{
    [SerializeField]
    private Image healthBarRect;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void setHealth(float _cur, float _max)
    {
        float _value = (float)_cur / _max;
        healthBarRect.fillAmount = _value;
    }
}
