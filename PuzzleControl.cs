using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using UnityEngine.SceneManagement;

public class PuzzleControl : MonoBehaviour
{
    [SerializeField] int xCount = 3, yCount = 2;
    [SerializeField] GameObject piecePrefab;
    [SerializeField] Transform parantCanvas;
    [SerializeField] List<Sprite> pieceSprite;
    [SerializeField] List<string> pieceName;
    [SerializeField] List<GameObject> pieceList;
    int count = 0, gameguess = 0, rightguess = 0;
    [SerializeField] GameObject firstObject, secondObject;
    private void Start()
    {
        StartCoroutine(SpawnPiece());
    }

    private IEnumerator SpawnPiece()
    {
        for (int y = 0; y < yCount; y++)
        {
            for (int x = 0; x < xCount; x++)
            {
                GameObject piece = Instantiate(piecePrefab, transform);
                piece.name = x.ToString() + "_" + y.ToString();
                piece.transform.SetParent(parantCanvas);
                pieceList.Add(piece);
                int rnd = Random.Range(0, pieceSprite.Count);
                while (2 == CountControl(pieceName, pieceSprite[rnd].name))
                {
                    rnd = Random.Range(0, pieceSprite.Count);
                }
                pieceName.Add(pieceSprite[rnd].name);
                piece.transform.GetChild(0).GetComponent<Image>().sprite = pieceSprite[rnd];
                yield return new WaitForSeconds(0.1f);
            }
        }
        yield return new WaitForSeconds(0.5f);
        parantCanvas.GetComponent<GridLayoutGroup>().enabled = false;

        PieceDisable();
    }
    private int CountControl(List<string> list, string value)
    {
        int count = list.Where(temp => temp.Equals(value)).Select(temp => temp).Count();
        return count;
    }
    private void PieceDisable()
    {
        foreach (GameObject item in pieceList)
        {
            item.transform.GetChild(0).GetComponent<Image>().enabled = false;
        }
    }
    public void PieceShow(GameObject click)
    {
        StartCoroutine(Piece(click));
    }



    public IEnumerator Piece(GameObject click)
    {

        if (count == 0)
        {
            firstObject = click;
            firstObject.transform.GetChild(0).GetComponent<Image>().enabled = true;
            count = 1;
        }
        else if (count == 1)
        {
            secondObject = click;
            secondObject.transform.GetChild(0).GetComponent<Image>().enabled = true;
            yield return new WaitForSeconds(0.5f);
            gameguess++;
            if (firstObject.name != secondObject.name)
            {
                
                if (firstObject.transform.GetChild(0).GetComponent<Image>().sprite.name == secondObject.transform.GetChild(0).GetComponent<Image>().sprite.name)
                {
                    Destroy(firstObject);
                    Destroy(secondObject);
                    rightguess++;
                    gameguess--;
                }
                else
                {
                    firstObject.transform.GetChild(0).GetComponent<Image>().enabled = false;
                    secondObject.transform.GetChild(0).GetComponent<Image>().enabled = false;
                }
                if (rightguess >= 3)
                {
                    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
                }
                if (gameguess > 4)
                {
                    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
                }
                count = 0;
            }

        }
    }
}
