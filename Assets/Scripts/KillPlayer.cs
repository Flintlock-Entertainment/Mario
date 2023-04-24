using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillPlayer : MonoBehaviour
{
    [SerializeField] protected string triggeringTag;
    [SerializeField] GameObject spawnPoint = null;
    public virtual void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == triggeringTag)
        {
            other.transform.position = spawnPoint.transform.position;
        }
    }
}
