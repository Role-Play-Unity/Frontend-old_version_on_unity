using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

[RequireComponent(typeof(CharacterMotor))]
[RequireComponent(typeof(CharacterSetup))]
public class MasterCharacter : NetworkBehaviour
{
    [SerializeField]
    [SyncVar]
    public float spead = 3f;
    
    [SerializeField]
    public float mouseSensitivityX = 3f;
    [SerializeField]
    public float mouseSensitivityY = 3f;

    private CharacterMotor motor;
    // Start is called before the first frame update
    void Start()
    {
        motor = GetComponent<CharacterMotor>();
    }

    // Update is called once per frame
    void Update()
    {
        // Calculer la velocite do movement de notre joueur
        float xMov = Input.GetAxisRaw("Horizontal");
        float zMov = Input.GetAxisRaw("Vertical");

        Vector3 moveHorizontal = transform.right * xMov;
        Vector3 moveVertical = transform.forward * zMov;

        Vector3 velocity = (moveHorizontal + moveVertical).normalized * spead;

        motor.Move(velocity);

        // On calcule la rotation du joueur en un Vector3
        float yRot = Input.GetAxisRaw("Mouse X");

        Vector3 rotation = new Vector3(0, yRot, 0) * mouseSensitivityX;

        motor.Rotate(rotation);

        // On calcule la rotation du camera en un Vector3
        float xRot = Input.GetAxisRaw("Mouse Y");

        float cameraRotation = xRot * mouseSensitivityY;

        motor.RotateCamera(cameraRotation);
    }
}


