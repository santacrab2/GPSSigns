
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using System.Threading.Tasks;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class fairycatch : MonoBehaviour
{
    public ARRaycastManager raymanager;
    TouchInput InputActions;
    public GameObject fairy;
    List<ARRaycastHit> hits = new();
    int score = 0;
    public TextMeshProUGUI scoretext;
    public Button backbutton;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        InputActions = new();
        backbutton.onClick.AddListener(() => 
        { 
            SceneManager.LoadScene("0");
            SceneManager.UnloadSceneAsync(3);
        });
    }
    public void OnTouch(InputValue value)
    {

        Vector2 touchposition = value.Get<Vector2>();
        Ray ray = Camera.main.ScreenPointToRay(touchposition);
        if(Physics.Raycast(ray, out RaycastHit hit))
        {
            if(hit.transform.gameObject.tag == "fairy")
            {
                Destroy(hit.transform.gameObject);
                score++;
                scoretext.text = "Captured Fairies: " + score.ToString();
                Task.Delay(1000).Wait();
                Instantiate(fairy, new Vector3(Random.Range(-2f, 2f), Random.Range(0.5f, 3f), Random.Range(-2f, 2f)), Quaternion.identity);
            }
        }

    }
    // Update is called once per frame
    void Update()
    {
       
    }

}

