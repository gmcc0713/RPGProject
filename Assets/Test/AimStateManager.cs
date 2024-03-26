using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class AimStateManager : MonoBehaviour
{

    public Cinemachine.AxisState m_xAxis, m_yAxis;
    [SerializeField] Transform m_followCamTrans;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        m_xAxis.Update(Time.deltaTime);
        m_yAxis.Update(Time.deltaTime);
    }
    private void LateUpdate()
    {
        m_followCamTrans.localEulerAngles = new Vector3(m_yAxis.Value, m_followCamTrans.localEulerAngles.y, m_followCamTrans.localEulerAngles.z);
        transform.localEulerAngles = new Vector3(transform.eulerAngles.x, m_xAxis.Value, transform.eulerAngles.z);
    }
}
