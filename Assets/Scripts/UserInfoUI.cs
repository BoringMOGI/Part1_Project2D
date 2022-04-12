using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UserInfoUI : MonoBehaviour
{
    [SerializeField] Player player;             // �÷��̾�.
    [SerializeField] Image[] hpImages;          // UI �̹��� �迭.
    [SerializeField] Text coinText;             // UI �ؽ�Ʈ.

    private void Update()
    {
        SetHpImage(player.Hp);
        SetCoinText(player.Coin);
    }

    private void SetHpImage(int hp)
    {
        for (int i = 0; i < 3; i++)
        {
            // component.enable : �ش� ������Ʈ�� Ȱ��/��Ȱ��ȭ ��Ų��.
            hpImages[i].enabled = i < hp;
        }
    }
    private void SetCoinText(int coin)
    {
        // int�� �� coin�� string���� ��ȯ���Ѿ��Ѵ�.
        // object.ToString() : ToString�� ���ڿ��� ������Ѷ�.
        coinText.text = coin.ToString();
    }
}
