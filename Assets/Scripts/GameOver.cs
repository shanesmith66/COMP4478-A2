using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    public TMP_Text wonLostText;
    public void Setup(int result) {
        gameObject.SetActive(true);
        // set text to won if won, lost if game lost
        if(result==1) {
            wonLostText.text = "You Won!";
        }
        else {
            wonLostText.text = "You Lost";
        }
    }

    public void PlayAgainButton() {
        SceneManager.LoadScene("GameScene");
    }
}
