using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Enemy Data", menuName = "ScriptableObject/MonsterData / Enemy Data Asset", order = 0)]
public class EnemyData : ScriptableObject
{
    [SerializeField] LayerMask targetLayer;
    [SerializeField] EnemyType type;

    [SerializeField] float searchRange = 20.0f;
    [SerializeField] float attackRange = 20.0f;
    [SerializeField] float attackDuration = 2.3f;
    [SerializeField] int atkPower = 10;
    [SerializeField] float maxHealth = 100;
    [SerializeField] float moveSpeed = 5;
    public LayerMask _targetLayer => targetLayer;
    public float _searchRange => searchRange;
    public float _attackRange => attackRange;
    public float _attackDuration => attackDuration;
    public int _atkPower => atkPower;
    public float _maxHealth => maxHealth;
    public float _moveSpeed => moveSpeed;
    public EnemyType _type => type;
}