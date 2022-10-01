using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WordManager : MonoBehaviour
{
    private List<TMP_Text> currentWordList = new List<TMP_Text>();

    public List<string> words;

    public GameObject letterPrefab;
    private WaitForFixedUpdate waitTime = new WaitForFixedUpdate();

    // Start is called before the first frame update
    void Start()
    {
        SetupNextWord(words[0]);
    }

    // Update is called once per frame
    void Update()
    {

    }

    // checks if the current keyboard press is the same as the word, if the current word is finished, load a new word
    /*void checkLetters()
    {
        if (currentWordQueue.Count == 0)
        {
            if (currentString != null)
            {
                Destroy(currentString.gameObject);
            }

            if (words.Count == 0)
            {
                Debug.Log("Out of words!");
                check = false;
                return;
            }

            SetupNextWord(words[0]);
        }

        if (KeyboardManager.currentKey.ToString() == currentWordList[letterCount].text)
        {
            //Debug.Log("Removing: " + currentWordQueue.Dequeue());
            currentWordList.Dequeue();
            KeyboardManager.currentKey = '|';
        }
    } */

    IEnumerator CheckLetters()
    {
        // runs through each letter to check if the key pressed at a certain frame was correct.
        for (int i = 0; i < currentWordList.Count; i++)
        {
            while (true)
            {
                if (KeyboardManager.currentKey.ToString().ToUpper() == currentWordList[i].text)
                {
                    //Debug.Log("Removing: " + currentWordQueue.Dequeue());
                    currentWordList[i].color = Color.black;
                    KeyboardManager.currentKey = '|'; // filler key
                    break;
                }

                yield return waitTime;
            }
        }

        // destroys each letter after it has been fully typed >>>>>> TODO: Make this a destroy function
        foreach (TMP_Text letter in currentWordList)
        {
            Destroy(letter.transform.parent.gameObject);
           
        }
        currentWordList.Clear();
        //Debug.Log("currentWordList count: " + currentWordList.Count);

        //  This means that you have finished the level >>>>>>> TODO: make this a next level function
        if (words.Count == 0)
        {
            Debug.Log("Out of words!");
            StopAllCoroutines();
            yield break;
        }

        SetupNextWord(words[0]);
    }

    // loads a new word
    void SetupNextWord(string word)
    {
        words.RemoveAt(0);

        for (int i = 0; i < word.Length; i++)
        {
                TMP_Text currentChar = Instantiate(letterPrefab, new Vector2((float)(word.Length * -1)/2 + i, 0), Quaternion.identity, transform).GetComponentInChildren<TMP_Text>();
                currentChar.text = word[i].ToString();
                currentWordList.Add(currentChar);
        }

        StopAllCoroutines();
        StartCoroutine(Timer.Countdown());
        StartCoroutine(CheckLetters());

    }
}
