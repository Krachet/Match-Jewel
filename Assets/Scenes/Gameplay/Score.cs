using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Score : Singleton<Score>
{
    public int score;

    public TextMeshProUGUI scoreText;
    // Start is called before the first frame update
    void Start()
    {
        score = 0;
    }

    // Update is called once per frame
    void Update()
    {
        scoreText.text = "Score: " + score;
    }
}
