using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.InputSystem;

public class WordManager : MonoBehaviour
{
    private List<Letter> currentWordList = new List<Letter>();
    private TMP_Text currentWord;
    public TMP_Text wordCountUI;
    private string currentLetters;
    public static int letterIndex;
    private bool currentLetterCorrect;

    [SerializeField]
    private List<string> currentLevelWords;
    private List<List<string>> levels;
    public List<string> level1;
    public List<string> level2;
    public List<string> level3;
    public List<string> level4;
    public List<string> level5;
    private int levelCounter;
    private bool levelComplete;

    public Canvas wordCanvas;

    public GameObject letterPrefab;
    public GameObject levelUpPrefab;
    private WaitForFixedUpdate waitTime = new WaitForFixedUpdate();

    bool keyboardSetUp; // This is a bandaid fix ~~~~~~~~~~~~~~~~~~~~~~ TODO: Make this an event from gameManager to start the game instead of a bool;

    // Start is called before the first frame update
    void Awake()
    {
        currentLevelWords = new List<string>();
        levels = new List<List<string>>();
        levels.Add(level1);
        levels.Add(level2);
        levels.Add(level3);
        levels.Add(level4);
        levels.Add(level5);
        levelComplete = false;

        levelCounter = 0;
        //StartCoroutine(NextLevel());

        letterIndex = 0;
        currentLetterCorrect = false;

        keyboardSetUp = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.gameStart && keyboardSetUp == false)
        {
            KeyboardManager.keyPressed += CheckLetterCorrect;
            keyboardSetUp = true;
        }
    }

    private void OnEnable()
    {
        
    }

    private void OnDisable()
    {
        KeyboardManager.keyPressed -= CheckLetterCorrect;
    }

    // checks if the keypressed was the correct letter at the correct index
    void CheckLetterCorrect(char a)
    {
        if (!GameManager.gameStart)
        {
            return;
        }
        if (levelComplete)
        {
            return;
        }

        if (KeyboardManager.currentKey.ToString().ToUpper() == currentWord.text[letterIndex].ToString())
        {
            Debug.Log("Correct Letter");
            //currentWordList[letterIndex].FlashGreen();
            currentLetterCorrect = true;
            currentWord.GetComponent<Letter>().red = false;

            //currentWord.GetComponent<Letter>().Green();

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

            //StartCoroutine(currentWord.GetComponent<Letter>().FlashGreen(letterIndex));
        }
        else
        {
            Debug.Log("Incorrect");
            currentWord.GetComponent<Letter>().red = true;
            //currentWordList[letterIndex].FlashRed();
        }
    }

    IEnumerator CheckLetters()
    {
        // runs through each letter to check if the key pressed at a certain frame was correct.
        for (int i = 0; i < currentWord.text.Length; i++)
        {
            while (true)
            {
                if (currentWord.textInfo.characterInfo[letterIndex].character == ' ')
                {
                    Debug.Log("SKipping space");
                    currentLetterCorrect = true;
                    currentWord.GetComponent<Letter>().red = false;

                }

                if (currentLetterCorrect == true)
                {
                    //Debug.Log("Removing: " + currentWord.text[i]);

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
        if (currentLevelWords.Count == 0)
        {
            Debug.Log("Out of words!");
            levelComplete = true;
            StopAllCoroutines();
            StartCoroutine(NextLevel());
            yield break;
        }

        SetupNextWord(currentLevelWords[0]);
    }

    // loads a new word
    void SetupNextWord(string word)
    {

        currentLevelWords.RemoveAt(0);
        Debug.Log("words left: " + currentLevelWords.Count);
        wordCountUI.text = "Words left: " + currentLevelWords.Count.ToString();

        //for (int i = 0; i < word.Length; i++)
        //{
           // Letter currentLetter = Instantiate(letterPrefab, new Vector2((float)(word.Length * -1) / 2 + i, 0), Quaternion.identity, transform).GetComponent<Letter>();
            //currentLetter.ChangeText(word[i].ToString());
            //currentWordList.Add(currentLetter);
       // }

        currentWord = Instantiate(letterPrefab, wordCanvas.transform).GetComponent<TMP_Text>();
        //currentWord.GetComponent<Letter>().ChangeText(word);
        currentWord.text = word;
        currentWord.ForceMeshUpdate();
        currentLetters = currentWord.text.Replace(" ", string.Empty);
        currentLetters.Replace(" ", string.Empty);
        currentWord.GetComponent<Letter>().ogPos = currentWord.mesh.vertices;
        Debug.Log(currentLetters);
        //Debug.Log(currentWord.mesh.vertices);

        StopAllCoroutines();
        StartCoroutine(Timer.Countdown());
        StartCoroutine(CheckLetters());



    }

    // destroys each letter, resets the letter index, and clears the current list
    void DestroyCurrentWord()
    {
        letterIndex = 0;

        currentWord.GetComponent<Letter>().Die();

    }

    public IEnumerator NextLevel()
    {
        levelComplete = true;
        Debug.Log("New Level");
        //currentLevelWords = words;
        currentLevelWords.Clear();


        if(levelCounter != 0)
        {
            Letter currentLevelUp = Instantiate(levelUpPrefab, wordCanvas.transform).GetComponent<Letter>();
            yield return new WaitForSeconds(3f);
            currentLevelUp.Die();
        }

        if(levelCounter == 5)
        {
            Debug.Log("Out of Levels");
            GameManager.wonGame = true;
            StartCoroutine(GameObject.Find("GameManager").GetComponent<GameManager>().Win());
            yield break;
        }


        for(int i = 0; i < levels[levelCounter].Count; i++)
        {
            currentLevelWords.Add(levels[levelCounter][i]);
            //Debug.Log(currentLevelWords[i]);
        }
        levelComplete = false;
        levelCounter++;
        SetupNextWord(currentLevelWords[0]);
    }
}
