using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class KeyboardManager : MonoBehaviour
{
    private KeyboardInput board;
    // Start is called before the first frame update
    void Awake()
    {
        board = new KeyboardInput();

        board.Typing.Enable();
        board.Typing.key.performed += Typing_Performed;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Typing_Performed(InputAction.CallbackContext context)
    {
        if (context.performed == true)
        {
            Debug.Log(context.control.name);
        }
    }

    private void OnDisable()
    {
        board.Typing.key.performed -= Typing_Performed;
        board.Typing.Disable();
    }
}
