using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Letter : MonoBehaviour
{
    public TMP_Text tmpText;
    Mesh mesh;
    Mesh colorMesh;
    public bool green;

    Vector3[] vertices;

    private SpriteRenderer render;
    // Start is called before the first frame update
    void Awake()
    {
        tmpText = GetComponent<TMP_Text>();
        render = GetComponent<SpriteRenderer>();
        green = false;

        Debug.Log(Screen.width);
        Debug.Log(Screen.height);
    }

    // Update is called once per frame
    void Update()
    {
        textIdleEffect();
        if (green)
        {
            FlashGreen(0);
        }
    }

    void textIdleEffect()
    {
        tmpText.ForceMeshUpdate();
        mesh = tmpText.mesh;
        vertices = mesh.vertices;

        for(int i = 0; i < tmpText.textInfo.characterCount; i++)
        {
            TMP_CharacterInfo c = tmpText.textInfo.characterInfo[i];

            int index = c.vertexIndex;
            
            Vector3 offset = Wobble(Time.time + i);
            vertices[index] += offset;
            vertices[index + 1] += offset;
            vertices[index + 2] += offset;
            vertices[index + 3] += offset;
        }

        mesh.vertices = vertices;
       // tmpText.SetVerticesDirty();
        tmpText.canvasRenderer.SetMesh(mesh);

        //Vector3 offset = Wobble(Time.time);
       // transform.Translate((offset) * Time.deltaTime);
    }



    public void FlashRed()
    {
        render.color = new Color(1, 0, 0, 0.2f);
    }

    public void FlashGreen(int letterIndex)
    {
        tmpText.ForceMeshUpdate();
        colorMesh = tmpText.mesh;

        TMP_CharacterInfo c = tmpText.textInfo.characterInfo[letterIndex];

        int index = c.vertexIndex;

        Color[] colors = mesh.colors;

        colors[index] = Color.cyan;
        colors[index + 1] = Color.cyan;
        colors[index + 2] = Color.cyan;
        colors[index + 3] = Color.cyan;

        mesh.colors = colors;

        tmpText.canvasRenderer.SetMesh(mesh);


    }

    public void ChangeText(string newText)
    {
        if(tmpText != null)
        {
            tmpText.text = newText;
        }
    }

    Vector2 Wobble(float time)
    {
        return new Vector2(Mathf.Sin(time * 5.3f), Mathf.Cos(time * 5.3f));
    }
}
