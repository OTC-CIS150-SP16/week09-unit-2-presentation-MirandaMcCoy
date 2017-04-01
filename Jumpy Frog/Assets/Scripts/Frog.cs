using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Frog : MonoBehaviour {
    // VARIABLES

    // This game object is a singleton
	public static GameObject S;
    
    // Constants that hold the original values to return to or use repeatedly
	public const float origMaxHeight = -10F, origMovementSpeed = .5F, maxMaxHeight = 17F;

    // Max, Min, and hopping (basic movement) heights for Austin as well as the movement of him, the logs, and the base for the trees
	public static float maxHeight = origMaxHeight, hoppingHeight = origMaxHeight, movementSpeed = origMovementSpeed, minHeight = -14F;

    // Direction of Austin's movement as well as a bool to tell if he needs to hover (over logs)
	public static string dir = "Up";
	public static bool hover = false;



    // START AND UPDATE

	// Initilizes object as a singleton
	void Start () {
		S = this.gameObject;
	}
	


	// Update is called once per frame
	void Update () {
		Vector3 pos = S.transform.position;

        // Basic movement logic

		// Determines if the frog has to go up or down
		if (pos.y <= minHeight) {
			dir = "Up";
		} else if (pos.y >= maxHeight) {
			dir = "Down";
		}

		// Moves the frog up or down
		if (dir == "Up") {
			pos.y += movementSpeed;
		} else if (dir == "Down"){
			if (!hover) {
				pos.y -= movementSpeed;
			} else {
				dir = "Up";
			}
		}

		// Sets the positions of the frog
		S.transform.position = pos;
	}



    // ACTIONS

    // Changes the current maxHeight so the frog will go higher
    public static void BigJump(float height){
		maxHeight = height;
	}



    // If the frog collides with a log, game over and movement stops
    void OnCollisionEnter(Collision coll){
		GameObject collidedWith = coll.gameObject;

		if (collidedWith.tag == "Log"){
			movementSpeed = 0;
			JumpyFrog.gameOver = true;
		}
	}
}
