using UnityEngine;

public class MovementController: MonoBehaviour {
  public float gravitationalConstant;
  public float autonomousVerticalAcceleration; // rate at which the vehicle can propel itself up
  public float maximumUpwardVelocity;
  private float currentUpwardVelocity;
  public bool hovering;

  // Start is called before the first frame update
  void Start() {
    this.currentUpwardVelocity = 0;
    this.hovering = false;
    Debug.Log("Controls:\n");
    Debug.Log("\tSpacebar/L: go up/down\n");
    Debug.Log("\tW/S: forwards/backwards\n");
    Debug.Log("\tA/D: turn left/right\n");
    Debug.Log("\tF: toggle hovering\n");
  }

  // Update is called once per frame
  void Update() {
    if (Input.GetKey(KeyCode.F)) {
      this.hovering = !this.hovering;
    }

    if (this.hovering) {
      this.handleHoveringState();
    } else {
      this.handleFallingState();
    }

    /* TODO make flight controls more car-like and more parameterized */
    float speed = 5.0f;
    if (Input.GetKey(KeyCode.W)) {
      this.transform.position += this.transform.forward * Time.deltaTime * speed;
    } else if (Input.GetKey(KeyCode.S)) {
      this.transform.position -= this.transform.forward * Time.deltaTime * speed;
    }
    float rotationSpeed = 20.0f;
    if (Input.GetKey(KeyCode.A)) {
      this.transform.Rotate(0, -rotationSpeed * Time.deltaTime, 0);
    }
    if (Input.GetKey(KeyCode.D)) {
      this.transform.Rotate(0, rotationSpeed * Time.deltaTime, 0);
    }
  }

  void handleHoveringState() {
    if (Input.GetKey(KeyCode.Space) && this.currentUpwardVelocity < this.maximumUpwardVelocity) {
      this.currentUpwardVelocity += this.autonomousVerticalAcceleration;
    } else if (Input.GetKey(KeyCode.L)
               && this.currentUpwardVelocity > -this.maximumUpwardVelocity) {
      this.currentUpwardVelocity -= this.autonomousVerticalAcceleration;
    } else {
      this.currentUpwardVelocity = 0;
    }
    this.adjustVerticalPosition();
  }

  void handleFallingState() {
    if (this.transform.position.y > 0) {
      this.currentUpwardVelocity -= this.gravitationalConstant;
    } else {
      this.currentUpwardVelocity = 0;
    }
    this.adjustVerticalPosition();
  }

  void adjustVerticalPosition() {
    // Pull the vehicle down if it is above the ground
    if (this.transform.position.y >= 0) {
      this.transform.position += new Vector3(
        0, // x axis
        this.currentUpwardVelocity * Time.deltaTime, // y axis
        0 // z axis
      );
    } else {
      this.transform.position = new Vector3(
        this.transform.position.x,
        0,
        this.transform.position.z
      );
    }
  }
}
