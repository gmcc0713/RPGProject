using UnityEngine;
using TMPro;
using System.Xml;
using System;
using System.Collections;
using System.Collections.Generic;

[Serializable]
public class toolTipData
{
    public List<string> tooltip;
    public List<string> tipDatas;
}
public class ItemToolTipManager : MonoBehaviour
{
    public tipData data;
    public TextAsset xmlFile;
    private int dataSize;
    [SerializeField] private TextMeshProUGUI ItemtoolTip;
    void Start()
    {
        Load();
        dataSize = data.tipDatas.Count;
    }
    void Save()
    {
        XmlDocument xmlDocument = new XmlDocument();
    }


    void Load()
    {
        var txtAsset = (TextAsset)Resources.Load("XML/TipDatas");
        XmlDocument xmlDocument = new XmlDocument();
        xmlDocument.LoadXml(txtAsset.text);

        XmlNodeList tipNodes = xmlDocument.SelectNodes("/Tips/tip");
        foreach (XmlNode tipNode in tipNodes)
        {
            string tipText = tipNode.InnerText;
            data.tipDatas.Add(tipText);
            Debug.Log(tipText);
        }
    }

}



