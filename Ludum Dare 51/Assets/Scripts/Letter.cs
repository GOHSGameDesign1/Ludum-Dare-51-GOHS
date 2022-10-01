using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Letter : MonoBehaviour
{
    public TMP_Text tmpText;
    private SpriteRenderer render;
    // Start is called before the first frame update
    void Awake()
    {
        tmpText = transform.GetComponentInChildren<TMP_Text>();
        render = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void FlashRed()
    {
        render.color = new Color(1, 0, 0, 0.2f);
    }

    public void FlashGreen()
    {
        render.color = new Color(0, 1, 0, 0.2f);
    }

    public void ChangeText(string newText)
    {
        if(tmpText != null)
        {
            tmpText.text = newText;
        }
    }
}
