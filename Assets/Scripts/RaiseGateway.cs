using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class RaiseGateway : MonoBehaviour
{
  public Vector3 initialPos;
  public Vector3 finalPos;
  public GameObject endGameCollider;
    // Start is called before the first frame update
    void Start()
    {
        // turn off end game collider
        endGameCollider.SetActive(false);
        StartCoroutine(RaiseGate(0.2f, transform));
    }

  /**
  * RaiseGate
  * @param {float} t - How long to fade in text
  * @param {i} i - Transform moving object
  */
  public IEnumerator RaiseGate(float t, Transform i)
  {
    i.position = initialPos;
      // Debug.Log("i.position.y: " + i.position.y);
      // Debug.Log("finalPos.y: " + finalPos.y);
    while (i.position.y < finalPos.y)
    {
      transform.position = new Vector3(finalPos.x, i.position.y + (Time.deltaTime / t), finalPos.z);
      yield return null;
    }
    // turn on end game collider
    endGameCollider.SetActive(true);
  }


}
