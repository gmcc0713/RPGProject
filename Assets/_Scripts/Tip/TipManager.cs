using UnityEngine;
using TMPro;
using System.Xml;
using System;
using System.Collections;
using System.Collections.Generic;

[Serializable]
public class tipData
{
    public List<string> tipDatas;
}
public class TipManager : MonoBehaviour
{
    public tipData data;
    public TextAsset xmlFile;
    private int dataSize;
    [SerializeField] private TextMeshProUGUI tipText;
    void Start()
    {
        Load();
        dataSize = data.tipDatas.Count;
        StartCoroutine(TipShowRandom());
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
    IEnumerator TipShowRandom()
    {
        int randomIndex;
        while (true)
        {
            randomIndex = UnityEngine.Random.Range(0, dataSize);
            System.Text.StringBuilder timerString = new System.Text.StringBuilder("фа : ");
            timerString.Append(data.tipDatas[randomIndex]);
            tipText.text = timerString.ToString();
            yield return new WaitForSeconds(3.0f);
        }
    }
}



