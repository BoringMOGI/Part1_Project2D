using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UserInfoUI : MonoBehaviour
{
    [SerializeField] Image[] hpImages;          // UI �̹��� �迭.
    [SerializeField] Text eatText;              // ���� ���� Text.
    [SerializeField] Text goldText;             // ���� ��� Text.

    Player player;              // �÷��̾�.
    GameManager gm;             // ���� �Ŵ���.

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
            // component.enable : �ش� ������Ʈ�� Ȱ��/��Ȱ��ȭ ��Ų��.
            hpImages[i].enabled = i < hp;
        }
    }
}
