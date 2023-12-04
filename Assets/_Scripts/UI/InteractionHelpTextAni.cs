using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InteractionHelpTextAni : MonoBehaviour
{
    public TextMeshProUGUI interactionText;
    public Image spaceBar;
    [SerializeField] private Sprite[] spaceBarSprite;
    // Start is called before the first frame update
    public IEnumerator RunInterectAnimation()
    {
        while (true)
        {
            spaceBar.sprite = spaceBarSprite[0];
            yield return new WaitForSeconds(0.5f);
            spaceBar.sprite = spaceBarSprite[1];
            yield return new WaitForSeconds(0.5f);
        }
    }

    private void OnEnable()
    {
        StartCoroutine(RunInterectAnimation());
    }
    private void OnDisable()
    {
            
        StopCoroutine(RunInterectAnimation());
    }
}
