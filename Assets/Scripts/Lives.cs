using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Lives : MonoBehaviour
{
    public TextMeshProUGUI livesText;

    // Update is called once per frame
    void Update()
    {
        livesText.text = PlayerStats.Lives + " LIVES";
    }
}
