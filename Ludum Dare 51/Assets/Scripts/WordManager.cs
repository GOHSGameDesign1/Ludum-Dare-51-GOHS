using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WordManager : MonoBehaviour
{
    private Queue<char> currentWordQueue = new Queue<char>();

    public List<string> words;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        checkLetters();
    }

    // checks if the current keyboard press is the same as the word, if the current word is finished, load a new word
    void checkLetters()
    {
        if(currentWordQueue.Count == 0)
        {
            if (words.Count == 0)
            {
                Debug.Log("Out of words!");
                return;
            }

            SetupNextWord(words[0]);
        }

        if(KeyboardManager.currentKey == currentWordQueue.Peek())
        {
            Debug.Log("Removing: " + currentWordQueue.Dequeue());
            KeyboardManager.currentKey = '|';
        }
    }

    // loads a new word, if the words for the level are done print hooray
    void SetupNextWord(string word)
    {
        words.RemoveAt(0);
        foreach (char letter in word)
        {
            //Debug.Log("Adding: " + letter);
            currentWordQueue.Enqueue(letter);
        }
    }
}
