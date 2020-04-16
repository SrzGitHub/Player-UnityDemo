using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AndroidJavaUtils {


    private static AndroidJavaObject mAndroidPlay;
    private static AndroidJavaObject mAndroidLog;
    private static AndroidJavaObject mContext;
    static AndroidJavaUtils()
    {

        GetAndroidPlay();
        GetAndroidLog();
    }

   

   
    //获取Android上下文
    public static AndroidJavaObject GetAndroidContext()
    {
        mContext = new AndroidJavaClass("com.unity3d.player.UnityPlayer").GetStatic<AndroidJavaObject>("currentActivity"); //获得Context
        return mContext;
    }
    //获取AndroidPlayer管理类
    private  static AndroidJavaObject GetAndroidPlay()
    {
        mAndroidPlay = new AndroidJavaObject("com.letinvr.playsdk.UnityForAndroidPlayManager");
        return mAndroidPlay;
    }
    //获取Android Log类 可修改plasdk-release.aar 内 assets包里的  logcontent.txt 关闭log  推荐使用
    private static AndroidJavaObject GetAndroidLog()
    {
        mAndroidLog = new AndroidJavaObject("com.letinvr.playsdk.util.UnityLogUtil");
        return mAndroidLog;
    }
    /// <summary>
    /// AndroidPlay
    /// </summary>
    /// <returns></returns>
    //获取TextureID
    public static int TextureID()
    {
        return mAndroidPlay.Call<int>("InitSurfaceTexture");
    }

    //是否有可用帧
    public static bool isNewFrameAvailable()
    {
        return mAndroidPlay.CallStatic<bool>("isNewFrameAvailable");
    }

    //更新texture
    public static void UpdateTexture()
    {
        mAndroidPlay.Call("UpdateTexture");
    }

    //注册网络广播
    public static void RegistrationBroadcast()
    {
        mAndroidPlay.Call("initBroadcast", GetAndroidContext());
    }
    //解除广播注册
    public static void unRegisterReceiver()
    {
        mAndroidPlay.Call("unRegisterReceiver", GetAndroidContext());
    }
    //初始化播放器
    public static void initPlay()
    {
        mAndroidPlay.Call("initPlay");
    }
    //开始播放
    public static void openPlay(string url)
    {
        mAndroidPlay.Call("openPlay", url);
    }
   
    //暂停
    public static void playPause()
    {
        mAndroidPlay.Call("playPause");
    }
    //继续播放
    public static void playResume()
    {
        mAndroidPlay.Call("playResume");
    }
    //销毁
    public static void playDestroy()
    {
        mAndroidPlay.Call("playDestroy");
    }
    //设置进度
    public static void playSeeto(int progress)
    {
        mAndroidPlay.Call("playSeeto", progress);
    }
    //设置倍速
    public static void playSpeed(float flt)
    {
        mAndroidPlay.Call("playSpeed", flt);
    }
    //音量+
    public static void playVolumeAdd()
    {
        mAndroidPlay.Call("playVolumeAdd");
    }
    //音量-
    public static void playVolumeReduce()
    {
        mAndroidPlay.Call("playVolumeReduce");
    }
    //获取设备 最大音量
    public static int playVertica()
    {
       return mAndroidPlay.CallStatic<int>("playVertica");
    }
    //获取当前倍速
    public static float GetPlaySpeed()
    {
       return mAndroidPlay.Call<float>("getPlaySpeed");
    }
    //获取播放状态
    public static bool isPlaying()
    {
        return mAndroidPlay.Call<bool>("isPlaying");
    }
    public static string GenerateTime(long time)
    {
        return mAndroidPlay.CallStatic<string>("generateTime", time);
    }

    /// <summary>
    /// Android Log
    /// </summary>
    /// <param name="msg">打印消息</param>
    

    public static void Debug(string msg)
    {
        mAndroidLog.CallStatic("D", "Srz ---> "+ msg + "\r\n("+GetCurSourceFileName()+":"+GetLineNum()+")");
    }

    public static void Info(string msg)
    {
        mAndroidLog.CallStatic("I", "Srz ---> " + msg + "\r\n(" + GetCurSourceFileName() + ":" + GetLineNum() + ")");
    }

    public static void Error(string msg)
    {
        mAndroidLog.CallStatic("E", "Srz ---> " + msg + "\r\n(" + GetCurSourceFileName() + ":" + GetLineNum() + ")");
    }

    public static void Warn(string msg)
    {
        mAndroidLog.CallStatic("W", "Srz ---> "+ msg + "\r\n(" + GetCurSourceFileName() + ":" + GetLineNum() + ")");
    }

    public static void Verbose(string msg)
    {
        mAndroidLog.CallStatic("V", "Srz ---> " + msg + "\r\n(" + GetCurSourceFileName() + ":" + GetLineNum() + ")");
    }













    //////////////////////////////////////////
    ///
    ////////////////////////////////////////////

    /// <summary>
    /// 取得当前源码的哪一行
    /// </summary>
    /// <returns></returns>
    public static int GetLineNum()
    {
        System.Diagnostics.StackTrace st = new System.Diagnostics.StackTrace(1, true);
        return st.GetFrame(0).GetFileLineNumber();
    }

    /// <summary>
    /// 取当前源码的源文件名
    /// </summary>
    /// <returns></returns>
    public static string GetCurSourceFileName()
    {
        System.Diagnostics.StackTrace st = new System.Diagnostics.StackTrace(1, true);

        return st.GetFrame(0).GetFileName();

    }

}
