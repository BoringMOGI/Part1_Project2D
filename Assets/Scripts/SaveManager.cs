using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

// 직렬화(Serialize)
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

    public Item()
    {

    }
    public Item(string name, string grade, int str, int dex, int ap, int luk, int level)
    {
        this.name = name;
        this.grade = grade;
        this.str = str;
        this.dex = dex;
        this.ap = ap;
        this.luk = luk;
        this.level = level;
    }
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

    // Json (JavaScript Object Notaion)
    // = 키:-값 형태로 이루어진 개방형 표준 포멧
    // = 프로그램의 변수 값 표현에 적합하며 확장성이 좋다.

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

    [ContextMenu("CSV Write")]
    public void SaveCSV()
    {
        Item[] items = GetItemArray(3);
        string csv = string.Empty;

        for (int i = 0; i < items.Length; i++)
        {
            Item item = items[i];
            List<string> list = new List<string>();
            list.Add(item.name);
            list.Add(item.grade);
            list.Add(item.str.ToString());
            list.Add(item.dex.ToString());
            list.Add(item.ap.ToString());
            list.Add(item.luk.ToString());
            list.Add(item.level.ToString());

            csv += string.Join(",", list.ToArray()) + "\n";
        }

        SaveFile("csvItemData", csv);
    }

    private class ItemJson
    {
        public Item[] items;
        public ItemJson(Item[] items)
        {
            this.items = items;
        }
    }

    [ContextMenu("ConvertToJson")]
    public void ConvertToJson()
    {
        ItemJson itemJson = new ItemJson(GetItemArray(5));
        string json = JsonUtility.ToJson(itemJson, true);
        SaveFile("itemData", json);
    }

    [ContextMenu("ConvertToObject")]
    public void ConvertToObject()
    {
        string json = LoadFile("itemData");
        object convert = JsonUtility.FromJson(json, typeof(ItemJson));
        ItemJson itemJson = convert as ItemJson;
        Item[] items = itemJson.items;

        this.items = items;
        Debug.Log("아이템 데이터 로드 완료!");
    }

    private Item[] GetItemArray(int count)
    {
        Item[] items = new Item[count];
        for (int i = 0; i < items.Length; i++)
        {
            string itemName = string.Format("롱소드 {0}", Random.Range(0, 100));
            Item newItem = new Item(itemName, "영웅", 100, 200, 10, 50, 200);
            items[i] = newItem;
        }
        return items;
    }
    private void SaveFile(string fileName, string text)
    {
        string path = string.Format("{0}/{1}.txt", Application.dataPath, fileName);
        StreamWriter sw = new StreamWriter(path);
        sw.Write(text);
        sw.Close();

        Debug.Log("파일 저장 완료!");
    }
    private string LoadFile(string fileName)
    {
        string path = string.Format("{0}/{1}.txt", Application.dataPath, fileName);
        StreamReader sr = new StreamReader(path);
        string readToEnd = sr.ReadToEnd();
        sr.Close();
        return readToEnd;
    }

}
