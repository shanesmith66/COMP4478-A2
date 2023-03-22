using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
    GameObject card;
    public GameOver GameOverScreen;
    
    // time to wait before the cards are flipped and the game is start
    int TIME_TO_MEMORIZE = 7;

    // list which will store all the cards
    public List<MainCard> allCards = new List<MainCard>();

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

            // add created list to cards
            allCards.Add(temp.GetComponent<MainCard>());

            faceIndexes.Remove(faceIndexes[shuffle]);

            xPosition += distance;

            // simulate rows and cols
            if((i+1)%4==0) {
                xPosition = -3.75f;
                yPosition -= distance;
            }
        }
        // set initial card to last item remaining in the cards
        card.GetComponent<MainCard>().faceIndex = faceIndexes[0];

        // add initial card to card list
        allCards.Add(card.GetComponent<MainCard>());

        StartCoroutine(startOfGame());
        

    }

    // function to flip all the cards at the start in order to allow the player a chance to memorize them
    // after 5 seconds, the cards will all flip back
    IEnumerator startOfGame() {
        for(int i=0; i<allCards.Count; i+=1) {
            allCards[i].showFace(allCards[i].faceIndex);
        }
        // wait for 5 seconds, then flip the cards to their backs
        yield return new WaitForSeconds(TIME_TO_MEMORIZE);
        for(int i=0; i<allCards.Count; i+=1) {
            allCards[i].showBack();
            allCards[i].clickable = true; // allow player to click on cards after this stage
        }

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

            MainCard card1 = null;
            MainCard card2 = null;
            for(int i=0; i<allCards.Count; i+=1) {
                if(allCards[i].faceIndex == visibleFaces[0]) {
                    if(card1 is null) {
                        card1 = allCards[i];
                    }
                    else if(card2 is null){
                        card2 = allCards[i];
                    }
                }
            }

            // mark both cards as matched by finding the index of the card in our list through
            card1.isMatched = true;
            card2.isMatched = true;
            card1.clickable = false;
            card2.clickable = false;
            visibleFaces[0] = -1;
            visibleFaces[1] = -1;
            gameWon();
            return true;
        }
        // if two cards are flipped but do not match, game is over
        else if (visibleFaces[0] >= 0 && visibleFaces[1] >= 0) {
            GameIsOver(0);
            return false;
        }
        return false;
    }

    // check for win
    public void gameWon() {
        for(int i=0; i<allCards.Count; i+=1) {
            if(allCards[i].isMatched == false) {
                return;
            }
        }
        GameIsOver(1);
    }

    // 
    public void GameIsOver(int result) {
        // disable clicking on cards while game is over
        for(int i=0; i<allCards.Count; i+=1) {
            allCards[i].clickable = false; // allow player to click on cards after this stage
        }
        GameOverScreen.Setup(result);
    }

    void Awake()
    {
        card = GameObject.Find("Card");
    }
}
