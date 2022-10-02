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

    public Vector3[] ogPos;
    private Vector3 spawnPos;

    RectTransform rect;

    private SpriteRenderer render;
    // Start is called before the first frame update
    void Awake()
    {
        tmpText = GetComponent<TMP_Text>();
        render = GetComponent<SpriteRenderer>();
        red = false;

        underLine = transform.GetChild(0).GetComponent<RectTransform>();

        rect = GetComponent<RectTransform>();

        spawnPos = new Vector3(0, -2.35f, 0);

        mesh = tmpText.mesh;
        //Debug.Log(Screen.width);
        //Debug.Log(Screen.height);
    }

    private void OnEnable()
    {

    }

    // Update is called once per frame
    void Update()
    {
        textIdleEffect();
        //TextSpawn();
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

    void TextSpawn()
    {
        mesh = tmpText.mesh;
        vertices = mesh.vertices;
        Debug.Log(vertices.Length);
        for(int i = 0; i < tmpText.textInfo.characterCount; i++)
        {

            TMP_CharacterInfo c = tmpText.textInfo.characterInfo[i];

            if (!c.isVisible)
            {
                continue;
            }

            int index = c.vertexIndex;

            /* Vector3 lerpPos = new Vector3(Mathf.Lerp(transform.InverseTransformPoint(spawnPos).x, transform.InverseTransformPoint(ogPos).x, Time.time + i),
                 Mathf.Lerp(transform.InverseTransformPoint(spawnPos).y, transform.InverseTransformPoint(ogPos).y, Time.time + i), 0);*/

            //vertices[index + 0] = Vector3.Lerp(new Vector3(0, -220, 0), ogPos[index + 0], Time.time + i);
           // vertices[index + 1] = Vector3.Lerp(new Vector3(0, -220, 0), ogPos[index + 1], Time.time + i);
           // vertices[index + 2] = Vector3.Lerp(new Vector3(0, -220, 0), ogPos[index + 2], Time.time + i);
           // vertices[index + 3] = Vector3.Lerp(new Vector3(0, -220, 0), ogPos[index + 3], Time.time + i);
            for (int j = 0; j < 4; j++)
            {
                Debug.Log(ogPos.Length);
                if(ogPos.Length == 0)
                {
                    return;
                }
                Vector3 lerpPos = Vector3.Lerp(new Vector3(0, -220,0), ogPos[index + j], Time.time * i);

                //Debug.Log(ogPos);
                //Debug.Log(rect.InverseTransformPoint(spawnPos));

                vertices[index + j] = lerpPos;
            }


        }
        mesh.vertices = vertices;
    }

    void TextFade()
    {
        tmpText.ForceMeshUpdate();
        colors = mesh.colors;

        for(int i = 0; i < tmpText.textInfo.characterCount; i++)
        {
            TMP_CharacterInfo c = tmpText.textInfo.characterInfo[i];

            int index = c.vertexIndex;

            colors[index] = new Color(colors[index].r, colors[index].g, colors[index].b, colors[index].a);
            colors[index + 1] = Color.green;
            colors[index + 2] = Color.green;
            colors[index + 3] = Color.green;
        }
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
        //Debug.Log(tmpText.mesh.vertices.Length);
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
            ogPos = tmpText.mesh.vertices;
        }
    }

    Vector2 Wobble(float time)
    {
        return new Vector2(Mathf.Sin(time * 5.3f), Mathf.Cos(time * 5.3f));
    }
}
