using UnityEngine;
using UnityEngine.UI;

public class Card : MonoBehaviour
{
    public Image frontImage;
    public Image backImage;

    public int cardID;
    public bool isMatched = false;

    public void Setup(Sprite sprite, int id)
    {
        frontImage.sprite = sprite;
        cardID = id;

        frontImage.enabled = false;
        backImage.enabled = true;
    }

    public void Flip()
    {
        frontImage.enabled = true;
        backImage.enabled = false;
    }

    public void FlipBack()
    {
        frontImage.enabled = false;
        backImage.enabled = true;
    }

    public void OnClick()
    {
        if (isMatched || frontImage.enabled || GameManager.instance.isBusy)
            return;

        Flip();
        GameManager.instance.CardSelected(this);
    }
}
