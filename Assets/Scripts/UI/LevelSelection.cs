using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelSelection : MonoBehaviour
{

    [SerializeField] Button tutorial;
    [SerializeField] Button level1;
    [SerializeField] Button level2;
    [SerializeField] Button level3;
    [SerializeField] Button level4;

    Button[] levelButtons; 

    public int maxLevelComplete = 0;
    
    // Start is called before the first frame update
    void Start()
    {
        //Get maxLevelComplete from GameManager 
        maxLevelComplete = PlayerPrefs.GetInt("MaxLevelCompleted", 0);
        Debug.Log("Max LevelCompleted:" + maxLevelComplete);

        tutorial.onClick.AddListener(() => GoToTutorial());

        levelButtons = new Button[] {level1, level2, level3, level4};
        for (int i = 0; i < levelButtons.Length; i++)
        {
            if (i + 3 > maxLevelComplete)
            {
                levelButtons[i].interactable = false;
            }
            int levelIndex = i + 4;
            levelButtons[i].onClick.AddListener(() => LoadLevel(levelIndex));
        }

        //Level one is always unlocked
        level1.interactable = true;
    }

    public void UpdateMaxLevelComplete(){
        maxLevelComplete = PlayerPrefs.GetInt("MaxLevelCompleted", 0);
        Debug.Log("Max LevelCompleted:" + maxLevelComplete);
    }

    public void GoToTutorial(){
        SceneManager.LoadScene("S3_Tutorial");
    }

    public void LoadLevel(int levelIndex)
    {
        GameManager gameManager = GameManager.instance;
        if (gameManager == null){
            GameObject gmObject = new GameObject("GameManager");
            gmObject.AddComponent<GameManager>();
        }
        GameManager.instance.SetNextScene(levelIndex);
        GameManager.instance.GoToLevelViaLoadout(levelIndex);
    }
}