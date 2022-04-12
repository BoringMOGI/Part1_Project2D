using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    // �̱���(Singleton)
    // => ��𿡼��� �ش� ��ü�� ������ �� �ִ� ������ ���� �� �ϳ�.
    //    ��, �ش� ��ü�� �ϳ��� �����ؾ��Ѵ�.
    static AudioManager instance;
    public static AudioManager Instance => instance;

    [SerializeField] AudioSource seSource;          // ȿ���� ����Ŀ.
    [SerializeField] AudioClip[] effects;           // ȿ���� �迭.

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
        audioSource.Play();         // BGM�� ����϶�.
    }
    public void PlaySE(string name)
    {
        for(int i = 0; i< effects.Length; i++)
        {
            if(effects[i].name == name)
            {
                AudioClip clip = effects[i];     // effects�� i��° ����.
                seSource.clip = clip;            // ����Ŀ�� clip ����.
                seSource.Play();                 // ȿ���� ���.
                break;
            }
        }
    }

}
