using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
    GameObject card;
    public GameOver GameOverScreen;

    // create list to represent each different kind of card face (2 of each)
    List<int> faceIndexes = new List<int> {
        0, 1, 2, 3, 4, 5, 6, 7,
        0, 1, 2, 3, 4, 5, 6, 7
        };

    public static System.Random rand = new System.Random();
    public int shuffle = 0;
    int[] visibleFaces = {-1, -1};

    // set distance each card will be from eachother
    float distance = 2.5f;
    // Start is called before the first frame update
    void Start()
    {

        int startingLength = faceIndexes.Count;

        // starting position for the 2nd left card at the top from the left
        // and starting y position for the first row
        float yPosition = 3.75f;
        float xPosition = -1.25f; 

        // loop to place each card on the board
        for (int i=1; i<16; i+=1) {

            // shuffle cards each loop
            shuffle = rand.Next(0, (faceIndexes.Count));

            var temp = Instantiate(card, new Vector3(
                xPosition, yPosition, 0),
            Quaternion.identity);
            
            // set face
            temp.GetComponent<MainCard>().faceIndex = faceIndexes[shuffle];
            faceIndexes.Remove(faceIndexes[shuffle]);

            xPosition += distance;

            // simulate rows and cols
            if((i+1)%4==0) {
                xPosition = -3.75f;
                yPosition -= distance;
            }
        }
        card.GetComponent<MainCard>().faceIndex = faceIndexes[0];
    }

    // function to determine whether two cards have been flipped
    public bool TwoFaces() {
        bool cardsUp = false;
        if (visibleFaces[0] >= 0 && visibleFaces[1] >= 0) {
            cardsUp = true;
        }
        return cardsUp;
    }

    // add turned over face to visible faces array
    public void AddFace(int index) {
        if(visibleFaces[0] < 0) {
            visibleFaces[0] = index;
        }
        else if (visibleFaces[1] < 0) {
            visibleFaces[1] = index;
        }
    }

    // remove turned over card from visible faces array (flipped back)
    public void RemoveFace(int index) {
        if(visibleFaces[0] == index) {
            visibleFaces[0] = -1;
        }
        else if (visibleFaces[1] == index) {
            visibleFaces[1] = -1;
        }
    }

    // check if cards match
    public bool CardsMatch() {
        if(visibleFaces[0] == visibleFaces[1]) {
            visibleFaces[0] = -1;
            visibleFaces[1] = -1;
            return true;
        }
        print("Game Should Be Over");
        GameIsOver(0);
        return false;
    }

    // 
    public void GameIsOver(int result) {
        GameOverScreen.Setup(result);
    }

    void Awake()
    {
        card = GameObject.Find("Card");
    }
}
