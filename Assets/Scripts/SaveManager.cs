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
    // ������ ���� ����.
    // ������ ����(Data-Format)
    //  = ���ӿ��� ���Ǵ� Ư�� ������ ������ �����ͷ� ��ȯ��ų �� ����ϴ� ����.

    // XML (eXtensible Markup Language)
    // = �ٸ��� ��ũ�� ���. �±׸� �̿��ؼ� �����͸� ����Ѵ�.

    // CSV (Comma Deparated Value)
    // = �����Ͱ� ��ǥ�� �������� ���еǴ� ���
    // = �����̳� ���̺� ������ �����Ϳ��� ���ȴ�.
    [SerializeField] TextAsset csvText;
    [SerializeField] Item[] items;

    [ContextMenu("CSV Read")]
    public void ReadCSV()
    {
        string[] lines = csvText.text.Split('\n');          // ������ ��ü�� ���� �������� �ڸ���.
        items = new Item[lines.Length - 1];                 // ������ �迭�� ������ ��ü ������ �� -1���� �����.

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
