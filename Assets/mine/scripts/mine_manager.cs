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
    bool [,] arr = new bool[10, 10];        //��ź ���翩��
    int[,] tnt_count_arr = new int[10, 10]; //�ֺ� ��ź ���� üũ
    public int tntCount = 5;                 //��ź ����
    bool[] clicked_check = new bool[100]; //Ŭ���� �� üũ
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

    public void tnt_make()                          //������ ������ŭ ��ź�� ��ġ�ϴ� �Լ�
    {
        open = 100 - tntCount;
        safePlace.text = open.ToString();
        for (int i=0;i<10;i++)                       //�ʱ�ȭ
        {
            for(int j=0;j<10;j++)
            {
                arr[i, j] = false;
            }
        }
        for (int i = 0; i < tntCount; i++)          //��ź ��ġ
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

    public void tnt_count(int y, int x)                         //�ֺ� ��ź ���� üũ �Լ�
    {
        int imsiCount = 0;
        int imsix = 0;
        int imsiy = 0;

        if (arr[y,x])                                            //�ֺ��� �ƴ϶� �ڱⰡ ��ź�� ���
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
                    if (arr[imsiy, imsix])                            //��ź�� �ִ� ���
                    {
                        imsiCount++;
                    }
                }
            }
        }
        
        tnt_count_arr[y, x] = imsiCount;                        //�迭�� ���� ���� ����
        tiles[y * 10 + x].set_tnt_count(imsiCount);             //�� Ÿ�Ͽ� ������ ���� ���� ���� ������
    }

    public void tile_setting()                                  //�ֺ� ��ź ������ ��� Ÿ�Ͽ��� ���������� �Լ�
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
        if (arr[num / 10, num % 10])                                             //��ź Ŭ����
        {
            fail_Pop.SetActive(true);
            isRunning = false;
        }
        else if(tnt_count_arr[num / 10, num % 10] == 0)                       //�ֺ��� ��ź�� ���� ���
        {
            int imsix = 0;
            int imsiy = 0;
            for (int i=0;i<8;i++)
            {
                imsix = xarr[i] + num % 10;
                imsiy = yarr[i] + num / 10;
                if (imsix >= 0 && imsix <= 9 && imsiy >= 0 && imsiy <= 9)    //�ֺ� ĭ�� �ڵ�Ŭ��
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
