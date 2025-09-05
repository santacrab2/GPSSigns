
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class backButton : MonoBehaviour
{
    public Button thisButton;
    public int sceneToUnload;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        thisButton.onClick.AddListener(async () => 
        {
            SceneManager.LoadScene(0);
            await SceneManager.UnloadSceneAsync(sceneToUnload);
        });
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
