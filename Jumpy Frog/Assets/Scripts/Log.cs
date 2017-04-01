using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Log : MonoBehaviour {
    // VARIABLES

    // This object is a singleton
	public static GameObject S;

    // Bool to determine if the log needs to be set back on the left of the screen
      // and have its size randomized
	public bool needsUpdate = true;



    // START AND UPDATE

	// Instantiates this object as a singleton
	void Start () {
		S = this.gameObject;
	}
	


	// Update is called once per frame
	void Update () {
		// Moves the log forward by getting its current position and moving it right at the set speed
		Vector3 pos = S.transform.position;
		pos.x -= Frog.movementSpeed;
		S.transform.position = pos;

		// If log reaches -44, means Austin has cleared the log.  Adds to score, shows the
          // score that was added to, resets the jump bar, and no longer needs an update this round
		if (pos.x <= -44) {
            if (needsUpdate)
            {
                JumpyFrog.showScorePlus = true;
                JumpyFrog.UpdateScore();

                JumpBar.ResetBar();

                needsUpdate = false;

                // Adjusts the speed of the game and adds to Austin's hopping height
                if (Frog.movementSpeed < 1F && JumpyFrog.logsJumped % 2 == 0)
                {
                    Frog.movementSpeed += .25F;
                    Frog.hoppingHeight += 1F;
                }
            }

            Frog.maxHeight = Frog.hoppingHeight;
        }

        // After the log is off the screen, resets the log's position to the left, stops showing the
          // score added that round, changes the log's size, and allows the user to press space to adjust
          // the jump bar again.
		if (pos.x <= -65) {
			needsUpdate = true;
            pos.x = 70;
            S.transform.position = pos;
            ChangeSize();

            JumpyFrog.showScorePlus = false;

			JumpBar.notPressed = true;
        }
	}



    // ACTIONS

    // Chooses a random size of log to throw at the player.
      // Between size 1 and 4
    public static void ChangeSize(){
		Vector3 size = S.transform.localScale;
		size.y = (int)Random.Range(1, 4.999F);

		if (size.y >= 4) {
			size.x = 3;
		} else {
			size.x = size.y;
		}

		S.transform.localScale = size;

		ChangeHeight ((int)size.y);
	}



    // Adjusts the Y position of the log so it's not cutting into the ground
    public static void ChangeHeight(int size){
		Vector3 pos = S.transform.position;
		pos.y = -17;

		if (size == 1) {
			pos.y = -16;
		} else {
			pos.y += (float)1.5 * size;
		}

		S.transform.position = pos;
	}

}
