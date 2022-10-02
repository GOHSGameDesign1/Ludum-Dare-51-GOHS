using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.InputSystem;

public class WordManager : MonoBehaviour
{
    private List<Letter> currentWordList = new List<Letter>();
    private TMP_Text currentWord;
    private int letterIndex;
    private bool currentLetterCorrect;

    public List<string> words;
    public Canvas wordCanvas;

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
        if (KeyboardManager.currentKey == currentWord.text[letterIndex])
        {
            Debug.Log("Correct Letter");
            //currentWordList[letterIndex].FlashGreen();
            currentLetterCorrect = true;

            /* int vertexIndex = currentWord.textInfo.characterInfo[letterIndex].vertexIndex;
             currentWord.mesh.colors[vertexIndex] = Color.green;
             currentWord.mesh.colors[vertexIndex + 1] = Color.green;
             currentWord.mesh.colors[vertexIndex + 2] = Color.green;
             currentWord.mesh.colors[vertexIndex + 3] = Color.green;

             currentWord.ForceMeshUpdate();

             //Color32[] vertexColors = currentWord.textInfo.meshInfo[0].colors32;
             //vertexColors[vertexIndex + 0] = myColor32;
             //vertexColors[vertexIndex + 1] = myColor32;
             //vertexColors[vertexIndex + 2] = myColor32;
             //vertexColors[vertexIndex + 3] = myColor32;
             */

            currentWord.GetComponent<Letter>().FlashGreen(letterIndex);
        }
        else
        {
            Debug.Log("Incorrect");
            //currentWordList[letterIndex].FlashRed();
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
                    KeyboardManager.currentKey = '|'; // filler key
                    break;
                }

                yield return waitTime;
            }
        }

        DestroyCurrentWord();

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

        //for (int i = 0; i < word.Length; i++)
        //{
           // Letter currentLetter = Instantiate(letterPrefab, new Vector2((float)(word.Length * -1) / 2 + i, 0), Quaternion.identity, transform).GetComponent<Letter>();
            //currentLetter.ChangeText(word[i].ToString());
            //currentWordList.Add(currentLetter);
       // }

        currentWord = Instantiate(letterPrefab, wordCanvas.transform).GetComponent<TMP_Text>();
        currentWord.text = word;

        StopAllCoroutines();
        StartCoroutine(Timer.Countdown());
        //StartCoroutine(CheckLetters());



    }

    // destroys each letter, resets the letter index, and clears the current list
    void DestroyCurrentWord()
    {
        letterIndex = 0;
        foreach (Letter letter in currentWordList)
        {
            Destroy(letter.gameObject);

        }
        currentWordList.Clear();
    }
}
