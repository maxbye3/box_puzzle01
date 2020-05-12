using UnityEngine;
using System.Collections;
using UnityEngine.UI;
 using UnityStandardAssets.Characters.FirstPerson;
using TMPro;

public class Ending : MonoBehaviour
{
  public GameObject fireworkLaunch;
  public GameObject fireworkBoom;
  public GameObject player;
  public GameObject endingTxt;

  void OnCollisionEnter(Collision collision) // Detect collisions between the GameObjects with Colliders attached
  {
    if (collision.gameObject.name == "Player")
    {
      EndGame();
    }
  }

  /*
  * End the game
  * Stop the user moving and move it close to white cube
  * Fade in text
  */
  void EndGame()
  {
    // turn off firework noises
    fireworkLaunch.SetActive(false);
    fireworkBoom.SetActive(false);

    if (player == null)
      player = GameObject.Find("Player");
    if (endingTxt == null)
      endingTxt = GameObject.Find("Ending text");

    // Disable scripts
    // player.GetComponent<FirstPersonController>().enabled = false;
    player.GetComponent<FirstPersonController>().enabled = false;

    // Fixed ending position 
    player.transform.position = new Vector3(8.02f, 7.78f, -20.5f);
    player.transform.rotation = Quaternion.Euler(0, -175, 0);


    // Start fade
    endingTxt.SetActive(true);
    // StartCoroutine(RotatePlayer(1f, player.transform));
    StartCoroutine(FadeTextToFullAlpha(5f, endingTxt.GetComponent<Text>()));
  }

  /**
  * Rotate player
  * @param {float} t - How long to fade in text
  * @param {i} i - GetComponent<Text>() text
  */
  // public IEnumerator RotatePlayer(float t, Transform i)
  // {
  //   // i.rotation = initialPos;
  //   // Debug.Log("player.transform.position: " + player.transform.rotation);
  //   Debug.Log("player.transform.position: " + player.transform.rotation.eulerAngles.y);

  //   float yRotation =  player.transform.rotation.eulerAngles.y;
  //   while (yRotation > 185)
  //   // {
  //     yRotation -= 1f;
  //     Debug.Log(yRotation);
  //     i.rotation = Quaternion.Euler(new Vector3(i.rotation.x, yRotation, i.rotation.z));
  //     yield return null;
  //   // }
  // }



  /**
  * Fade in text
  * @param {float} t - How long to fade in text
  * @param {i} i - GetComponent<Text>() text
  */
  public IEnumerator FadeTextToFullAlpha(float t, Text i)
  {
    i.color = new Color(i.color.r, i.color.g, i.color.b, 0);
    while (i.color.a < 1.0f)
    {
      i.color = new Color(i.color.r, i.color.g, i.color.b, i.color.a + (Time.deltaTime / t));
      yield return null;
    }
  }
}