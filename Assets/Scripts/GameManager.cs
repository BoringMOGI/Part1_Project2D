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
        gameOverPanel.SetActive(false);         // 게임 오버 패널 비활성화.
        pausePanel.SetActive(false);            // 일시정지 패널 비활성화.

        StartCoroutine(GameStart());
    }

    private void Update()
    {
        if (!isGameOver && player.isDead)
            StartCoroutine(GameOver());

        // 게임 오버 상태가 아니고 ESC키를 눌렀을때. + 화면전환 중이 아닐 때.
        if (!isGameOver)
        {
            if (Input.GetKeyDown(KeyCode.Escape) && !SceneMover.isFading)
            {
                pausePanel.SetActive(true);     // 일시정지 패널 활성화.
                Time.timeScale = 0f;            // 월드 전체 시간 배율 x0배로 설정.
            }
        }

        if (Input.GetKeyDown(KeyCode.Q))
            AudioManager.Instance.PlaySE("jump");
        if (Input.GetKeyDown(KeyCode.W))
            AudioManager.Instance.PlaySE("light");

    }

    // 일시정지 해제.
    public void OnReleasePause()
    {
        pausePanel.SetActive(false);            // 일시정지 패널 비활성화.
        Time.timeScale = 1f;                    // 월즈 전체 시간 배율 x1배로 설정.
    }
    // 게임 씬에서 나가기.
    public void OnExitGameScene()
    {
        // 타이틀 씬 로드.
        Time.timeScale = 1f;                    // 월즈 전체 시간 배율 x1배로 설정.
        //UnityEngine.SceneManagement.SceneManager.LoadScene("Title");
    }

    public void OnGameClear()
    {
        StartCoroutine(GameClear());
    }

    private IEnumerator GameStart()
    {
        yield return new WaitForSeconds(1f);    // 1초 대기.
        AudioManager.Instance.PlayBGM();        // 싱글톤을 이용해 AudioManager객체에 접근. PlayBGM 호출.

    }
    private IEnumerator GameOver()
    {
        AudioManager.Instance.StopBGM();

        isGameOver = true;
        yield return new WaitForSeconds(1.5f);

        // GameObject.SetActive(bool) : void
        // => 게임 오브젝트를 활성/비활성화 한다.
        gameOverPanel.SetActive(true);
        yield return new WaitForSeconds(4.0f);

        // Game씬을 로드하라.
        UnityEngine.SceneManagement.SceneManager.LoadScene("Game");
    }
    private IEnumerator GameClear()
    {
        player.OnSwitchLockControl(true);

        AudioManager.Instance.StopBGM();
        yield return null;
    }
}
