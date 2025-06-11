using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.UI;

public class Loader : MonoBehaviour
{
    [SerializeField]
    private Image m_Image;
    
    private List<Sprite> m_Sprites = new List<Sprite>();
    private int m_CurrentSprite = 0;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Addressables.InitializeAsync().Completed += handle =>
        {
            Debug.Log("Init completed: " + handle.Result);

            LoadSprite("white");
            LoadSprite("blue");
            LoadSprite("green");
            
            InvokeRepeating("SwitchSprite", 3f, 3f);
        };
    }

    private void LoadSprite(string spriteKey)
    {
        Addressables.LoadAssetAsync<Sprite>(spriteKey).Completed += handle =>
        {
            m_Sprites.Add(handle.Result);
        };
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void SwitchSprite()
    {
        if (m_Sprites.Count <= 0)
            return;
        
        m_Image.sprite = m_Sprites[m_CurrentSprite++];
        if (m_CurrentSprite >= m_Sprites.Count)
            m_CurrentSprite = 0;
    }
}
