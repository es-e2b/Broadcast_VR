using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonSwitch : MonoBehaviour
{
    public void DisableObject()
    {
        gameObject.SetActive(false);
    }
    public void ableObject()
    {
        gameObject.SetActive(true);
    }

    // Start is called before the first frame update
    void Awake()
    {
        DisableObject();
    }
}
