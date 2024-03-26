using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[System.Serializable]
public class EnemyData 
{
    [SerializeField] LayerMask targetLayer;
    [SerializeField] int enemyID;
    [SerializeField] string enemyName;
    [SerializeField] EnemyType type;

    [SerializeField] float searchRange;

    [SerializeField] float attackRange;
    [SerializeField] float attackDuration;
    [SerializeField] float attackDamage;
    [SerializeField] float maxHealth;
    [SerializeField] float moveSpeed;
    [SerializeField] int dropItemID;


    

    public LayerMask _targetLayer => targetLayer;
    public float _searchRange => searchRange;
    public float _attackRange => attackRange;
    public float _attackDuration => attackDuration;
    public float _atkPower => attackDamage;
    public float _maxHealth => maxHealth;
    public float _moveSpeed => moveSpeed;
    public EnemyType _type => type;
    public int _enemyID => enemyID;
    public string _enemyName => enemyName;
    public int _dropItemID=> dropItemID;

    public void SetData(string[] datas)
    {
        enemyID = int.Parse(datas[0]);
        enemyName = datas[1];
        type = System.Enum.Parse<EnemyType>(datas[2]);
        //로드를 여러번 할 경우 확인 해야된다(dictionary에 저장 후 키값으로 확인)
        //더이상 사용 안 할때 해제 필수(이때 dictionary도 같이 해제)

        searchRange = float.Parse(datas[3]);
        attackRange = float.Parse(datas[4]);
        attackDuration = float.Parse(datas[5]);
        attackDamage = float.Parse(datas[6]);
        maxHealth = float.Parse(datas[7]);
        moveSpeed = float.Parse(datas[8]);
        dropItemID = int.Parse(datas[9]);

        //enemyIcon = Resources.Load<Sprite>(datas[3]);

    }
}