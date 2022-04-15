using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] Player player;
    [SerializeField] GameObject gameOverPanel;
    [SerializeField] GameObject pausePanel;
    [SerializeField] SceneMover sceneMover;

    bool isGameOver = false;

    private void Start()
    {
        gameOverPanel.SetActive(false);         // ���� ���� �г� ��Ȱ��ȭ.
        pausePanel.SetActive(false);            // �Ͻ����� �г� ��Ȱ��ȭ.

        StartCoroutine(GameStart());
    }

    private void Update()
    {
        if (!isGameOver && player.isDead)
            StartCoroutine(GameOver());

        // ���� ���� ���°� �ƴϰ� ESCŰ�� ��������. + ȭ����ȯ ���� �ƴ� ��.
        if (!isGameOver)
        {
            if (Input.GetKeyDown(KeyCode.Escape) && !SceneMover.isFading)
            {
                pausePanel.SetActive(true);     // �Ͻ����� �г� Ȱ��ȭ.
                Time.timeScale = 0f;            // ���� ��ü �ð� ���� x0��� ����.
            }
        }

        if (Input.GetKeyDown(KeyCode.Q))
            AudioManager.Instance.PlaySE("jump");
        if (Input.GetKeyDown(KeyCode.W))
            AudioManager.Instance.PlaySE("light");

    }

    // �Ͻ����� ����.
    public void OnReleasePause()
    {
        pausePanel.SetActive(false);            // �Ͻ����� �г� ��Ȱ��ȭ.
        Time.timeScale = 1f;                    // ���� ��ü �ð� ���� x1��� ����.
    }
    // ���� ������ ������.
    public void OnExitGameScene()
    {
        // Ÿ��Ʋ �� �ε�.
        Time.timeScale = 1f;                    // ���� ��ü �ð� ���� x1��� ����.
        //UnityEngine.SceneManagement.SceneManager.LoadScene("Title");
    }

    public void OnGameClear()
    {
        StartCoroutine(GameClear());
    }

    private IEnumerator GameStart()
    {
        yield return new WaitForSeconds(1f);    // 1�� ���.
        AudioManager.Instance.PlayBGM();        // �̱����� �̿��� AudioManager��ü�� ����. PlayBGM ȣ��.

    }
    private IEnumerator GameOver()
    {
        AudioManager.Instance.StopBGM();

        isGameOver = true;
        yield return new WaitForSeconds(1.5f);

        // GameObject.SetActive(bool) : void
        // => ���� ������Ʈ�� Ȱ��/��Ȱ��ȭ �Ѵ�.
        gameOverPanel.SetActive(true);
        yield return new WaitForSeconds(4.0f);

        // Game���� �ε��϶�.
        UnityEngine.SceneManagement.SceneManager.LoadScene("Game");
    }
    private IEnumerator GameClear()
    {
        player.OnSwitchLockControl(true);

        AudioManager.Instance.StopBGM();
        yield return null;
    }
}
