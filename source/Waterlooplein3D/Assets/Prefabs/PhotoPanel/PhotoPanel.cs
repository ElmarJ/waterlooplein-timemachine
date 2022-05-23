using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[ExecuteInEditMode]
public class PhotoPanel : MonoBehaviour
{

    [Header("Picture")]
    public Texture Picture;

    public string Title = "<< Title >>";

    [Multiline]
    public string Body = "";
    public string Author = "";
    public string Date = "";
    public string URL = "";

    [Header("Appearance")]
    public Color PanelColor = new Color(0.1f, 0.1f, 0.1f, 0.85f);
    public Color TitleColor = new Color(1.0f,1.0f,1.0f,1.0f);
    public Color BodyColor = new Color(1.0f,1.0f,1.0f,0.80f);
    public Vector2 Size = new Vector2(1.0f, 1.0f);

    [Header("Object-links (Do not touch)")]
    public TextMeshPro TitleObject;
    public TextMeshPro BodyObject;
    public TextMeshPro DateObject;
    public TextMeshPro AuthorObject;
    public GameObject[] OtherSubObjects;
    public GameObject Root;
    public GameObject Generic;
    public Renderer PictureRenderer;
    
	void Start ()
    {
		Layout();
        if (PictureRenderer != null && PictureRenderer.sharedMaterial != null)
        {
            PictureRenderer.sharedMaterial = new Material(PictureRenderer.sharedMaterial)
            {
                color = Color.white,
                mainTexture = Picture
            };
        }
	}
	
	// Update is called once per frame
	void Update ()
    {
        if(!Application.isPlaying)
        {
            Layout();
        }
	}

    public void Layout()
    {
        TitleObject.text = Title;
        BodyObject.text = Body;
        DateObject.text = Date;
        AuthorObject.text = Author;

        TitleObject.color = TitleColor;
        BodyObject.color = BodyColor;

        // Note: a default plane is 10x10, so we need to scale it:
        PictureRenderer.gameObject.transform.localScale = new Vector3(0.1f * Size.x, 0.1f, 0.1f * Size.y);
    }
}
