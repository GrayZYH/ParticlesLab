using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FontTest : MonoBehaviour
{
    public Text NameText;
    public Font[] Fonts;
    public string[] Names;

    public void Test()
    {
        if (checkString(Names[1]) == true)
        {
            Debug.Log("检测到是中文！");
            NameText.font = Fonts[0];
            NameText.text = Names[0];
        }
        else
        {
            Debug.Log("检测不是中文！");
            NameText.font = Fonts[1];
            NameText.text = Names[1];
        }
    }

    /// <summary>
    /// 中文校验
    /// </summary>
    /// <param name="c">中文字符</param>
    /// <returns>真假</returns>
    private bool isChinese(char c)
    {
        return c >= 0x4E00 && c <= 0x9FA5;
    }

    private bool checkString(string str)
    {
        char[] ch = str.ToCharArray();
        if (str != null)
        {
            for (int i = 0; i < ch.Length; i++)
            {
                if (isChinese(ch[i]))
                {
                    return true;
                }
            }
        }
        return false;
    }
}
