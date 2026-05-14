using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance;
    public TMP_Text scoreText;
    public scORE _ScoreRef;

    void Awake()
    {
        Instance = this;
    
    }

    void Start()
    {
       UpdateUI();
    }

    void UpdateUI()
    {
        scoreText.text=_ScoreRef.scoreText.text;
        scoreText.text = "SCORE:"+_ScoreRef.point;
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag=="Player")
        {
            _ScoreRef.point+=1;
            UpdateUI();
            Destroy(this.gameObject);
        }
    }

    // Added this function for CollisionHandler
    public int GetScore()
    {
        return _ScoreRef.point;
    }
}