using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    // 싱글톤(Singleton)
    // => 어디에서든 해당 객체에 접근할 수 있는 디자인 패턴 중 하나.
    //    단, 해당 객체는 하나만 존재해야한다.
    static AudioManager instance;
    public static AudioManager Instance => instance;

    [SerializeField] AudioEffect sePrefab;          // 효과음 소스 프리팹.
    [SerializeField] AudioClip[] effects;           // 효과음 배열.

    AudioSource audioSource;

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void PlayBGM()
    {
        audioSource.Play();         // BGM을 재생하라.
    }
    public void StopBGM()
    {
        audioSource.Stop();         // BGM을 멈춰라.
    }
    public void PlaySE(string name)
    {
        // effects배열 순회.
        for(int i = 0; i< effects.Length; i++)
        {
            // i번째의 이름이 매개변수 name과 같으면.
            if(effects[i].name == name)
            {
                AudioClip clip = effects[i];                    // effects의 i번째 대입.
                AudioEffect effect = Instantiate(sePrefab);     // prefab의 복제본 생성.
                effect.PlaySE(clip);                            // 복제본 재생.
                break;
            }
        }
    }

}
