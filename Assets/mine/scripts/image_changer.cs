using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class image_changer : MonoBehaviour
{
    public Text count;
    public GameObject blind;
    private int tnt_count;
    private Image blindImage;

    public void close_Btn()
    {
        blind.SetActive(true);
        blindImage = blind.GetComponent<Image>();
        blindImage.color = Color.white;
    }

    public void click_Btn()
    {
        blind.SetActive(false);
    }

    public void set_tnt_count(int num)
    {
        tnt_count = num;
        change_Text();
    }

    public void change_Text()
    {
        if (tnt_count == 0)
        {
            count.text = "";
        } else if (tnt_count == 10)
        {
            count.text = "*";
        }
        else
        {
            count.text = tnt_count.ToString();
        }
    }
}
