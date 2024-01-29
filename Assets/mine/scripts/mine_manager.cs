using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class mine_manager : MonoBehaviour
{
    public Text Timer_text;
    public Text safePlace;
    public GameObject clear_Pop;
    public GameObject fail_Pop;
    int[] xarr = { -1, 0, 1, -1, 1, -1, 0, 1 };
    int[] yarr = { -1, -1, -1, 0, 0, 1, 1, 1 };
    bool [,] arr = new bool[10, 10];        //폭탄 존재여부
    int[,] tnt_count_arr = new int[10, 10]; //주변 폭탄 개수 체크
    public int tntCount = 5;                 //폭탄 개수
    bool[] clicked_check = new bool[100]; //클릭된 것 체크
    int open = 100;
    public image_changer [] tiles = new image_changer[100];

    public float maxTime;
    private float elapsedTime = 100f;
    private bool isRunning;

    void Start()
    {
        replay();
        elapsedTime = maxTime;
    }

    void Update()
    {
        if (isRunning)
        {
            elapsedTime -= Time.deltaTime;
            UpdateTimerText();
            if(elapsedTime <= 0)
            {
                isRunning = false;
                fail_Pop.SetActive(true);
            }
        }
    }

    public void UpdateTimerText()
    {
        Timer_text.text = Mathf.FloorToInt(elapsedTime).ToString();
    }

    public void replay()
    {
        for (int i = 0; i < 10; i++)
        {
            for (int j = 0; j < 10; j++)
            {
                tiles[i * 10 + j].close_Btn();
                clicked_check[i * 10 + j] = false;
            }
        }
        tnt_make();
        elapsedTime = maxTime;
        isRunning = false;
    }

    public void tnt_make()                          //정해진 개수만큼 폭탄을 설치하는 함수
    {
        open = 100 - tntCount;
        safePlace.text = open.ToString();
        for (int i=0;i<10;i++)                       //초기화
        {
            for(int j=0;j<10;j++)
            {
                arr[i, j] = false;
            }
        }
        for (int i = 0; i < tntCount; i++)          //폭탄 설치
        {
            int x, y;
            x = Random.Range(0, 10);
            y = Random.Range(0, 10);
            if (arr[y, x] == false)
            {
                arr[y, x] = true;
            }
            else
            {
                i--;
            }
        }
        tile_setting();
    }

    public void tnt_count(int y, int x)                         //주변 폭탄 개수 체크 함수
    {
        int imsiCount = 0;
        int imsix = 0;
        int imsiy = 0;

        if (arr[y,x])                                            //주변이 아니라 자기가 폭탄인 경우
        {
            imsiCount = 10;
        }
        else
        {
            for (int i = 0; i < 8; i++)
            {
                imsix = xarr[i] + x;
                imsiy = yarr[i] + y;
                if (imsix >= 0 && imsix <= 9 && imsiy >= 0 && imsiy <= 9)
                {
                    if (arr[imsiy, imsix])                            //폭탄이 있는 경우
                    {
                        imsiCount++;
                    }
                }
            }
        }
        
        tnt_count_arr[y, x] = imsiCount;                        //배열로 정보 묶어 저장
        tiles[y * 10 + x].set_tnt_count(imsiCount);             //각 타일에 정보를 띄우기 위한 정보 보내기
    }

    public void tile_setting()                                  //주변 폭탄 개수를 모든 타일에서 돌리기위한 함수
    {
        for(int i=0;i<10;i++)
        {
            for(int j=0;j<10;j++)
            {
                tnt_count(i, j);
            }
        }
    }

    public void click_tile(int num)
    {
        isRunning = true;
        open--;
        if(open == 0)
        {
            gameClear();
            isRunning = false;
        }
        safePlace.text = open.ToString();
        tiles[num].click_Btn();
        clicked_check[num] = true;
        if (arr[num / 10, num % 10])                                             //폭탄 클릭시
        {
            fail_Pop.SetActive(true);
            isRunning = false;
        }
        else if(tnt_count_arr[num / 10, num % 10] == 0)                       //주변에 폭탄이 없는 경우
        {
            int imsix = 0;
            int imsiy = 0;
            for (int i=0;i<8;i++)
            {
                imsix = xarr[i] + num % 10;
                imsiy = yarr[i] + num / 10;
                if (imsix >= 0 && imsix <= 9 && imsiy >= 0 && imsiy <= 9)    //주변 칸들 자동클릭
                {
                    if (clicked_check[imsiy * 10 + imsix] == false)
                    {
                        click_tile(imsiy * 10 + imsix);
                    }
                }
            }
        }
    }

    public void gameClear()
    {
        clear_Pop.SetActive(true);
    }
}
