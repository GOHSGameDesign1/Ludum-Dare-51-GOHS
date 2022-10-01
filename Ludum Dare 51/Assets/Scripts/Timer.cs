using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    public static float timer;
    public TMP_Text timerUI;
    public Image timerRing;
    public float lerpSpeed;
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
        timerRing.fillAmount = Mathf.Lerp(timerRing.fillAmount, timer/10, lerpSpeed * Time.deltaTime);
        ColorChanger();

        if(timer <= 0)
        {
            Debug.Log("DEAD!!!!!");
        }
    }

    void ColorChanger()
    {
        if(timer < 12)
        {
            timerRing.color = Color.Lerp(Color.red, Color.white, timer/10);
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
