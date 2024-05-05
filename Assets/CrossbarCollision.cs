using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEditor;
using System;

public class CrossbarCollision : MonoBehaviour
{
    public static CrossbarCollision Instance;
   
    public Transform ballLoc;// 축구공위치
    public Text hitCount;
    public Text stageLevel;// 스테이지 레벨 텍스트
    public Text hitScore;// 포스트바 위치에 따른 점수 
    public int FinalScore = 0;
    public int trySuccessCount;//미션 성공횟수
    public AudioClip hitSound;
    public AudioClip clearSound;
    public AudioSource audioSource;
    public int lvCount=1;// 레벨 변수 
    
    public GameObject stageClearUI;// 스테이지 클리어 알림
    [SerializeField] Transform tr;// 골대 위치 
    [SerializeField] Transform post;// 크로스바 위치 

    private int speed = 3;

    private bool gameMissionFlag = false;
    int tryBkCount = 0;// 미션 성공수 백업
    int score = 0;
    // Start is called before the first frame update
    public Renderer rendererComponent;
    public int getScore()// 점수 반환
    { return score; }
    private void Awake()
    {
        Instance = this;
    }
    void Start()
    {
        hitCount.text = "Count : " + (trySuccessCount).ToString();
        tryBkCount = trySuccessCount;
      
    }

    // Update is called once per frame
    void Update()
    {
        
        if(trySuccessCount == 0&&gameMissionFlag ==true )
        {
            Debug.Log("전 위치:"+tr.transform.position.z);
            Vector3 move = new Vector3(0, 0, 2);
            TimerSetting.Instance.remainingTime = TimerSetting.Instance.firstTime;// 성공할 경우 시간 복구
            UniStageClear();
            tr.position += move;
           
            Debug.Log(tr.transform.position.z);
            trySuccessCount = tryBkCount;
            hitCount.text = "Count : " + (trySuccessCount).ToString();
            lvCount++;
            stageLevel.text = "Stage : "+ lvCount.ToString();
            audioSource.PlayOneShot(clearSound);
            gameMissionFlag = false;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        
        ContactPoint contactPoint = collision.contacts[0];
        Vector3 pos = contactPoint.point;
        score += ChallangeScore(pos);
        hitScore.text ="Score :"+ score.ToString();
        audioSource.PlayOneShot(hitSound);
        hitCount.text = "Count : " + (trySuccessCount - 1).ToString();
        trySuccessCount -= 1;

        gameMissionFlag = true;
      
    }

    int ChallangeScore(Vector3 pos)// 좌표에 따른 점수 구분 
    {
        if (Mathf.Abs(pos.x)<=2 &&Mathf.Abs(pos.y)>=0)
        {
            ColorChange(Color.green);
            return 100;
        }
        else if (Mathf.Abs(pos.x) >2&& Mathf.Abs(pos.x) <= 5)
        {
            ColorChange(Color.blue);
            return 250;
        }
        else if (Mathf.Abs(pos.x) >5 && Mathf.Abs(pos.x) <= 7)
        {
            ColorChange(Color.cyan);
            return 500;
        }
        else if (Mathf.Abs(pos.x) > 7 && Mathf.Abs(pos.x) <= 9)
        {
            ColorChange(Color.yellow);
            return 700;
        }
        else
        {
            ColorChange(Color.red);
            return 1000; 
        }

    }
    /*
    void ColorSetting(Vector3 pos)// 좌표에 따른 점수 구분 
    {
        if (Mathf.Abs(pos.x) <= 2 && Mathf.Abs(pos.y) >= 0)
        {
            
         
           
        }
        else if (Mathf.Abs(pos.x) > 2 && Mathf.Abs(pos.x) <= 5)
        {
           
            
        }
        else if (Mathf.Abs(pos.x) > 5 && Mathf.Abs(pos.x) <= 7)
        {
            rendererComponent.material.color = Color.cyan;
            
        }
        else if (Mathf.Abs(pos.x) > 5 && Mathf.Abs(pos.x) <= 8)
        {
            rendererComponent.material.color = Color.yellow;
           
        }
        else
        {
            rendererComponent.material.color = Color.red;
           
        }

    }
    */
    IEnumerator StageClear()
    {
        stageClearUI.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        stageClearUI.SetActive(false);

    }
    
     

    async UniTaskVoid UniStageClear()
    {
        stageClearUI.SetActive(true);
        await UniTask.Delay(500);
        stageClearUI.SetActive(false);
    }
    async UniTaskVoid ColorChange(Color color)
    {      
        rendererComponent.material.color = color;
        await UniTask.Delay(500);
        rendererComponent.material.color = Color.gray;
    }

}   
