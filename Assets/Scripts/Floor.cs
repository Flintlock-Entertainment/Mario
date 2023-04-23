using System.Collections;
using UnityEngine;

/**
 * This component represents the floor of the game area.
 * When the player hits the floor, it is sent back to the respawn point.
 */
public class Floor : MonoBehaviour
{
    

    private void OnCollisionEnter2D(Collision2D other)
    {
        Debug.Log("enter");
        other.gameObject.GetComponent<Mover>().isGrounded = true;
           // var controller = other.GetComponent<CharacterController>();
           //if (controller)
           //   {
           // controller.enabled = false;
           //   }
            
          //  StartCoroutine(EnableController(controller));
        
    }
    private void OnCollisionExit2D(Collision2D other)
    {
        Debug.Log("exit");
        other.gameObject.GetComponent<Mover>().isGrounded = false;
        // var controller = other.GetComponent<CharacterController>();
        //if (controller)
        //   {
        // controller.enabled = false;
        //   }

        //  StartCoroutine(EnableController(controller));

    }

    //private IEnumerator EnableController(CharacterController controller)
    //{
    //    yield return new WaitForSeconds(0.5f);
    // controller.enabled = true;
    // }
}