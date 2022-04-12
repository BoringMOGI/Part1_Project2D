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

    [SerializeField] AudioEffect sePrefab;          // ȿ���� �ҽ� ������.
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
    public void StopBGM()
    {
        audioSource.Stop();         // BGM�� �����.
    }
    public void PlaySE(string name)
    {
        // effects�迭 ��ȸ.
        for(int i = 0; i< effects.Length; i++)
        {
            // i��°�� �̸��� �Ű����� name�� ������.
            if(effects[i].name == name)
            {
                AudioClip clip = effects[i];                    // effects�� i��° ����.
                AudioEffect effect = Instantiate(sePrefab);     // prefab�� ������ ����.
                effect.PlaySE(clip);                            // ������ ���.
                break;
            }
        }
    }

}
