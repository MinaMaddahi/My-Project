using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class StartButton : MonoBehaviour
{
    private Button button;
    // Start is called before the first frame update
    void Start()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(StartGame);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void StartGame()
    {

        Debug.Log("was clicked");
    }
}