using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.InputSystem;

public class WordManager : MonoBehaviour
{
    private List<TMP_Text> currentWordList = new List<TMP_Text>();
    private int letterIndex;
    private bool currentLetterCorrect;

    public List<string> words;

    public GameObject letterPrefab;
    private WaitForFixedUpdate waitTime = new WaitForFixedUpdate();

    // Start is called before the first frame update
    void Awake()
    {
        SetupNextWord(words[0]);
        letterIndex = 0;
        currentLetterCorrect = false;
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnEnable()
    {
        KeyboardManager.keyPressed += CheckLetterCorrect;
    }

    private void OnDisable()
    {
        KeyboardManager.keyPressed -= CheckLetterCorrect;
    }

    // checks if the keypressed was the correct letter at the correct index
    void CheckLetterCorrect(char a)
    {
        if (KeyboardManager.currentKey.ToString().ToUpper() == currentWordList[letterIndex].text)
        {
            Debug.Log("Correct Letter");
            currentWordList[letterIndex].GetComponentInParent<SpriteRenderer>().color = new Color(1, 0, 0, 0f);
            currentLetterCorrect = true;
        }
        else
        {
            Debug.Log("Incorrect");
            currentWordList[letterIndex].GetComponentInParent<SpriteRenderer>().color = new Color(1, 0, 0, 0.19f);
        }
    }

    IEnumerator CheckLetters()
    {
        // runs through each letter to check if the key pressed at a certain frame was correct.
        for (int i = 0; i < currentWordList.Count; i++)
        {
            while (true)
            {
                if (currentLetterCorrect == true)
                {
                    //Debug.Log("Removing: " + currentWordQueue.Dequeue());
                    currentLetterCorrect = false;
                    letterIndex++;
                    currentWordList[i].color = Color.green;
                    KeyboardManager.currentKey = '|'; // filler key
                    break;
                }

                yield return waitTime;
            }
        }

        letterIndex = 0;
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
