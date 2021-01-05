using UnityEngine;
using UnityEngine.UI;

public class MainDataController : MonoBehaviour
{

    private const int TEXTWIDTH = 250;

    public int textSize;
    public string titleText;
    public string bodyText;

    // Start is called before the first frame update
    void Start()
    {
        updateDisplay();
    }

    public void updateDisplay()
    {
        GameObject panel = GameObject.Find("MainDataPanel");
        GameObject title = GameObject.Find("MainDataTitle");
        GameObject body = GameObject.Find("MainDataText");

        panel.GetComponent<RectTransform>().sizeDelta = new Vector2(TEXTWIDTH, textSize);
        title.GetComponent<RectTransform>().sizeDelta = new Vector2(TEXTWIDTH, textSize);
        title.GetComponent<Text>().text = titleText;
        body.GetComponent<RectTransform>().sizeDelta = new Vector2(TEXTWIDTH, textSize - 50);
        body.GetComponent<Text>().text = bodyText;
    }

}
