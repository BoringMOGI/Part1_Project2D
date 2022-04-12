using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioEffect : MonoBehaviour
{
    [SerializeField] AudioSource source;        // 나의 오디오 소스.
    
    public void PlaySE(AudioClip clip)
    {
        source.clip = clip;         // 매게변수로 받은 clip을 source에 삽입.
        source.loop = false;        // loop옵션 비활성화.
        source.Play();              // source를 재생한다.
    }

    private void Update()
    {
        if (!source.isPlaying)
            Destroy(gameObject);    // 나의 게임 오브젝트를 삭제한다.
    }
}
