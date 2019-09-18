using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BarValueChange : MonoBehaviour
{
    [SerializeField] private hightlightStats getDogStats;
    private float MyValue;
    
    private RectTransform myBar;
    private float maxValue;
    void Start()
    {
        myBar = GetComponent<RectTransform>();
        maxValue = myBar.sizeDelta.x;
        
    }

    void Update()
    {
        if (getDogStats != null)
        {
            if (getDogStats.dogStats != null)
            {
                switch (gameObject.tag)
                {
                    case "Hygiene":
                        MyValue = getDogStats.dogStats.Hygiene;
                        break;
                    case "Nutrition":
                        MyValue = getDogStats.dogStats.Nutrition;
                        break;
                    case "Socialization":
                        MyValue = getDogStats.dogStats.Socialization;
                        break;
                }


                myBar.GetComponent<Image>().fillAmount = MyValue / 100;
            }
        }
    }
}
