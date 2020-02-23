using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPlayerPos : MonoBehaviour
{
    public Enemy_Controller en_con;

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.isTrigger != true && col.CompareTag("Player"))
        {
            en_con.target = col.transform.position;
        }
    }
}
