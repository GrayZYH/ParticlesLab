using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using UnityEngine;

public class SvaeToLocalManager : MonoBehaviour
{
    public static SvaeToLocalManager Instance;

    private void Awake()
    {
        Instance = this;
    }

    //保存最终RGB照片到本地
    public void SaveLocalPhoto(Texture2D resultTex)
    {
        string uploadFileName = GetTimeStamp() + ".png";
        string localSavePath = Application.persistentDataPath + "/Photos";
        string _saveurl = localSavePath + "/" + uploadFileName;
        byte[] payload = resultTex.EncodeToPNG();
        if (!Directory.Exists(localSavePath))
        {
            Debug.Log("本地保存目录：" + localSavePath + "不存在，正在创建");
            Directory.CreateDirectory(localSavePath);
        }
        File.WriteAllBytes(_saveurl, payload);
        Debug.Log("本地照片保存目录：" + localSavePath);
        payload = null;
    }

    ///获取时间戳
    private static string GetTimeStamp()
    {
        DateTime data = DateTime.Now;
        string format = "yyyy-MM-dd HH-mm-ss";
        return data.ToString(format, DateTimeFormatInfo.InvariantInfo);
    }
}
