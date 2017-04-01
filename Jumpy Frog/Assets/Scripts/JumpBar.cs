using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpBar : MonoBehaviour {
    // VARIABLES

    // This object is a singleton
	public static GameObject S;

    // Jump height to be passed to Austin
	public static float jumpHeight = Frog.origMaxHeight, addCounter = 0;

    // Bool to determine if the space bar has been pressed so it can't be pressed again until next jump
	public static bool notPressed = true;

    // The jump bar's original position for resetting
    public static Vector3 origPos;

    

    // START AND UPDATE

	// Initilizes this object as a singleton and sets the original position
	void Start () {
		S = this.gameObject;
        origPos = S.transform.position;
    }
	


	// Update is called once per frame
	void Update () {
		// When user presses space bar, the new height is added to and the jump bar is increased
          // The counter slows down how quickly this happens
		if (Input.GetKey ("space") && notPressed && addCounter % 3 == 0) {
			JumpBar.ChangeHeight();
            addCounter++;
		} else
        {
            addCounter++;
        }

		// When user releases space bar, notPressed = false so they can't press again
		if (Input.GetKeyUp("space")) {
			notPressed = false;
		}
	}



    // ACTIONS

    // Changes the tallness of the jump bar by adding to the Y scale and adjusting the Y position
	public static void ChangeHeight(){
        // Gets current position and scale
		Vector3 pos = S.transform.position;
		Vector3 scale = S.transform.localScale;

        // 28 is the max size of the jump bar.
          // Adds to scale and pos Y.
		if (scale.y < 28) {
			pos.y += .5F;
			scale.y += 1;

            // Adds to Austin's jump height if it's not larger than his max height
            if ((jumpHeight + 1.14) < Frog.maxMaxHeight)
            {
                jumpHeight += 1.14F;
            }
			
		}

        // Sets new pos and scale
		S.transform.position = pos;
		S.transform.localScale = scale;
	}



    // Resets the position and scale of the bar
    public static void ResetBar(){
		jumpHeight = Frog.origMaxHeight;
        
		S.transform.position = origPos;

		Vector3 size = new Vector3 (2F, 1F, 1F);
		S.transform.localScale = size;
	}
}
