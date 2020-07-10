using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpdateUIText : MonoBehaviour
{
    [Header("Lives")]
    [SerializeField]
    private AGetTextForUI livesGetter;
    [SerializeField]
    private Text livesText;
    [Header("Coins")]
    [SerializeField]
    private AGetTextForUI coinsGetter;
    [SerializeField]
    private Text coinsText;
    [Header("Score")]
    [SerializeField]
    private AGetTextForUI scoreGetter;
    [SerializeField]
    private Text scoreText;

    void Update()
    {
        this.livesText.text = livesGetter.getText();
        this.coinsText.text = coinsGetter.getText();
        this.scoreText.text = scoreGetter.getText();
    }
}
