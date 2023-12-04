using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoadingSceneController : MonoBehaviour
{
    [SerializeField] Slider loadingBar;

    void Start()
    {
        StartCoroutine(LoadingSceneProgress());
    }


    IEnumerator LoadingSceneProgress()
    {
        //비동기 방식(씬을 불러오는 도중 다른작업이 가능함)
        AsyncOperation op =   SceneManager.LoadSceneAsync("PlayScene");
        //씬을 비동기로 불러들일때 씬의 로딩이 끝나면 자동으로 불러온 씬으로 이동할건지 설정(로딩시간이 너무 짧을때를 대비)
        op.allowSceneActivation = false;    

        float timer = 0;
        //bar를 차오르게 하기위해 op가 끝나지 않았을때 계속 실행
        while (!op.isDone)   
        {
            yield return null;
            if (op.progress < 0.9f)     //씬의 로딩이 90퍼 까지 로딩 바를 채운다
            {
                loadingBar.value = op.progress;
            }
            else                        //나머지 10퍼는 1초동안 채운뒤 씬을 불러온다
            {
                timer += Time.unscaledDeltaTime;
                loadingBar.value = Mathf.Lerp(0.9f, 1f, timer);
                if(loadingBar.value >= 1f)
                {
                    op.allowSceneActivation = true;
                    yield return null;
                }
            }

        }
    }
}
