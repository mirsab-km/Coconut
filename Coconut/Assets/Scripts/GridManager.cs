using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GridManager : MonoBehaviour
{
    public GameObject cardPrefab;
    public Transform gridParent;
    public int rows = 2;
    public int columns = 2;

    private void Start()
    {
        GenerateGrid();
    }

    void GenerateGrid()
    {
        int totalCards = rows * columns;

        // Just using temp matching logic (0, 0, 1, 1, 2, 2, ...)
        List<int> cardIDs = new List<int>();
        for (int i = 0; i < totalCards / 2; i++)
        {
            cardIDs.Add(i);
            cardIDs.Add(i); // matching pair
        }

        // Shuffle the list
        for (int i = 0; i < cardIDs.Count; i++)
        {
            int temp = cardIDs[i];
            int randomIndex = Random.Range(i, cardIDs.Count);
            cardIDs[i] = cardIDs[randomIndex];
            cardIDs[randomIndex] = temp;
        }

        // Create cards
        for (int i = 0; i < totalCards; i++)
        {
            GameObject card = Instantiate(cardPrefab, gridParent);
            card.name = "Card_" + i;

            // TODO: Assign card ID to each card (used for matching)
            card.GetComponent<Card>().cardID = cardIDs[i];
        }

        // Set layout constraint
        GridLayoutGroup layout = gridParent.GetComponent<GridLayoutGroup>();
        layout.constraint = GridLayoutGroup.Constraint.FixedColumnCount;
        layout.constraintCount = columns;
    }
}
