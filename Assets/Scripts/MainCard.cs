using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCard : MonoBehaviour
{

    GameObject controller;
    SpriteRenderer renderer;

    // create face and back of card variables
    public Sprite[] faces;
    public Sprite back;
    public int faceIndex;
    public bool matched = false;
    public bool clickable = false;
    public bool isMatched = false;

    public void OnMouseDown(){

        if (clickable == true){
            // only allow player to flip cards if there is not already a match
            if(isMatched == false) {

                if(renderer.sprite == back) {

                    if(controller.GetComponent<Controller>().TwoFaces() == false) {
                        renderer.sprite = faces[faceIndex];
                        controller.GetComponent<Controller>().AddFace(faceIndex);
                        matched = controller.GetComponent<Controller>().CardsMatch();
                    }

                }
                else {
                    renderer.sprite = back;
                    controller.GetComponent<Controller>().RemoveFace(faceIndex);
                }
            }
        }
    }

    // function used in controller to flip all the cards at the start of the game then
    // flip them back
    public void showFace(int index) {
        renderer.sprite = faces[index];
    }

    public void showBack() {
        renderer.sprite = back;
    }

    private void Awake() {
        renderer = GetComponent<SpriteRenderer>();
        controller = GameObject.Find("Controller");
    }
}
