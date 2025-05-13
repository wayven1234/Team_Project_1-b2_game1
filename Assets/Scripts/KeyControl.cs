using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class KeyControl : MonoBehaviour
{
    // 
    public GameObject wallobject;

    // 
    public GameObject EndingSpot;

    // 
    public GameObject Keyprefab;

    public Transform Keyspawnpoint1;
    public Transform Keyspawnpoint2;

    // 
    public void randomkey()
    {
        // 
        int randomindex = Random.Range(0, 2);
        Transform chosenPoint = (randomindex == 0) ? Keyspawnpoint1 : Keyspawnpoint2;
        Instantiate(Keyprefab, chosenPoint.position,chosenPoint.rotation);
    }

    void Start()
    {
        EndingSpot.SetActive(false);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Key"))
        {
            wallobject.SetActive(false);
            EndingSpot.SetActive(true);

            Debug.Log("ø≠ºË∏¶ »πµÊ«ﬂΩ¿¥œ¥Ÿ");
            Destroy(collision.gameObject);
        }
    }
}
