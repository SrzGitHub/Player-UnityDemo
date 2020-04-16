using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayMessage : MonoBehaviour
{

     
    public Slider mSlider;
    Slider SpeedSlider;
    public static string videoMsg;
    public static string speed;
    string CurrentProgress;
    string TotalProgress;
    string DoubleSpeed;
    string MaximumVolume;
    string CurrentVolume;
    string type;
    int mMax;
  
    Text androidMsg;
    Text mText;
    Button playStart;
    Button playPause;
    Button playStop;
    Button playSpeedAdd;
    Button playSpeedReduxet;
    Button playAudioAdd;
    Button playAudioReduxe;
    Button playTop;
    Button playUnder;
    bool isInfoBuffer = false;
    
    enum Type
    {
        HTTP,
        HTTPS,
        RTSP,
        RTMP
    }

   

    // Use this for initialization
    void Start()
    {

        initButtonClick();
                                                                      

    }
    public void initButtonClick()
    {
        mText = GameObject.Find("playMsg").GetComponent<Text>();//调试使用  
        androidMsg = GameObject.Find("androidMsg").GetComponent<Text>();//调试使用  
        SpeedSlider = GameObject.Find("SpeedSlider").GetComponent<Slider>();//调试使用  

        /////////////////////初始化按钮////////////////////////////////
        playStart = GameObject.Find("playStart").GetComponent<Button>();
        playPause = GameObject.Find("playPause").GetComponent<Button>();
        playStop = GameObject.Find("playStop").GetComponent<Button>();
        playSpeedAdd = GameObject.Find("speedAdd").GetComponent<Button>();
        playSpeedReduxet = GameObject.Find("speedReduxe").GetComponent<Button>();
        playAudioAdd = GameObject.Find("audioAdd").GetComponent<Button>();
        playAudioReduxe = GameObject.Find("audioReduxe").GetComponent<Button>();
        playTop = GameObject.Find("playTop").GetComponent<Button>();
        playUnder = GameObject.Find("playUnder").GetComponent<Button>();
        ////////////////////////////设置按钮点击事件/////////////////////////////////////
        playStart.onClick.AddListener(playStarts);//开始播放
        playPause.onClick.AddListener(palyPauses);//暂停
        playStop.onClick.AddListener(playStops);//停止
        playSpeedAdd.onClick.AddListener(playSpeedAdds);//倍速+
        playSpeedReduxet.onClick.AddListener(playSpeedReduxets);//倍速-
        playAudioAdd.onClick.AddListener(playAudioAdds);//音量+
        playAudioReduxe.onClick.AddListener(playAudioReduxes);//音量-
        playTop.onClick.AddListener(playTops);//上一集+
        playUnder.onClick.AddListener(playUnders);//下一集-
        AndroidJavaUtils.Error("初始化完成");

        mSlider.onValueChanged.AddListener(delegate (float m)
        {
            float value = mSlider.value;


            if (mMax == 0||type.Equals("RTSP")|| type.Equals("RTMP"))
            {
                return;
            }

            if (value > mMax)
            {

                /**
                 * 
                 * 
                 * */
                isInfoBuffer = true;
                int seek = (int)value * 10;
                AndroidJavaUtils.Error("前进");
                AndroidJavaUtils.playSeeto(seek);
              
            }
            else if (value<mMax)
            {
                isInfoBuffer = true;
                int seek = (int) value * 10;
                AndroidJavaUtils.Error("后腿");
                AndroidJavaUtils.playSeeto(seek);
              
            }

        });


        SpeedSlider.onValueChanged.AddListener(delegate (float m)
        {
            float value = SpeedSlider.value;


            if (AndroidJavaUtils.isPlaying())
            {

                AndroidJavaUtils.playSpeed(value);
            }



        });


    }

    
   
     public void playStarts()
    {
        if (!AndroidJavaUtils.isPlaying())
        {
         

            AndroidJavaUtils.openPlay(PlayerUrlAPI.HTTPS_PLAYE);
           
            AndroidJavaUtils.Error("开始播放");
            type = "HTTPS";
        }
    
    }
    public void palyPauses()
    {
        if (AndroidJavaUtils.isPlaying())
        {   //暂停
            AndroidJavaUtils.playPause();
         
            AndroidJavaUtils.Error("暂停");
        }
       else
       {   //继续播放
            AndroidJavaUtils.playResume();
    
            AndroidJavaUtils.Error("继续");
        }
    }
    public void playStops()
    {
        if (AndroidJavaUtils.isPlaying())
        {
            AndroidJavaUtils.playDestroy();
            AndroidJavaUtils.Error("停止");
        }
    }
    public void playSpeedAdds()
    {
        AndroidJavaUtils.playDestroy();
        AndroidJavaUtils.initPlay();
        AndroidJavaUtils.openPlay(PlayerUrlAPI.PLAY_AILS);
        type = "HTTP";

    }
    public void playSpeedReduxets()
    {
        AndroidJavaUtils.playDestroy();
        AndroidJavaUtils.initPlay();
        AndroidJavaUtils.openPlay(PlayerUrlAPI.HTTPS_PLAYE);
        type = "HTTPS";

    }
    public void playTops()
    {

        AndroidJavaUtils.playDestroy();
        AndroidJavaUtils.initPlay();
        AndroidJavaUtils.openPlay(PlayerUrlAPI.RTSP_PLAYE);
        AndroidJavaUtils.Error("RTSP");
        type = "RTSP";

    }
    public void playUnders()
    {

        AndroidJavaUtils.playDestroy();
        AndroidJavaUtils.initPlay();
        AndroidJavaUtils.openPlay(PlayerUrlAPI.RTMP_PLAYE);
        AndroidJavaUtils.Error("RTMP");
        type = "RTMP";


    }
    public void playAudioAdds()
    {
        if (AndroidJavaUtils.isPlaying())
        {
            AndroidJavaUtils.playVolumeAdd();
            AndroidJavaUtils.Error("音量+++");
        }
      
    }
    public void playAudioReduxes()
    {
        if (AndroidJavaUtils.isPlaying())
        {
            AndroidJavaUtils.playVolumeReduce();
            AndroidJavaUtils.Error("音量----");
        }
        
    }
   

    private void OnApplicationPause(bool pause)
    {
     

        if (pause && AndroidJavaUtils.isPlaying())
        {
            AndroidJavaUtils.Error("OnApplicationPause  1 : " + pause);
            AndroidJavaUtils.playPause();
          

        }
        else
        {
            AndroidJavaUtils.Error("OnApplicationPause  2 : " + pause);
            AndroidJavaUtils.playResume();
   
        }
    }



    public void PlayeMessageCallback(string msg)
    {

        //"00:41|06:01|1.0|15|3"

       

        if (msg.Contains("Play"))
        {
            mText.text = "播放完成";
          
        }
        else if (msg.Contains("videoMessage"))
        {
            if (AndroidJavaUtils.isPlaying())
                videoMsg = msg.Replace("videoMessage|", "");
            androidMsg.text = videoMsg;
            //AndroidJavaUtils.Debug(videoMsg);
        }
        else if (msg.Contains("CurrentBufferPercentage|"))
        {
            //设置进度条
          
             mMax = int.Parse(msg.Replace("CurrentBufferPercentage|", ""));
            if (!isInfoBuffer)
            {
                mSlider.value = mMax;
            }
             

        }
        else if (msg.Contains("invalid progressive playback") || msg.Contains("unknown"))
        {
            mText.text = msg;
   
        }
        else if (msg.Contains("MEDIA_INFO_VIDEO_RENDERING_START"))
        {
            mText.text = "开始播放";
            isInfoBuffer = false;
   
        }
        else if (msg.Contains("MEDIA_INFO_BUFFERING_START"))
        {
            mText.text = "媒体缓冲中";
            AndroidJavaUtils.Error("播放状态---"+AndroidJavaUtils.isPlaying());
      
        }
        else if (msg.Contains("MEDIA_INFO_BUFFERING_END"))
        {
            mText.text = "媒体缓冲结束";
            AndroidJavaUtils.Error("播放状态---" + AndroidJavaUtils.isPlaying());
            isInfoBuffer = false;
        }
        else if (msg.Contains("MEDIA_INFO_BAD_INTERLEAVING"))
        {
            mText.text = "媒体文件损坏";
    
        }
        else if (msg.Contains("READY_TO_PLAY"))
        {
            mText.text = "准备播放";
        }
        else if (msg.Contains("NetworkIsConnected-2G"))
        {
            mText.text = "当前为2G网络，请切换WIFI环境";
            AndroidJavaUtils.playPause();
         
        }
        else if (msg.Contains("NetworkIsConnected-4G")|| msg.Contains("NetworkIsConnected-WIFI"))
        {
            if (!AndroidJavaUtils.isPlaying())
            {
                AndroidJavaUtils.playResume();
      
            }

        }else if (msg.Contains("NetworkIsDisconnected")){

            if (AndroidJavaUtils.isPlaying())
            {
                AndroidJavaUtils.playPause();
            }
        }
       
        else
        {
            string[] ms = msg.Split('|');


             CurrentProgress  = AndroidJavaUtils.GenerateTime(long.Parse(ms[0]));
             TotalProgress    = AndroidJavaUtils.GenerateTime(long.Parse(ms[1]));
             DoubleSpeed      = ms[2];
             MaximumVolume    = ms[3];
             CurrentVolume    = ms[4];



            if (AndroidJavaUtils.isPlaying())
            {
                SpeedSlider.value = float.Parse(DoubleSpeed);
                mText.text = "进度：" + CurrentProgress + "/" + TotalProgress + " 倍速：" + DoubleSpeed + " 最大音量：" + MaximumVolume + " 当前音量：" + CurrentVolume;
            }
              

        }
    }
    private void Update()
    {
     
    }
}
