using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class GameMaster : MonoBehaviour
{
    public GameObject startpanel, gameoverpanel, winpanel, ingamepanel;
    public TextMeshProUGUI wingamescore;
    public TextMeshProUGUI losegamescore;
    public TextMeshProUGUI ballcount;
    public int currentscore;
    public int successshot;
    public int shotcount;
    public GameObject[] balls;
    public bool start;
    // Start is called before the first frame update
    void Start()
    {
        start = false;
        startpanel.SetActive(true);
        gameoverpanel.SetActive(false);
        winpanel.SetActive(false);
        ingamepanel.SetActive(false);
    }
    // Update is called once per frame
    void Update()
    {
        ballcount.text = "X " + (5 - shotcount); 
    }
    private void LateUpdate()
    {
        ChechkPlayerShoot();
    }
    void ChechkPlayerShoot()
    {
        if (successshot!=0)
        {
            balls[successshot - 1].gameObject.GetComponent<Image>().color = Color.green;
        }
        if (shotcount==5 && successshot<3)
        {
            GameOver();
        }
        if (successshot==3)
        {
            Win();
        }
    }
    void GameOver()
    {
        start = false;
        startpanel.SetActive(false);
        gameoverpanel.SetActive(true);
        winpanel.SetActive(false);
        ingamepanel.SetActive(false);
        losegamescore.text = "Score: " + currentscore;
    }
    void Win()
    {
        start = false;
        startpanel.SetActive(false);
        gameoverpanel.SetActive(false);
        winpanel.SetActive(true);
        ingamepanel.SetActive(false);
        wingamescore.text = "Score: " + currentscore;
    }
    public void StartButton()
    {
        start = true;
        startpanel.SetActive(false);
        ingamepanel.SetActive(true);
    }
    public void Restart()
    {
        successshot = 0;
        currentscore = 0;
        shotcount = 0;
        GameObject.FindGameObjectWithTag("Player").GetComponent<BallControl>().combo = 0;
        GameObject.FindGameObjectWithTag("Player").GetComponent<BallControl>().touchball = false;
        start = true;
        startpanel.SetActive(false);
        ingamepanel.SetActive(true);
        gameoverpanel.SetActive(false);
        winpanel.SetActive(false);
        foreach (GameObject item in balls)
        {
            item.gameObject.GetComponent<Image>().color = Color.white;
        }
    }
}