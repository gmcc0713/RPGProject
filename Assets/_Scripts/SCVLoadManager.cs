using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//생성자에서 호출

public class SCVLoadManager : MonoBehaviour
{
    public static SCVLoadManager Instance { get; private set; }
    public TextAsset m_csvFile;

    void Awake()
    {

        if (null == Instance)
        {
            Instance = this;
            return;
        }
    }
    public List<string[]> Load(string path)
    {
        m_csvFile = (TextAsset)Resources.Load(path);
        string[] data = m_csvFile.text.Split(new char[] { '\n' });

        //테이블의 행들을 하나씩 저장하는 리스트
        List<string[]> tables = new List<string[]>();

        for (int i = 1; i < data.Length - 1; i++)
        {
            //,기준으로 나누기(행으로 나눠짐)
            string[] table = data[i].Split(new char[] { ',' });
            tables.Add(table);
        }


        return tables;
    }

    /* public List<T> Load(string path)
     {
         List<T> datas = new List<T>();
         m_csvFile = (TextAsset)Resources.Load(path);
         string[] data = m_csvFile.text.Split(new char[] { '\n' });

         //테이블의 행들을 하나씩 저장하는 리스트
         List<string[]> tables = new List<string[]>();

         for (int i = 1; i < data.Length - 1; i++)
         {
             //,기준으로 나누기(행으로 나눠짐)
             string[] table = data[i].Split(new char[] { ',' });
             tables.Add(table);
         }
         T item = new T();


         return datas;
     }*/

}
