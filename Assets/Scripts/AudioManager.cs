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

    [SerializeField] AudioSource seSource;          // 효과음 스피커.
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
    public void PlaySE(string name)
    {
        for(int i = 0; i< effects.Length; i++)
        {
            if(effects[i].name == name)
            {
                AudioClip clip = effects[i];     // effects의 i번째 대입.
                seSource.clip = clip;            // 스피커에 clip 삽입.
                seSource.Play();                 // 효과음 재생.
                break;
            }
        }
    }

}
