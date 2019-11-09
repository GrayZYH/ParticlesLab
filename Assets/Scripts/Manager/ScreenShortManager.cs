using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScreenShortManager : MonoBehaviour
{
    public static ScreenShortManager Instance;
    public event Action<Texture2D> ScreenShortPhotoEvent;
    public Camera ScreenShortCamera;
    public RenderTexture ScreenShortRT;
    public Text NameText;
    public Font[] Fonts;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        //隐藏屏幕光标
        Cursor.visible = false;
        SocketServer.Instance.OnReceiveRFIDNameEvent += OnReceiveRFIDNameEvent;
    }

    private void OnDestroy()
    {
        SocketServer.Instance.OnReceiveRFIDNameEvent -= OnReceiveRFIDNameEvent;
    }

    /// <summary>
    /// RFID接收客户姓名回调
    /// </summary>
    /// <param name="fullName">客户姓名</param>
    private void OnReceiveRFIDNameEvent(string fullName)
    {
        if (checkString(fullName)==true)
        {
            Debug.Log("检测到客户姓名为中文！");
            NameText.font = Fonts[0];
            NameText.fontSize = 36;
        }
        else
        {
            Debug.Log("检测到客户姓名不为中文！");
            NameText.font = Fonts[1];
            if (fullName.Length >17 && fullName.Length <=24)
            {
                //Anne Phillips Jacqueline
                NameText.fontSize = 24;
                Debug.Log("名字比较长的长度："+fullName.Length);
            }
            else if (fullName.Length>24)
            {
                NameText.fontSize = 20;
                Debug.Log("名字无限长的长度：" + fullName.Length);
            }
            else 
            {
                NameText.fontSize = 36;
                Debug.Log("名字比较短的长度：" + fullName.Length);
            }
        }

        NameText.text = fullName;
        Debug.Log("Name_Text:" + NameText.text);
        ScreenShortTexture(ScreenShortCamera,ScreenShortRT);
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

    /// <summary>
    /// 截图
    /// </summary>
    /// <param name="camera">相机</param>
    /// <param name="renderTexture">渲染画面</param>
    public void ScreenShortTexture(Camera camera,RenderTexture renderTexture)
    {
        renderTexture.Release();
        camera.Render();
        RenderTexture.active = renderTexture;
        Texture2D tempTex = new Texture2D(renderTexture.width, renderTexture.height);
        tempTex.ReadPixels(new Rect(0f, 0f, renderTexture.width, renderTexture.height), 0, 0);
        tempTex.Apply();

        if (ScreenShortPhotoEvent !=null)
        {
            ScreenShortPhotoEvent.Invoke(tempTex);
        }
    }
}
