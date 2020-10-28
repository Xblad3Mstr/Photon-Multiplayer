using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuScript : MonoBehaviour
{
    public int startButton;
    public int exitButton;

    public Scene gameScene;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
  
        if (Input.GetMouseButtonDown(exitButton))
        {
            Application.Quit();
        }
    }

    private void LoadScene(Scene sceneName)
    {
        //  if (Input.GetMouseButtonDown(startButton))
        //{
        //  SceneManager.LoadScene("GameScene");
        // }
        SceneManager.LoadScene(gameScene);
    }
}
