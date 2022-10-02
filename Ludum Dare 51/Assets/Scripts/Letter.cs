using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Letter : MonoBehaviour
{
    public TMP_Text tmpText;
    Mesh mesh;
    Mesh colorMesh;
    public bool red;
    RectTransform underLine;

    Vector3[] vertices;
    Color[] colors;

    private SpriteRenderer render;
    // Start is called before the first frame update
    void Awake()
    {
        tmpText = GetComponent<TMP_Text>();
        render = GetComponent<SpriteRenderer>();
        red = false;

        underLine = transform.GetChild(0).GetComponent<RectTransform>();
        //Debug.Log(Screen.width);
        //Debug.Log(Screen.height);
    }

    // Update is called once per frame
    void Update()
    {
        textIdleEffect();
        UnderLineFollow();
        Green();
        if (red)
        {
            Red();
        }

        
    }

    private void LateUpdate()
    {
        tmpText.canvasRenderer.SetMesh(mesh);
    }

    void UnderLineFollow()
    {
        TMP_CharacterInfo c = tmpText.textInfo.characterInfo[WordManager.letterIndex];
        int index = c.vertexIndex;

        if (!c.isVisible)
        {
           // index += WordManager.letterIndex + 1;
            //underLine.anchoredPosition = new Vector3(tmpText.mesh.vertices[index - 3].x + 6, tmpText.mesh.vertices[index - 3].y - 10f, 0);
            return;
        }

        float xPos = (tmpText.mesh.vertices[index].x + tmpText.mesh.vertices[index+2].x)/2;
        underLine.anchoredPosition = new Vector3(xPos , tmpText.mesh.vertices[index].y - 10f, 0);
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
        //tmpText.canvasRenderer.SetMesh(mesh);

        //Vector3 offset = Wobble(Time.time);
       // transform.Translate((offset) * Time.deltaTime);
    }



    public void Red()
    {
        colors = mesh.colors;

        for (int i = 0; i < WordManager.letterIndex; i++)
        {

        }

        TMP_CharacterInfo c = tmpText.textInfo.characterInfo[WordManager.letterIndex];

        if (!c.isVisible)
        {
            return;
        }

        int index = c.vertexIndex;  

        colors[index] = Color.red;
        colors[index + 1] = Color.red;
        colors[index + 2] = Color.red;
        colors[index + 3] = Color.red;


        mesh.colors = colors;
    }

    public void Green()
    {
        colors = mesh.colors;

        for (int i = 0; i < WordManager.letterIndex; i++)
        {
            TMP_CharacterInfo c = tmpText.textInfo.characterInfo[i];

            int index = c.vertexIndex;

            colors[index] = Color.green;
            colors[index + 1] = Color.green;
            colors[index + 2] = Color.green;
            colors[index + 3] = Color.green;
        }

        mesh.colors = colors;
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
