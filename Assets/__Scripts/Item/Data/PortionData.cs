using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PortionType
{
    HP = 0,
    MP,

}

public class PortionData : ItemData
{
    public int m_iID;
    public PortionType m_eType;
    public int m_iValue;
    public int m_iCoolTime;
    public void SetPortionData(string[] datas)
    {
        m_iID = int.Parse(datas[0]);
        m_eType = System.Enum.Parse<PortionType>(datas[1]);
        m_iValue = int.Parse(datas[2]);
        m_iCoolTime = int.Parse(datas[3]);
    }
}