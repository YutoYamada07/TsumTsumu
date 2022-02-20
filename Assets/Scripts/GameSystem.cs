using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameSystem : MonoBehaviour
{
    [SerializeField] BallGenerator ballGenerator = default;
    bool isDragging;
    [SerializeField] List<Ball> removeBalls = new List<Ball>();
    Ball currentDraggingBall;
    int score;
    [SerializeField] Text scoreText = default;
    [SerializeField] Text timerText = default;
    int timeCount;

    bool gameOver;

    [SerializeField] GameObject resultPanel = default;

    // Start is called before the first frame update
    void Start()
    {
        SoundManager.instance.PlayBGM(SoundManager.BGM.Main);
        timeCount = ParamsSO.Entity.TimeCount;
        score = 0;
        AddScore(0);
        StartCoroutine(ballGenerator.Spawns(ParamsSO.Entity.initBallCount));
        StartCoroutine(CountDown());
        
    }
    //カウントダウン
    IEnumerator CountDown()
    {
        while (timeCount > 0)
        {
            yield return new WaitForSeconds(1);
            timeCount--;
            timerText.text = timeCount.ToString();
            timerText.text = timeCount.ToString();
        }
        gameOver = true;
        resultPanel.SetActive(true);
        
    }
    public void OnRetryButton()
    {
        SceneManager.LoadScene("Main");
    
    }

    void UpdateTimerText()
    {
        timerText.text = timeCount.ToString();
    
    }
    void AddScore(int point)
    {
        score += point;
        scoreText.text = score.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        if (gameOver)
        {
            return;//ここで終わらせる
        }
        if (Input.GetMouseButtonDown(0))
        {
            OnDragBegin();
        }
        else if (Input.GetMouseButtonUp(0))
        {
            OnDragEnd();
        }
        else if (isDragging)
        {
            OnDragging();
        }
        
    }

    void OnDragBegin()
    {
        Debug.Log("ドラッグ開始");
        //マウスにyるオブジェクトの判定
        //Ray
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(mousePosition,Vector2.zero);
        if (hit&& hit.collider.GetComponent<Ball>())
        {
            Debug.Log("オブジェクトにヒットしたよ");
            Ball ball = hit.collider.GetComponent<Ball>();
            AddRemoveBall(ball);
            isDragging = true;
        }
    }
    void OnDragging()
    {
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(mousePosition, Vector2.zero);
        if (hit && hit.collider.GetComponent<Ball>())
        {
            //同じ種類＆距離が近かったらListに追加
            Ball ball = hit.collider.GetComponent<Ball>();
            if (ball.id == currentDraggingBall.id)
            {
                float distance = Vector2.Distance(ball.transform.position,currentDraggingBall.transform.position);
                if (distance < ParamsSO.Entity.ballDistance)
                {
                    AddRemoveBall(ball);
                }
            
            }
            
            
        }
    }

    void OnDragEnd()
    {
        int removeCount = removeBalls.Count;
        if (removeCount >= 3)
        {
            for (int i = 0; i < removeCount; i++)
            {
                Destroy(removeBalls[i].gameObject);
                
            }
            StartCoroutine(ballGenerator.Spawns(removeCount));
            
            AddScore(removeCount * ParamsSO.Entity.ScorePoint);
            SoundManager.instance.PlaySE(SoundManager.SE.Destroy);


        }
        
        
        
        for (int i = 0; i < removeCount; i++)
        {
            //Vector2 kero = removeBalls[i].transform.localScale;
            //kero.x = 1;
            //kero.y = 1;
            removeBalls[i].GetComponent<SpriteRenderer>().color = Color.white;
            removeBalls[i].transform.localScale = Vector3.one ;
            

        }
        removeBalls.Clear();
        isDragging = false;
    }

    void AddRemoveBall(Ball ball)
    {
        
        currentDraggingBall = ball;
        if (removeBalls.Contains(ball) == false)
        {
            removeBalls.Add(ball);
            ball.transform.localScale *= 1.4f;
            ball.GetComponent<SpriteRenderer>().color = Color.yellow;

            
        }
        
    }
}
