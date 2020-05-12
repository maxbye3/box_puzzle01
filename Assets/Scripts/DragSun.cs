using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class DragSun : MonoBehaviour
{

  public GameObject islandDaySound;
  public GameObject islandNightSound;
  public GameObject launchFireworkNoise;
  public GameObject explodeFireworkNoise;
  public GameObject fireworks;
  public GameObject ending;
  public GameObject gateway;
  public GameObject sun;
  public GameObject fakeLight;
  private Vector3 screenPoint;
  private Vector3 offset;

  private Vector3 initialPos;


  private Vector3 curScreenPoint;
  private Vector3 curPosition;
  private bool mouseUp;
  private float intensityValue;

  // Start is called before the first frame update
  void Start()
  {
    //   Debug.Log("The sun: " + transform.rotation.eulerAngles);
    initialPos = transform.position;
  }

  void OnMouseDown()
  {
    screenPoint = Camera.main.WorldToScreenPoint(transform.position);
    offset = transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z));
  }

  void OnMouseDrag()
  {
    mouseUp = false;
    curScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z);
    curPosition = Camera.main.ScreenToWorldPoint(curScreenPoint) + offset;
    sun.transform.rotation = Quaternion.Euler(curPosition);
    // Debug.Log(curPosition.x);

    transform.position = curPosition;
    intensityValue = 0;

  }

    void OnMouseUp()
  {
      mouseUp = true;
  }

  void Update()
  {
    // if (Input.GetMouseButtonDown(0))
    //   mouseUp = false;

    // if (Input.GetMouseButtonUp(0))

    float verticalChange = initialPos.y - curPosition.y;
    float lightIntensity = 2 * curPosition.y / (initialPos.y);

    if (verticalChange > 400)
    { // you can make it bright again if you keep dragging
      sun.transform.rotation = Quaternion.Euler(320, 0, 0);
    }
    else if (
    curPosition.y != 0 // initial      
    && verticalChange > -1)
    {
      fakeLight.GetComponent<Light>().intensity = lightIntensity;
      // Debug.Log("lightIntensity: " + lightIntensity);
      sun.transform.rotation = Quaternion.Euler(160 - (verticalChange * 0.78f), 0, 0);
    }
    else
    {
      sun.transform.rotation = Quaternion.Euler(160, 0, 0);
    }



    if (verticalChange > 225 && mouseUp)
    {
      // int ending
      ending.SetActive(true);
      // int gateway
      gateway.SetActive(true);
      // launch fireworks
      fireworks.SetActive(true);
      // turn off island day sound
      islandDaySound.SetActive(false);
      // turn on island night sound
      islandNightSound.SetActive(true);


      // launch firework noises
      StartCoroutine(playSoundAfterSeconds(launchFireworkNoise, 0.5f));
      StartCoroutine(playSoundAfterSeconds(explodeFireworkNoise, 1.5f));



      verticalChange = 450; // make sure it is night time
      mouseUp = false; // only call the above once
    }
    else if (verticalChange > 100 && mouseUp)
    {
      // go back to y 210
      // Debug.Log(transform.position.y);
      if (transform.position.y < 210)
      {
        transform.position = new Vector3(transform.position.x, transform.position.y + 1, transform.position.z);
      }

      sun.transform.rotation = Quaternion.Euler(370, 0, 0);

      if (intensityValue < 2)
        intensityValue += 0.05f;
      fakeLight.GetComponent<Light>().intensity = intensityValue;
    }
  }

  IEnumerator playSoundAfterSeconds(GameObject sound, float t)
  {
    yield return new WaitForSeconds(t);
    sound.SetActive(true);
  }


}
