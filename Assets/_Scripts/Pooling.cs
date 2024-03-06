using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPoolingObject
{
    void SetPosition(Vector3 pos);
    
}
[System.Serializable]
public class ObjectPool<T> where T : MonoBehaviour, IPoolingObject
{
    [SerializeField] T targetObject;                            //대상 오브젝트
    [SerializeField] [Range(1, 100)] int poolingAmount = 1;     //풀링 갯수
    Transform containerObject;                                  //풀링할 장소를 저장하는 부모 오브잭트
    Queue<T> objectPool;                                        //풀링할 장소
                                  //
    public void SettingParent(Transform parent = null)
    {
        containerObject = parent;
    }
    public bool Initialize()
    {
        if (!targetObject || containerObject) return false;     
        if (1 > poolingAmount) poolingAmount = 1;                            //풀링되어있는 양 1
        System.Text.StringBuilder sb = new System.Text.StringBuilder();     //+연산으로는 GC가 발생하기 때문에 StringBuilder의 Append사용
        sb.Append("Object Pool Container : ");
        sb.Append(targetObject.name);
        containerObject = new GameObject(sb.ToString()).transform;
        objectPool = new Queue<T>();                                            //풀링할 장소 할당
        MakeAndPooling();
        return true;
    }
    bool MakeAndPooling()
    {
        if (!containerObject) return false;
        T poolObject;
        for (int i = 0; poolingAmount > i; i++)
        {
            poolObject = MonoBehaviour.Instantiate(targetObject, containerObject);
            poolObject.name = targetObject.name;
            
            poolObject.gameObject.SetActive(false);
            objectPool.Enqueue(poolObject);       
        }
        return true;
    }

    public bool GetObject(out T item)
    {
        item = null;
        if (!containerObject) return false;
        if (0 >= objectPool.Count)
        {
            if (!MakeAndPooling()) return false;
        }
        item = objectPool.Dequeue();                                            //해당 오브젝트 사용을 위해 가져오기
        item.gameObject.SetActive(true);
        return true;
    }
    /// <summary> item을 비활성화 시키고 Pool에 넣는다. </summary>
    public bool PutInPool(T item)                                               //사용이 끝났을 경우다시 넣기
    {
        if (!(item && containerObject)) return false;
        item.gameObject.SetActive(false);

        objectPool.Enqueue(item);
        item.transform.SetParent(containerObject);
        return true;
    }
    public bool Destroy()
    {
        if (!containerObject) return false;
        MonoBehaviour.Destroy(containerObject.gameObject);
        containerObject = null;
        objectPool.Clear();
        objectPool = null;
        return true;
    }

    public void ReturnBackPool()
    {
        if (containerObject)
        {
            // 모든 자식을 순회 한다.
            foreach (Transform child in containerObject)
            {
                if (child.gameObject.activeSelf)
                {
                    if (child.TryGetComponent(out T item)) PutInPool(item);
                }
            }
        }
    }
} // class ObjectPool

