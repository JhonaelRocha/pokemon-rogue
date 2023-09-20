using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Auto_destroy : MonoBehaviour
{
    public float seconds;
    IEnumerator timeToExecute(float seconds, System.Action action){
        yield return new WaitForSeconds(seconds);
        action.Invoke();
    }

    void Start(){
        StartCoroutine(timeToExecute(seconds, () => Destroy(this.gameObject)));
    }
}
