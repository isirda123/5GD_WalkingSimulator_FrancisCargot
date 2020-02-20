using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlySpiderWeb : MonoBehaviour
{
    public Transform[] positionTarget;

    public FMODUnity.StudioEventEmitter fly_event;

    IEnumerator Start()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(3f, 10f));

            fly_event.transform.position = positionTarget[Random.Range(0, positionTarget.Length)].position;
            fly_event.Play();
        }
    }
}
