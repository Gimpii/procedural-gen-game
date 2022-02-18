using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class genManager : MonoBehaviour
{

    [SerializeField] private Button genbtn;
    [SerializeField] private InputField genseed;

    private string seed;
    private void Start()
    {
        Button genbutton = genbtn.GetComponent<Button>();
        genbutton.onClick.AddListener(onclick);
        
    }

    
    void onclick()
    {
        Random.InitState((int)System.DateTime.Now.Ticks);
        seed = "";
        for (int i = 0; i < 8; i++)
        {
            seed = seed + Random.Range(0, 10);
        }
        genseed.text = seed;
        int b = seed.GetHashCode();
        print(b);
        Random.InitState(b);

    }
    void Update()
    {
    }
}
