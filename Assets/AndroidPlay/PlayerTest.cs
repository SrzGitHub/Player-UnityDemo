using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.UI;

public class PlayerTest : MonoBehaviour {

    public Renderer mRenderer;

    Texture2D videoTexture;

   

    void Start ()
    {


       
        mRenderer = GetComponent<Renderer>();

        if (Application.platform == RuntimePlatform.Android)
        {
            

            try
            {
                //初始化TextureID
                int textureId = AndroidJavaUtils.TextureID();
                //设置TextureID
                videoTexture = Texture2D.CreateExternalTexture(2880, 1600, TextureFormat.RGB24,
                                                               false, false, new IntPtr(textureId));
            }
            catch (Exception e)
            {
                Debug.LogError("LetinCodec.InitSurfaceTexture() failed: " + e.Message);
                return;
            }

            mRenderer.sharedMaterial.mainTexture = videoTexture;

            //注册广播
            AndroidJavaUtils.RegistrationBroadcast();
            //初始化播放器
            AndroidJavaUtils.initPlay();
            
         
        }	
	}


   


    // Update is called once per frame
    void Update ()
    {
        if (Application.platform == RuntimePlatform.Android)
        {
            if (AndroidJavaUtils.isNewFrameAvailable())
            {
                //更新Texture
                AndroidJavaUtils.UpdateTexture();
            }
        }
        
    }
}
