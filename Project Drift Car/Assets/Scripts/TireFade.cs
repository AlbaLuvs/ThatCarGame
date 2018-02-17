using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TireFade : MonoBehaviour {

    [SerializeField]
    float fadeTimer;
    [SerializeField]
    Color ownColor, fadedColor;
    SpriteRenderer colorRenderer;


    private void Start()
    {
        colorRenderer = GetComponent<SpriteRenderer>();
        ownColor = colorRenderer.color;
        fadedColor = ownColor;
        fadedColor.a = 0;
    }

    void Update () {
        fadeTimer += 0.03f;

        colorRenderer.color = Color.Lerp(ownColor, fadedColor, fadeTimer);

        if (fadeTimer > 1)
        {
            Destroy(gameObject);
        }
	}
}
