using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextAnimation : MonoBehaviour
{
    public float animationLength;
    public Vector3 endPos;
    public int endSize;

    private float animationDelta = 0;
    private bool animating = false;
    private Vector3 startPos;
    private int startSize;

    private RectTransform RT;
    private Text text;

    // Start is called before the first frame update
    void Start()
    {
        RT = GetComponent<RectTransform>();
        text = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        // Move to the new position
        if (animating)
        {
            // Work out how far through the animation it is
            animationDelta += Time.deltaTime;
            float percent = animationDelta / animationLength;

            // Lerp to the target values
            transform.localPosition = Vector3.Lerp(startPos, endPos, percent);
            text.fontSize = (int)Mathf.Lerp(startSize, endSize, percent);

            // Stop if there
            if (percent > 1)
            {
                animating = false;
            }
        }
    }

    // Starts moving the text
    public void startAnimation()
    {
        // Store the start size and position
        startPos = transform.localPosition;
        startSize = text.fontSize;

        // Calculate the end position as the middle of the canvas
        RectTransform canvas = GetComponentInParent<RectTransform>();
        endPos = new Vector3(canvas.rect.width / 2, canvas.rect.height / 2, 0);

        animating = true;
    }
}
