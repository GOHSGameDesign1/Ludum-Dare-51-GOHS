using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    public static int timer;
    public TMP_Text timerUI;
    // Start is called before the first frame update
    void Awake()
    {
        timer = 10;
        //StartCoroutine(Countdown());
    }

    // Update is called once per frame
    void Update()
    {
        timerUI.text = timer.ToString();
        if(timer <= 0)
        {
            Debug.Log("DEAD!!!!!");
        }
    }

    public static IEnumerator Countdown()
    {
        timer = 10;
        while (timer > 0)
        {
            //Debug.Log(timer);

            yield return new WaitForSeconds(1f);
            timer--;
        }
    }
}
