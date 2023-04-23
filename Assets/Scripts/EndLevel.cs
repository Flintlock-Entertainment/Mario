using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndLevel : KillPlayer
{
    public override void OnTriggerEnter2D(Collider2D other)
    {
        base.OnTriggerEnter2D(other);
        GameManager.Instance.ChangeState(GameManager.Instance.levelNumber + 1);
    }
}
