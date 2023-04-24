using System.Collections;
using UnityEngine;

/**
 * This component represents the floor of the game area.
 * When the player hits the floor, it is sent back to the respawn point.
 */
public class Floor : MonoBehaviour
{
    [SerializeField] protected string triggeringTag;
    [SerializeField] float threshold = 1f;
    private void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.tag == triggeringTag)
        {
            other.gameObject.GetComponent<Mover>().isGrounded = true;
            if(GameManager.Instance.levelNumber == Level.Three && other.relativeVelocity.y < -1 * threshold)
            {

                other.gameObject.GetComponent<Mover>().walkForce /= 2;
                StartCoroutine(BrokenLeg(other.gameObject.GetComponent<Mover>()));

            }
        }
            
        
    }
    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.tag == triggeringTag)
            other.gameObject.GetComponent<Mover>().isGrounded = false;
        

    }

    private IEnumerator BrokenLeg(Mover mover)
    {
        yield return new WaitForSeconds(2f);
        mover.walkForce *= 2 ;
     }
}