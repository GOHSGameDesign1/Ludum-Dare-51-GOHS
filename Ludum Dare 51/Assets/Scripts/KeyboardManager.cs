using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class KeyboardManager : MonoBehaviour
{
    private Keyboard board;
    private InputAction.CallbackContext lastContext;
    public static char currentKey;
    // Start is called before the first frame update
    void Awake()
    {
        //board = new Keyboard();
        //currentKey = null;
        Keyboard.current.onTextInput += Typing_Performed;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Typing_Performed(char a)
    {
        Debug.Log(a);
        currentKey = a;
    }


    private void OnDisable()
    {
        Keyboard.current.onTextInput -= Typing_Performed;
    }
}
