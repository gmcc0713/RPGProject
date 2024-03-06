using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml;

public class DropItemLoader : MonoBehaviour
{
    public TextAsset csvFile;
    public List<EnemyDropItemDatas> dropItemDatas;

    private void Awake()
    {
        dropItemDatas = new List<EnemyDropItemDatas>();
        LoadDropItemTable();
    }
    public void LoadDropItemTable()
    {
        csvFile =  (TextAsset)Resources.Load("CSV/Enemy/EnemyDropTable");
        string[] data = csvFile.text.Split(new char[] { '\n' });

        //테이블의 행들을 하나씩 저장하는 리스트
        List<string[]> tables = new List<string[]>();
        for (int i = 1; i < data.Length-1; i++)
        {
            //,기준으로 나누기(행으로 나눠짐)
            string[] table = data[i].Split(new char[] { ',' });
            tables.Add(table);
        }
        for (int i =0; i<tables.Count;i++)
        {
            EnemyDropItemDatas dropItems = new EnemyDropItemDatas();
            dropItems.dropItems = new List<DropItemData>();

            string[] itemID = tables[i][1].Split(new char[] { ';' });
            string[] dropPercent = tables[i][2].Split(new char[] { ';' });
            dropItems.gold = int.Parse(tables[i][3]);
            dropItems.exp = int.Parse(tables[i][4]);
            DropItemData itemdata = new DropItemData();
            for (int j = 0; j < itemID.Length; j++)
            {
                itemdata.itemID = int.Parse(itemID[j]);
                itemdata.dropPercent = int.Parse(dropPercent[j]);
                dropItems.dropItems.Add(itemdata);

            }


            dropItemDatas.Add(dropItems);

        }

       
    }
    public EnemyDropItemDatas GetDropItemDatas(int dropID)
    {
        return dropItemDatas[dropID];
    }
}
