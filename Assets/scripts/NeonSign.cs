using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class NeonSign : MonoBehaviour
{
    public Button thisButton;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        thisButton.onClick.AddListener(async () => 
        {
            SceneManager.LoadScene(1);
            await SceneManager.UnloadSceneAsync(0);
        });
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
