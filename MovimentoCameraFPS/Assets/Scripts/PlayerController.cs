using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
  public bool enableMouse;
  [Header("PlayerConfig")]
  public string PlayerName;
  public int Life;
  public float speed;
  public float runSpeed;
  public float sensibility;
  [Header("Imports")]
  public Camera cam;


  private Rigidbody rb;
  private float realSpeed;
  private Vector3 velocity;
  private Vector3 rotation;
  private Vector3 camRotation;
  private float rotCam;
  // Start is called before the first frame update
  void Start()
  {
    rb = GetComponent<Rigidbody>();

  }

  // Update is called once per frame
  void Update()
  {

    //controle de movimentação do player
    float _xMov = Input.GetAxisRaw("Horizontal");
    float _yMov = Input.GetAxisRaw("Vertical");

    if((Input.GetButton("Run") == true) && (_xMov == 0) && (_yMov == 1)){
      realSpeed = runSpeed;
    }else{
      realSpeed = speed;
    }
    Vector3 _MoveHorizontal = transform.right * _xMov;
    Vector3 _MoveVertical = transform.forward * _yMov;
    velocity = (_MoveHorizontal + _MoveVertical).normalized * realSpeed;

    //ativa o mouse ou não
    if(enableMouse == true){
      Cursor.lockState = CursorLockMode.Locked;
      Cursor.visible = false;
    }else{
      Cursor.lockState = CursorLockMode.None;
      Cursor.visible = true;
    }

    //controla rotação da camera
    float _yMouse = Input.GetAxisRaw("Mouse X");
    rotation = new Vector3(0, _yMouse, 0) * sensibility;

    float  _xMouse = Input.GetAxisRaw("Mouse Y");
    camRotation = new Vector3(_xMouse, 0, 0) * sensibility;

  }

  private void FixedUpdate()
  {
    if(enableMouse == true){
      Moviment();
      Rotation();
    }
  }

  void Moviment()
  {
    if(velocity != Vector3.zero){
      rb.MovePosition(rb.position + velocity * Time.deltaTime);
    }
  }

  void Rotation()
  {
    rb.MoveRotation(rb.rotation * Quaternion.Euler(rotation));

    if(cam != null){
      rotCam += camRotation.x;
      rotCam = Mathf.Clamp(rotCam, -80, 80);
      cam.transform.localEulerAngles = new Vector3(-rotCam, 0, 0);
    }
  }
}
