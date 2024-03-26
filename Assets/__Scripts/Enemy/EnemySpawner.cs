using System.Collections;
using System.Collections.Generic;

using UnityEngine;



public class EnemySpawner : MonoBehaviour
{
    public GameObject target;

    [SerializeField] private ObjectPool<BaseEnemy> enemyPool;
    public float spawnDelay;
    private float currentTime;
    public bool spawning = false;

    [SerializeField] private bool m_bIsSpawnOnce;

    [SerializeField] private List<Transform> spawnPos = new List<Transform>();

    [SerializeField] private int maxSpawnCount = 4;
    public int curSpawnCount = 0;
    void Start()
    {
        enemyPool.Initialize();
        currentTime = spawnDelay;
    }


    void SpawnFirst()
    {
        for(int i = curSpawnCount; i<maxSpawnCount;i++)
        {
            Debug.Log(i);
            Spawn(spawnPos[i].transform.position);
        }
    }

    void Spawn(Vector3 spawnPosition)
    {
        BaseEnemy clone;
        enemyPool.GetObject(out clone);         //오브젝트 꺼내기

        clone.SetSpawner(this);                 //생성된 enemy data세팅
        clone.SetPosition(spawnPosition);       //위치 세팅

        curSpawnCount++;


        clone.ResetData();
        EnemyManager.Instance.EnemyInit(clone);

        clone.Run();                                        //오브젝트작동시키기
                                                            //스폰 코드

    }
    public void DieEnemy(BaseEnemy enemy)
    {
        curSpawnCount--;
        if(!m_bIsSpawnOnce)
        {
            StartCoroutine(RespawnEnemy(enemy._defaultPos));
        }
        enemyPool.PutInPool(enemy);

    }
    public IEnumerator RespawnEnemy(Vector3 pos)
    {

        yield return new WaitForSeconds(1f);
        if (CanEnemySpawnCountCheck())
        {
            Spawn(pos);
        }
        
    }

    public bool CanEnemySpawnCountCheck()          //스폰할수 있는 갯수 확인
    {
        
        if ( maxSpawnCount > curSpawnCount)
        {
            return true;
        }
        return false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            SpawnFirst();
            spawning = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        spawning = false;
        Debug.Log("스폰 영역 나감");
    }
}
