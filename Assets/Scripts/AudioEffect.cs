using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioEffect : MonoBehaviour
{
    [SerializeField] AudioSource source;        // ���� ����� �ҽ�.
    
    public void PlaySE(AudioClip clip)
    {
        source.clip = clip;         // �ŰԺ����� ���� clip�� source�� ����.
        source.loop = false;        // loop�ɼ� ��Ȱ��ȭ.
        source.Play();              // source�� ����Ѵ�.
    }

    private void Update()
    {
        if (!source.isPlaying)
            Destroy(gameObject);    // ���� ���� ������Ʈ�� �����Ѵ�.
    }
}
