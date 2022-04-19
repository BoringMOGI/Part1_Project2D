using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameClearPanel : MonoBehaviour
{
    [SerializeField] Text eatText;
    [SerializeField] Text goldText;
    [SerializeField] Image[] starImages;

    // ������Ʈ�� Ȱ��ȭ �Ǿ��� ��.
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

        yield return new WaitForSeconds(0.2f);                // 0.5�� ���.
        eatText.text = gm.Eat.ToString("#,##0");              // eatText�� eatCount ���� �߰�.

        yield return new WaitForSeconds(0.2f);                // 0.5�� ���.
        goldText.text = gm.Gold.ToString("#,##0");            // goldText�� gold ���� �߰�.

        yield return new WaitForSeconds(0.2f);                // 0.5�� ���.
        for (int i = 0; i < starImages.Length; i++)
            yield return StartCoroutine(ShowStar(starImages[i].transform));     // �ش� �ڷ�ƾ�� ���������� ���.
    }

    IEnumerator ShowStar(Transform star)
    {
        float scale = 2.0f;
        float speed = 3.0f;
        star.gameObject.SetActive(true);
        star.localScale = new Vector3(scale, scale, 1.0f);

        // scale���� 1.0���� Ŭ �� �ݺ�.
        while (scale > 1.0f)
        {
            // scale ���� Time.deltaTime�� �� ���� �ּ� 1.0f ~ �ִ� MaxValue ���հ��� �ǵ��� ����.
            scale = Mathf.Clamp(scale - (Time.deltaTime * speed), 1.0f, float.MaxValue);
            star.localScale = new Vector3(scale, scale, 1.0f);
            yield return null;
        }
    }
}
