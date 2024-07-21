using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneController : MonoBehaviour
{
    public const int gridrows = 2;
    public const int gridcolumns = 5;
    public const float offsetX = 3f;
    public const float offsetY = 3f;
    [SerializeField]
    private MainCard _originalCard;
    [SerializeField]
    private Sprite[] imgs;
    [SerializeField]
    private GameObject greetImg;

    private void Start()
    {
        Vector3 startPos = _originalCard.transform.position;
        int[] num = {0, 0, 1, 1, 2, 2, 3, 3, 4, 4};
        num = ShuffelArray(num);
        for (int i = 0; i < gridcolumns; i++)
        {
            for (int j = 0; j < gridrows; j++)
            {
                MainCard card = new MainCard();
                if( i == 0 && j == 0)
                {
                    card = _originalCard;
                }
                else
                {
                    card = Instantiate(_originalCard) as MainCard;
                }
                int index = j * gridcolumns + i;
                int id = num[index];
                card.ChangeSprite(id, imgs[id]);

                float posX = (offsetX * i) + startPos.x;
                float posY = (offsetY * j) + startPos.y;
                card.transform.position = new Vector3(posX, posY, startPos.z);
            }
        }
    }
    private int[] ShuffelArray(int[] num)
    {
        int[] newArray = num.Clone() as int[];
        for(int i = 0; i < newArray.Length; i++)
        {
            int temp = newArray[i];
            int r = Random.Range(i, newArray.Length);
            newArray[i] = newArray[r];
            newArray[r] = temp;
        }
        return newArray;
    }

    private MainCard _firstReveal;
    private MainCard _secondReveal;
    private int _score = 0;
    [SerializeField]
    private TextMesh _scoreLabel;
    public bool canReveal
    {
        get { return _secondReveal == null; }
    }
    
    public void cardReveal(MainCard c)
    {
        if(_firstReveal == null)
        {
            _firstReveal = c;
        }
        else
        {
            _secondReveal = c;
            StartCoroutine(CheckMatch());
        }
    }
    private IEnumerator CheckMatch()
    {
        if(_firstReveal.id == _secondReveal.id)
        {
            _score++;
            _scoreLabel.text = "Score: " + _score;
        }
        else
        {
            yield return new WaitForSeconds(0.5f);
            _firstReveal.Unreveal();
            _secondReveal.Unreveal();
        }
        _firstReveal = null;
        _secondReveal = null;
        if(_score == 5)
        {
            greetImg.SetActive(true);
        }
    }
}
