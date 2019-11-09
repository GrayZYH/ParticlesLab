using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Test : MonoBehaviour
{
    public GameObject NamePrefab;
    public string Name = "Gray Zhang";
    public int NameDis;
    private List<GameObject> NameSamples;

    private void Start()
    {
        NameSamples = new List<GameObject>();
    }

    public void ActiveNameEffect()
    {
        int nameLenth = Name.Length;
        Debug.Log("姓名长度为：" + nameLenth);
        char[] nameChars = Name.ToCharArray();
        for (int i = 0; i < nameLenth; i++)
        {
            GameObject tempName = Instantiate(NamePrefab) as GameObject;
            tempName.transform.parent = this.transform;
            tempName.transform.name = "NameIndex" + (i + 1).ToString();
            tempName.transform.localPosition = new Vector3(i * NameDis, 0f, 0f);
            tempName.transform.localScale = Vector3.one;
            tempName.gameObject.GetComponent<Text>().text = nameChars[i].ToString();
            NameSamples.Add(tempName);
        }
    }
}
