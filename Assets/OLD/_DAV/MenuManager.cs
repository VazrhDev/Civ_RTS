using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    [SerializeField] private GameObject pauseMenuPanel;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            pauseMenuPanel.SetActive(true);
        }
    }

    public void OnClick_MainMenuBtn()
    {
        SceneManager.LoadScene(0);
    }

    public void OnClick_ResumeBtn()
    {
        pauseMenuPanel.SetActive(false);
    }
}
