using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class UICheckButton : MonoBehaviour
{
    public Button checkBtn;
    public GameObject onCheckBtn;
    private bool isOn;

    void Start()
    {
        checkBtn.onClick.AddListener(() =>
        {
            if (isOn == false)
            {
                OnCheckBox();
            }
            else
            {
                OffCheckBox();
            }
        });
    }

    public void OnCheckBox()
    {
        onCheckBtn.SetActive(true);
        isOn = true;
    }

    public void OffCheckBox()
    {
        onCheckBtn.SetActive(false);
        isOn = false;
    }
}

