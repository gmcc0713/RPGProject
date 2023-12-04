using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public enum ItemType
{
   Weapon = 0,           //���������
   Armor ,              //��������
   Portion ,            //���Ǿ�����
   Etc ,                //��Ÿ������
}
public class ItemData : ScriptableObject
{
    public ItemType itemtype;           //������ Ÿ��
    public int itemId = -1;             //������ ���̵�
    public string itemName;             //������ �̸�

    public Sprite itemIcon;             //������ ������
    public bool isEquipmentItem;        //�������� ���� ���� ����


    public int price;               //������ �ǸŰ���

}



