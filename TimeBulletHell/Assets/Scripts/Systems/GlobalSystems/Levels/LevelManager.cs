using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    [SerializeField]
    private List<LevelScenePair> levels;

    private Dictionary<string, string> levelMap;

    void Start()
    {
        EventManager.instance.switchToLevelEvent += this.loadLevel;
        this.levelMap = new Dictionary<string, string>();
        this.initLevels();
    }

    private void initLevels()
    {
        foreach(LevelScenePair lsp in this.levels)
        {
            this.levelMap.Add(lsp.levelId, lsp.sceneName);
        }
    }

    private void loadLevel(string levelId)
    {
        string s;
        if (this.levelMap.TryGetValue(levelId, out s)) {
            SceneManager.LoadScene(s);
            EventManager.instance.onLevelSwitch(levelId);
        }
    }
}
