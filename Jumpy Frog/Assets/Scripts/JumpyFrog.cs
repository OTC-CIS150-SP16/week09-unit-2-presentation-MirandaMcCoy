using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpyFrog : MonoBehaviour {
    // ** THIS IS THE MAIN SCRIPT FOR THE GAME ** //



    // VARIABLES

    // GUI Texts
	public GUIText gtScore, gtGO, gtScorePlus;

    // Floats for the score, number of logs jumped, height of the log on the screen, height jumped over the log (during jump), and score before what is added
	public static float score = 0, logsJumped = 0, heightOfLog, heightJOL, scoreBefore;

    // Bools for if the game is over and whether the number of points added should be displayed or not
	public static bool gameOver = false, showScorePlus = false;



    // UPDATE (Start not used)

	// Update sets a current log position and size for reference, calls to show the current score, tests if the log is close enough
      // that the frog needs to jump, shows the score that is added if need be, sets up for Austin to fall after the log passes,
      // and handles game over and if the user wants to play again
	void Update () {
        // Sets log's current size and pos
        Vector3 logPos = Log.S.transform.position;
        Vector3 logSize = Log.S.transform.localScale;

        // Shows the current score
		ShowGT ();

        // Determines when the frog needs to "jump" (Changing height, hovering, falling after log)
          // If the user did not charge the jump bar at all, Austin does not jump
          // Jump and hover
        if (!JumpBar.notPressed)
        {
            if (logPos.x <= -10 && logSize.x >= 3)
            {
                Frog.BigJump(JumpBar.jumpHeight);
                Frog.hover = true;
            }
            else if (logPos.x <= -15)
            {
                Frog.BigJump(JumpBar.jumpHeight);
                Frog.hover = true;
            }
        }

		  // Shows the score added
		if (showScorePlus) {
			if (!gameOver) {
				gtScorePlus.text = "+" + (score - scoreBefore);
			}
		} else {
			gtScorePlus.text = " ";
		}

		  // Fall after log
		if (logPos.x <= -43 && logSize.x != 3) {
			Frog.hover = false;
        } else if (logPos.x <= -50) {
			Frog.hover = false;
        }

        // GAME OVER

        // If the game is over, display this text
        if (gameOver == true)
        {
            gtGO.text = "Game Over\n\nFinal score: " + score + "\nLogs jumped: " + logsJumped + "\n\nPress the enter\nkey to continue";
            gtScore.text = " ";
        }

        // GAME OVER RESET

        // If user wants to continue, press the enter/return key. Resets several things
        if (gameOver == true && Input.GetKeyDown("return"))
        {
            gameOver = false;
            gtGO.text = " ";

            logPos.x = 70;
            Log.S.transform.position = logPos;
            Log.ChangeSize();

            Frog.hover = false;
            Frog.maxHeight = Frog.origMaxHeight;
            Frog.movementSpeed = Frog.origMovementSpeed;

            score = 0;
            logsJumped = 0;

            JumpBar.notPressed = true;
            JumpBar.ResetBar();
        }
    }



    // ACTIONS

	// Updates score display
	void ShowGT(){
		gtScore.text = "Score: " + score + "\nLogs jumped: " + logsJumped;
	}



	// Determines score by subtracting how high over the log the frog jumped.  If too high,
      // no points are awarded.  Also adds to number of logs jumped
	public static void UpdateScore(){
        Vector3 logSize = Log.S.transform.localScale;

        logsJumped++;

        // Log is approximately 8 units times its Y scale
		heightOfLog = logSize.y * 8;

		heightJOL = (Frog.maxHeight + 15) - (heightOfLog / 2);

		scoreBefore = score;

		double predictedScore = (int)(100 - (heightJOL * 3));

		if (predictedScore <= 50) {
			score += 0;
		} else if (predictedScore >= 92) {
			score += 100;
		} else {
			score += (float)predictedScore;
		}

        if (gameOver) {
			score = 0;
			logsJumped = 0;
		}
	}
}
