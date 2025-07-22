using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Card : MonoBehaviour
{
    public Image frontImage;
    public Image backImage;

    public int cardID;
    public bool isMatched = false;

    private bool isFlipped = false;
    private bool isAnimating = false;

    public void Setup(Sprite sprite, int id)
    {
        cardID = id;
        frontImage.sprite = sprite;
        frontImage.enabled = false;
        backImage.enabled = true;
        isFlipped = false;
    }

    public void OnClick()
    {
        if (isMatched || isFlipped || isAnimating)
            return;

        StartCoroutine(FlipCard(true));
        GameManager.instance.CardSelected(this);
    }

    public void FlipBack()
    {
        if (!isMatched && isFlipped && !isAnimating)
            StartCoroutine(FlipCard(false));
    }

    IEnumerator FlipCard(bool showFront)
    {
        isAnimating = true;

        // Scale down
        for (float t = 0; t < 0.2f; t += Time.deltaTime)
        {
            float scale = Mathf.Lerp(1f, 0f, t / 0.2f);
            transform.localScale = new Vector3(scale, 1f, 1f);
            yield return null;
        }

        // Swap images
        frontImage.enabled = showFront;
        backImage.enabled = !showFront;
        isFlipped = showFront;

        // Scale up
        for (float t = 0; t < 0.2f; t += Time.deltaTime)
        {
            float scale = Mathf.Lerp(0f, 1f, t / 0.2f);
            transform.localScale = new Vector3(scale, 1f, 1f);
            yield return null;
        }

        transform.localScale = Vector3.one;
        isAnimating = false;
    }

    public void MarkAsMatched()
    {
        isMatched = true;
    }
}
