using UnityEngine;
using System.Collections;

[RequireComponent (typeof (Controller2D))]
public class Player : MonoBehaviour {

    public float jumpHeight = 4;
    //time that the p layer takes to reach the heighest point in the jump
    public float timeToJumpApex = .4f;
    float accelerationTimeAirBorne = .2f;
    float acccelerationTimeGrounded = .1f;
    float moveSpeed  = 6;


    float gravity;
    float jumpVelocity;
    float velocityXSmoothing;
    Vector3 velocity;

	Controller2D controller;

	void Start() {
		controller = GetComponent<Controller2D> ();

        gravity = -(2* jumpHeight)/Mathf.Pow(timeToJumpApex, 2);
        jumpVelocity =Mathf.Abs(gravity) * timeToJumpApex;
        print ("Gravity : " + gravity + " JumpVelocity: " + jumpVelocity);
	}

    void Update(){

        if(controller.collisions.above || controller.collisions.below){
            velocity.y = 0;
        }

        Vector2 input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

        if(Input.GetKeyDown(KeyCode.Space) && controller.collisions.below){
            velocity.y = jumpVelocity;
        }

        float targetVelocityX = input.x * moveSpeed;
        velocity.x = Mathf.SmoothDamp(velocity.x, targetVelocityX, ref velocityXSmoothing, (controller.collisions.below)? acccelerationTimeGrounded: accelerationTimeAirBorne);
        //applies the gravity
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }

}
