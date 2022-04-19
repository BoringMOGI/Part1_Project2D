using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
class Item
{
    public string name;
    public string grade;
    public int str;
    public int dex;
    public int ap;
    public int luk;
    public int level;
}

public class SaveManager : MonoBehaviour
{
    // 데이터 저장 형식.
    // 데이터 포맷(Data-Format)
    //  = 게임에서 사용되는 특정 값들을 별도의 데이터로 변환시킬 때 사용하는 형식.

    // XML (eXtensible Markup Language)
    // = 다목적 마크업 언어. 태그를 이용해서 데이터를 기술한다.

    // CSV (Comma Deparated Value)
    // = 데이터가 쉼표를 기준으로 구분되는 방식
    // = 엑셀이나 테이블 형태의 데이터에서 사용된다.
    [SerializeField] TextAsset csvText;
    [SerializeField] Item[] items;

    [ContextMenu("CSV Read")]
    public void ReadCSV()
    {
        string[] lines = csvText.text.Split('\n');          // 데이터 전체를 띄어쓰기 기준으로 자른다.
        items = new Item[lines.Length - 1];                 // 아이템 배열의 개수를 전체 데이터 수 -1개로 만든다.

        for (int i = 0; i < lines.Length; i++)
        {
            if (i <= 0)
                continue;

            string[] elements = lines[i].Split(',');

            Item item = new Item();
            item.name = elements[0];
            item.grade = elements[1];
            item.str = int.Parse(elements[2]);
            item.dex = int.Parse(elements[3]);
            item.ap = int.Parse(elements[4]);
            item.luk = int.Parse(elements[5]);
            item.level = int.Parse(elements[6]);

            items[i - 1] = item;
        }
    }
}
