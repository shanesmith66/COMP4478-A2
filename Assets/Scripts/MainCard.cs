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

    public void OnMouseDown(){

        // only allow player to flip cards if there is not already a match
        if(matched == false) {

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

    private void Awake() {
        renderer = GetComponent<SpriteRenderer>();
        controller = GameObject.Find("Controller");
    }
}
