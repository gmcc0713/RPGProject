using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Cinemachine;
using TMPro;
public class UIManager : MonoBehaviour
{
    //================== ΩÃ±€≈Ê==========================================
    public static UIManager Instance { get; private set; }

    void Awake()
    {
        if (null == Instance)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            return;
        }
        Destroy(gameObject);
    }

    //====================================================================
    public Slider mouseSensivitySlider;
    public CinemachineFreeLook cinemachineFreeLook;
    public TextMeshProUGUI interactionText;
    void Start()
    {
        cinemachineFreeLook.m_XAxis.m_MaxSpeed = mouseSensivitySlider.value * 300f;
        interactionText.gameObject.SetActive(false);
    }

    // Update is called once per frame
    public void ShowInteractionText()
    {
        interactionText.gameObject.SetActive(true);
    }
    public void HideInteractionText()
    {
        interactionText.gameObject.SetActive(false);
    }
    public void ShowPanel(GameObject panel)
    {
        panel.SetActive(true);
    }
    public void HidePanel(GameObject panel)
    {
        panel.SetActive(false);
    }
    public void ChangeMouseSensivity()
    {
      cinemachineFreeLook.m_XAxis.m_MaxSpeed =100 +  mouseSensivitySlider.value* 200f;
    }


}
