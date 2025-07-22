using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class GridManager : MonoBehaviour
{
    public GameObject cardPrefab;
    public Transform gridParent;
    public Sprite[] fruitSprites;
    public int rows = 2;
    public int columns = 2;

    void Start()
    {
        int totalCards = rows * columns;

        // STEP 1: Create sprite+ID pairs
        List<(Sprite, int)> cardData = new List<(Sprite, int)>();
        for (int i = 0; i < totalCards / 2; i++)
        {
            cardData.Add((fruitSprites[i], i));
            cardData.Add((fruitSprites[i], i));
        }

        // STEP 2: Shuffle the list
        for (int i = 0; i < cardData.Count; i++)
        {
            int rand = Random.Range(i, cardData.Count);
            (cardData[i], cardData[rand]) = (cardData[rand], cardData[i]);
        }

        // STEP 3: Setup grid layout
        GridLayoutGroup grid = gridParent.GetComponent<GridLayoutGroup>();
        grid.constraint = GridLayoutGroup.Constraint.FixedColumnCount;
        grid.constraintCount = columns;
        grid.cellSize = new Vector2(100, 100); // tweak if needed

        // STEP 4: Instantiate cards
        for (int i = 0; i < totalCards; i++)
        {
            GameObject cardObj = Instantiate(cardPrefab, gridParent);
            Card card = cardObj.GetComponent<Card>();
            card.Setup(cardData[i].Item1, cardData[i].Item2);
        }
    }
}
