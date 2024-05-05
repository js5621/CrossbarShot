using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class GameOverScreen : MonoBehaviour
{ 
    public static GameOverScreen Instance;
    public  Text FinalScoreText;
    private void Awake()
    {
        Instance = this;
    }
    public int finalScore=0;
    
    // Start is called before the first frame update
    void Start()
    {
        finalScore = CrossbarCollision.Instance.getScore();
        FinalScoreText.text = "score : "+finalScore.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
