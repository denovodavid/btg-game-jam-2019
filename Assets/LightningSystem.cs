using System.Collections;
using UnityEngine;

public class LightningSystem : MonoBehaviour
{
    public SpriteRenderer boat;
    public GameObject warningBoxPrefab;
    public GameObject lightningPrefab;
    public TheCamera theCamera;
    public GameUI gameUI;
    public float yValue = -0.5f;
    public float zValue = -7f;

    public void Strike()
    {
        StartCoroutine(DoStrike());
    }

    IEnumerator DoStrike()
    {
        // pick random point
        var xValue = Random.Range(boat.bounds.min.x, boat.bounds.max.x);
        var strikePos = new Vector3(xValue, yValue, zValue);
        // show warning that flashes for 2 seconds
        var warning = Instantiate(warningBoxPrefab, strikePos, Quaternion.identity, transform);
        yield return new WaitForSeconds(2);
        Destroy(warning);
        // strike lightning and screen flash
        yield return new WaitForSeconds(0.2f);
        var lightning = Instantiate(lightningPrefab, strikePos, Quaternion.identity, transform);
        theCamera.Impact();
        gameUI.ScreenFlash();
        // kill player
        var results = new Collider2D[8];
        var resultNum = Physics2D.OverlapBoxNonAlloc(strikePos, Vector2.one, 0, results);
        for (int i = 0; i < resultNum; i++)
        {
            if (results[i].name.Equals("Character"))
            {
                Debug.Log("STRIKE!");
                GameManager.instance.GameOver();
            }
        }
        // cleanup
        yield return new WaitForSeconds(0.5f);
        Destroy(lightning);
    }
}
