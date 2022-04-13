using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UserInfoUI : MonoBehaviour
{
    [SerializeField] Player player;             // 플레이어.
    [SerializeField] Image[] hpImages;          // UI 이미지 배열.
    [SerializeField] Text coinText;             // UI 텍스트.

    private void Update()
    {
        SetHpImage(player.Hp);
        SetCoinText(player.Coin);
    }

    private void SetHpImage(int hp)
    {
        for (int i = 0; i < 3; i++)
        {
            // component.enable : 해당 컴포넌트를 활성/비활성화 시킨다.
            hpImages[i].enabled = i < hp;
        }
    }
    private void SetCoinText(int coin)
    {
        // int형 값 coin을 string으로 변환시켜야한다.
        // object.ToString() : ToString은 문자열로 변경시켜라.
        coinText.text = coin.ToString();
    }
}
