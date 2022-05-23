using UnityEngine;

public class MovementController: MonoBehaviour {
  public float gravitationalConstant;
  public float autonomousVerticalAcceleration; // rate at which the vehicle can propel itself up
  public float maximumUpwardVelocity;
  private float currentUpwardVelocity;

  // Start is called before the first frame update
  void Start() {
    this.currentUpwardVelocity = 0;
  }

  // Update is called once per frame
  void Update() {
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

    if (Input.GetKey(KeyCode.Space)
        && this.currentUpwardVelocity < this.maximumUpwardVelocity) {
      this.currentUpwardVelocity += this.autonomousVerticalAcceleration;
    } else if (this.transform.position.y > 0) {
      this.currentUpwardVelocity -= this.gravitationalConstant;
    } else {
      this.currentUpwardVelocity = 0;
    }

  }
}
