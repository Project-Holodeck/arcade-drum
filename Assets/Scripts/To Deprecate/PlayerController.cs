// Hit detection, add scores, add, hide & unhide combos

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

/// <summary>
/// Should be fully deprectated now. Check that all the functionality is in LevelController and PlayerInputController.
/// </summary>
public class PlayerController : MonoBehaviour
{
    private GameObject[] circles0, circles1, circles2, circles3;
    private TextMeshProUGUI scoreCountText, comboText, comboCountText;
    private int scoreInt = 0;
    public int comboCount = 0;

    // Start is called before the first frame update
    void Start()
    {
        scoreCountText = GameObject.Find("ScoreCountText").GetComponent<TextMeshProUGUI>();
        comboText = GameObject.Find("ComboText").GetComponent<TextMeshProUGUI>();
        comboCountText = GameObject.Find("ComboCountText").GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.A)){
            circles0 = GameObject.FindGameObjectsWithTag("Circle Lane 0");
            Destruction(circles0);        
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            circles1 = GameObject.FindGameObjectsWithTag("Circle Lane 1");
            Destruction(circles1);
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            circles2 = GameObject.FindGameObjectsWithTag("Circle Lane 2");
            Destruction(circles2);
        }
        if (Input.GetKeyDown(KeyCode.F))
        {
            circles3 = GameObject.FindGameObjectsWithTag("Circle Lane 3");
            Destruction(circles3);
        }
        if (this.comboCount > 0)
        {
            comboText.maxVisibleCharacters = 5;
            comboCountText.maxVisibleCharacters = 10;
            comboCountText.text = comboCount.ToString() + 'x';
        }
        if (this.comboCount == 0)
        {
            comboText.maxVisibleCharacters = 0;
            comboCountText.maxVisibleCharacters = 0;
        }
    }
    
    void Destruction(GameObject[] circleList)
    {
        if (circleList.Length > 0)
        {
            GameObject circle = circleList[0];
            float distance = circle.transform.position.z;
            distance += 26.3f; //TODO: lane length
            if (distance <= 10) // TODO: accuracy based on model
            {
                scoreInt += (int)((1/distance)*50000.0f*(1 + comboCount/10f)); //edited formula: combocount actually matters in terms of scoring
                scoreCountText.text = scoreInt.ToString();
                this.comboCount++;
                Destroy(circle);
            }
            else
            {
                this.comboCount = 0;
            }
        }
        else
        {
            this.comboCount = 0;
        }
    }
}
