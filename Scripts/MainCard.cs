using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCard : MonoBehaviour
{
    [SerializeField]
    private SceneController controller;
    [SerializeField]
    private GameObject _back;
    private int _id;
    [SerializeField]
    private AudioSource _audioSource;

    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        if(_audioSource == null)
        {
            Debug.Log("AudioSource component is missing from this GameObject!");
        }
    }
    public void OnMouseDown()
    {
        if(_back.activeSelf && controller.canReveal)
        {
            _back.SetActive(false);
            controller.cardReveal(this);
            if(_audioSource != null)
            {
                _audioSource.Play();
            }
            
        }
    }
    public int id
    {
        get { return _id; }
    }
    public void ChangeSprite(int id, Sprite img)
    {
        _id = id;
        GetComponent<SpriteRenderer>().sprite = img;
    }
    public void Unreveal()
    {
        _back.SetActive(true);
    }

}
