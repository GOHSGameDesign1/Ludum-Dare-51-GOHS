using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Letter : MonoBehaviour
{
    public TMP_Text tmpText;
    Mesh mesh;

    Vector3[] vertices;

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
        textIdleEffect();
    }

    void textIdleEffect()
    {
       // tmpText.ForceMeshUpdate();
       // mesh = tmpText.mesh;
       // vertices = mesh.vertices;

        //for(int i = 0; i < vertices.Length; i++)
       // {
            //Vector3 offset = Wobble(Time.time + i);
            //vertices[i] += offset;
       // }

       // mesh.vertices = vertices;
       // tmpText.SetVerticesDirty();
        //tmpText.canvasRenderer.SetMesh(mesh);

        Vector3 offset = Wobble(Time.time);
        transform.Translate((offset) * Time.deltaTime);
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

    Vector2 Wobble(float time)
    {
        return new Vector2(Mathf.Sin(time * 3.3f), Mathf.Cos(time * 3.3f));
    }
}
