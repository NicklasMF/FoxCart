using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

	public Transform path;
	public float maxSteerAngle = 45f;
	List<Transform> nodes;
	int currentNode = 0;
	float maxMotorTorque = 80f;
	float maxBrakeTorque = 150f;
	float currentSpeed;
	float maxSpeed = 100f;
	bool isBraking = false;

	public WheelCollider wheelFR;
	public WheelCollider wheelFL;
	public WheelCollider wheelRR;
	public WheelCollider wheelRL;

	void Start() {
		Transform[] pathTransform = path.GetComponentsInChildren<Transform>();
		nodes = new List<Transform>();

		for (int i = 0; i < pathTransform.Length; i++) {
			if (pathTransform[i] != path.transform) {
				nodes.Add(pathTransform[i]);
			}
		}
	}

	void FixedUpdate() {
		ApplySteer();
		Drive();
		Braking();
		CheckWaypointDistance();
	}

	void ApplySteer() {
		Vector3 relativeVector = transform.InverseTransformPoint(nodes[currentNode].position);
		float newSteer = (relativeVector.x / relativeVector.magnitude) * maxSteerAngle;
		wheelFL.steerAngle = newSteer;
		wheelFR.steerAngle = newSteer;
	}

	void Drive() {
		currentSpeed = 2 * Mathf.PI * wheelFL.radius * wheelFL.rpm * 60 / 100;

		if (currentNode < maxSpeed && !isBraking) {
			wheelFL.motorTorque = maxMotorTorque;
			wheelFR.motorTorque = maxMotorTorque;
		} else {
			wheelFL.motorTorque = 0;
			wheelFR.motorTorque = 0;
		}
	}

	void CheckWaypointDistance() {
		if (Vector3.Distance(transform.position, nodes[currentNode].position) < 1f) {
			if (currentNode == nodes.Count - 1) {
				currentNode = 0;
			} else {
				currentNode++;
			}
		}
	}

	void Braking() {
		if (isBraking) {
			wheelRL.brakeTorque = maxBrakeTorque;
			wheelRR.brakeTorque = maxBrakeTorque;
		} else {
			wheelRL.brakeTorque = maxBrakeTorque;
			wheelRR.brakeTorque = maxBrakeTorque;
		}
	}
}
