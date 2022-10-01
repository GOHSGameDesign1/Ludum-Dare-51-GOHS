using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class KeyboardManager : MonoBehaviour
{
    private Keyboard board;
    private InputAction.CallbackContext lastContext;
    public static char currentKey;

    public delegate void KeyPress(char cha);
    public static event KeyPress keyPressed;
    // Start is called before the first frame update
    void Awake()
    {
        //board = new Keyboard();
        //currentKey = null;
        //Keyboard.current.OnTextInput(currentKey);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Typing_Performed(char a)
    {
        currentKey = a;
        Debug.Log(a);

        if(keyPressed != null)
        {
            keyPressed(currentKey);
        }
    }

    private void OnEnable()
    {
        Keyboard.current.onTextInput += Typing_Performed;
    }


    private void OnDisable()
    {
        Keyboard.current.onTextInput -= Typing_Performed;
    }
}
