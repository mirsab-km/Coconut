using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    private List<Card> flippedCards = new List<Card>();
    private bool isChecking = false;

    private void Awake()
    {
        instance = this;
    }

    public void CardSelected(Card card)
    {
        if (isChecking || card.isMatched || flippedCards.Contains(card))
            return;

        flippedCards.Add(card);

        // Check if we have two cards to compare
        if (flippedCards.Count == 2)
        {
            StartCoroutine(CheckMatch());
        }
    }

    IEnumerator CheckMatch()
    {
        isChecking = true;
        yield return new WaitForSeconds(0.5f);

        Card card1 = flippedCards[0];
        Card card2 = flippedCards[1];

        if (card1.cardID == card2.cardID)
        {
            card1.MarkAsMatched();
            card2.MarkAsMatched();
        }
        else
        {
            card1.FlipBack();
            card2.FlipBack();
        }

        flippedCards.Clear();
        isChecking = false;
    }
}
