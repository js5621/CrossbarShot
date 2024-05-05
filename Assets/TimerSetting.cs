using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TimerSetting : MonoBehaviour
{
    public Text timerText; 
    public float remainingTime;
    public AudioClip hitSound;
    public AudioSource audioSource;
    bool setCountDown= false;
    public float firstTime;
    public static TimerSetting Instance;
    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        firstTime = remainingTime;
    }
    // Update is called once per frame
    void Update()
    {
        //OptionDelay();
        if (remainingTime < 0)
        {
            Debug.Log("GameOver!!!");
            
            SceneManager.LoadScene(2);
        }
              
         int minutes = Mathf.FloorToInt(remainingTime / 60);
        int seconds = Mathf.FloorToInt(remainingTime % 60);
        if (seconds <= 10 && minutes < 1)// 시간 임박효과 표시 
        {
            if (!setCountDown) 
            {
                audioSource.Play();
                audioSource.PlayOneShot(hitSound, 1);
                setCountDown = true;    
            }
           
            timerText.color = Color.red;
            
        }
        
        else
        {
            setCountDown= false;
            audioSource.Stop();
            timerText.color = Color.black;


        }
       
        timerText.text = "TIME : " + string.Format("{0:00}:{1:00}",minutes,seconds);
        remainingTime -= Time.deltaTime;
    }

    async UniTaskVoid OptionDelay()
    {
       
        await UniTask.Delay(1000);
        
    }
}
