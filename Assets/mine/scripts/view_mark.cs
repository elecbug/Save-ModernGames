using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class view_mark : MonoBehaviour, IPointerClickHandler
{
    private Image blindImage;
    int color_num;

    private void Start()
    {
        blindImage = gameObject.GetComponent<Image>();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Right) // 마우스 우클릭
        {
            switch (color_num)
            {
                case 0:
                    blindImage.color = Color.red;
                    color_num++;
                    break;
                case 1:
                    blindImage.color = Color.yellow;
                    color_num++;
                    break;
                case 2:
                    blindImage.color = Color.white;
                    num_Set();
                    break;
            }
        }
    }

    public void num_Set()
    {
        color_num = 0;
    }
}
