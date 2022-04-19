using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameClearPanel : MonoBehaviour
{
    [SerializeField] Text eatText;
    [SerializeField] Text goldText;
    [SerializeField] Image[] starImages;

    // 오브젝트가 활성화 되었을 때.
    private void OnEnable()
    {
        eatText.text = "0";
        goldText.text = "0";
        for (int i = 0; i < starImages.Length; i++)
            starImages[i].gameObject.SetActive(false);

        StartCoroutine(ShowResult());
    }

    IEnumerator ShowResult()
    {
        GameManager gm = GameManager.Instance;

        yield return new WaitForSeconds(0.2f);                // 0.5초 대기.
        eatText.text = gm.Eat.ToString("#,##0");              // eatText에 eatCount 문자 추가.

        yield return new WaitForSeconds(0.2f);                // 0.5초 대기.
        goldText.text = gm.Gold.ToString("#,##0");            // goldText에 gold 문자 추가.

        yield return new WaitForSeconds(0.2f);                // 0.5초 대기.
        for (int i = 0; i < starImages.Length; i++)
            yield return StartCoroutine(ShowStar(starImages[i].transform));     // 해당 코루틴이 끝날때까지 대기.
    }

    IEnumerator ShowStar(Transform star)
    {
        float scale = 2.0f;
        float speed = 3.0f;
        star.gameObject.SetActive(true);
        star.localScale = new Vector3(scale, scale, 1.0f);

        // scale값이 1.0보다 클 때 반복.
        while (scale > 1.0f)
        {
            // scale 값에 Time.deltaTime을 뺀 값이 최소 1.0f ~ 최대 MaxValue 사잇값이 되도록 고정.
            scale = Mathf.Clamp(scale - (Time.deltaTime * speed), 1.0f, float.MaxValue);
            star.localScale = new Vector3(scale, scale, 1.0f);
            yield return null;
        }
    }
}
