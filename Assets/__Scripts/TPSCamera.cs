using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TPSCamera : MonoBehaviour
{

    private void Update()
    {
        //카메라와 플레이어 사이의 거리
        float Distance = Vector3.Distance(transform.position, ThirdPersonMovement.Instance.transform.position);
        //카메라가 플레이어를 보는 ㄴ방향
        Vector3 Direction = (ThirdPersonMovement.Instance.transform.position - transform.position).normalized;

        RaycastHit hit;
        if (Physics.Raycast(transform.position, Direction, out hit, Distance))
        {
            
        }
    }

}
