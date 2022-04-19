using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UserInfoUI : MonoBehaviour
{
    [SerializeField] Image[] hpImages;          // UI 이미지 배열.
    [SerializeField] Text eatText;              // 먹은 개수 Text.
    [SerializeField] Text goldText;             // 소지 골드 Text.

    Player player;              // 플레이어.
    GameManager gm;             // 게임 매니저.

    private void Start()
    {
        player = Player.Instance;
        gm = GameManager.Instance;
    }
    private void Update()
    {
        SetHpImage(player.Hp);

        eatText.text = gm.Eat.ToString("#,##0");
        goldText.text = gm.Gold.ToString("#,##0");
    }

    private void SetHpImage(int hp)
    {
        for (int i = 0; i < 3; i++)
        {
            // component.enable : 해당 컴포넌트를 활성/비활성화 시킨다.
            hpImages[i].enabled = i < hp;
        }
    }
}
