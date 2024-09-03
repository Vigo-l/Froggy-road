using TMPro;
using UnityEngine;

public class KeepScore : MonoBehaviour
{
    public PlayerController Score;
    public TextMeshProUGUI score;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        score.text = "Your score: " + Score.score.ToString();
    }
}
