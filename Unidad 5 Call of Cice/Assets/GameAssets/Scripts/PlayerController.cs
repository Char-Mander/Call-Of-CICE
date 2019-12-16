using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private const float gravity = -9.8f;

    //Variables de movimiento del player
    //Variables accesibles desde Unity
    [SerializeField]
    private float runSpeed;
    [SerializeField]
    private float walkSpeed;
    [SerializeField]
    private float rotationSpeed;
    [SerializeField]
    private float jumpForce;


    //Variables privadas
    private CharacterController cController;
    private Vector3 dirPos;
    private float moveSpeed;
    private float pitch;
    private float yaw;
    private float iniRotationY;
    private float iniMouseY;
    private bool iniRotateMouseY;
    //Stamina
    private bool isRunning=false;
    private bool canRunAgain = true;
    private bool isWalking = false;
    private Stamina stamina;
    private PlayerSoundsManager soundsManager;

    // Start is called before the first frame update
    private void Start()
    {
        iniRotateMouseY = false;
        cController = GetComponent<CharacterController>();
        //Stamina asignation
        stamina = GetComponent<Stamina>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        iniRotationY = transform.rotation.eulerAngles.y;
        soundsManager = GetComponent<PlayerSoundsManager>();
    }

    // Update is called once per frame
    private void Update()
    {
        PlayerVelocity();
        JumpaAndMovePlayer();
        RotatePlayer();
        WeaponInputs();
    }

    //Función que hace que el player se mueva y salte con las ArrowKeys
    private void JumpaAndMovePlayer()
    {
        if (cController.isGrounded) {
            dirPos = transform.forward * Input.GetAxis("Vertical") + transform.right * Input.GetAxis("Horizontal");

            if (Input.GetKeyDown(KeyCode.Space))
            {
                if((isRunning || isWalking) && cController.velocity.magnitude == 0)
                {
                    isRunning = false;
                    isWalking = false;
                    soundsManager.StopSound();
                }
                dirPos.y = jumpForce;
                soundsManager.ManageJumpSound();
            }

        }
       dirPos.y += gravity * Time.deltaTime;
       cController.Move(dirPos * moveSpeed * Time.deltaTime);
        
    }

    //Función que controla la velocidad del player al moverse
    private void PlayerVelocity()
    {

        ManagePlayerStates();
        ModifyStamina();
        moveSpeed = isRunning ? runSpeed : walkSpeed;
    }

    //Función que hace que el player y la cámara roten con el ratón
    private void RotatePlayer()
    {
        if (iniRotateMouseY)
        {
        pitch -= rotationSpeed * Input.GetAxis("Mouse Y");
        yaw += rotationSpeed * Input.GetAxis("Mouse X");

        pitch = Mathf.Clamp(pitch, -60, 60);

        Camera.main.transform.localRotation = Quaternion.Euler(pitch + iniMouseY, 0, 0);
        transform.rotation = Quaternion.Euler(0, yaw + iniRotationY, 0);
        }
        else if(Input.GetAxis("Mouse Y") != 0)
        {
            iniMouseY = Input.GetAxis("Mouse Y");
            iniRotateMouseY = true;
        }
    }
                                                                                                                                                        
    ///Función que hace que el player dispare
    private void WeaponInputs()
    {

        if (Input.GetButton("Fire1") || Input.GetKey(KeyCode.T))
        {
            if (checkTypeOfActiveWeapon() == 1)
            {
                transform.GetComponentInChildren<Weapon>().Shoot();
            }
            else if(checkTypeOfActiveWeapon() == 2)
            {
                if(!transform.GetComponentInChildren<ParticleWeapon>().IsShooting())
                transform.GetComponentInChildren<ParticleWeapon>().setShooting(true);
                transform.GetComponentInChildren<ParticleWeapon>().Shoot();
            }
        }
        else if(Input.GetButtonUp("Fire1") || Input.GetKeyUp(KeyCode.T))
        {
            if (checkTypeOfActiveWeapon() == 2)
            {   if(transform.GetComponentInChildren<ParticleWeapon>().IsShooting())
                transform.GetComponentInChildren<ParticleWeapon>().setShooting(false);
            }
        }

        if (Input.GetMouseButton(1))
        {
            if (checkTypeOfActiveWeapon() == 1)
            {
                transform.GetComponentInChildren<Weapon>().Zoom(true);
            }
            else if(checkTypeOfActiveWeapon() == 2)
            {
                transform.GetComponentInChildren<ParticleWeapon>().Zoom(true);
            }
        }
        else
        {
            if (checkTypeOfActiveWeapon() == 1)
            {
                transform.GetComponentInChildren<Weapon>().Zoom(false);
            }
            else if (checkTypeOfActiveWeapon() == 2)
            {
                transform.GetComponentInChildren<ParticleWeapon>().Zoom(false);
            }
        }
    }


    //Funciones que modifican la stamina del player
    void ModifyStamina()
    {
        if (isRunning)
        {
            stamina.LoseStamina(Time.deltaTime*10);
            if (stamina.hasNoStamina())
            {
                isRunning = false;
                StartCoroutine(RegenerationWaitTime());
            }
        }
        else if (!stamina.hasMaxStamina())
        {
            stamina.GainStamina(Time.deltaTime * 5);
        }
    }

    IEnumerator RegenerationWaitTime()
    {
        yield return new WaitForSeconds(2f);
    }

    void ManagePlayerStates()
    {        
        //Cuando empieza a correr
        if (cController.velocity.magnitude != 0 && Input.GetKey(KeyCode.LeftShift) && !stamina.hasNoStamina() && !isRunning && cController.isGrounded && canRunAgain)
        {
            isRunning = true;
            soundsManager.ManageRunSound();
        }

        //Cuando para de correr
        if (((isRunning && Input.GetKeyUp(KeyCode.LeftShift)) || stamina.hasNoStamina()))
        {
            //Si pasa de correr a andar
            if (isRunning && cController.velocity.magnitude != 0 && !isWalking && cController.isGrounded)
            {
                isWalking = true;
                soundsManager.ManageWalkSound();
            }
            else if (cController.velocity.magnitude != 0)
            {
                isWalking = false;
                soundsManager.StopSound();
            }
            isRunning = false;
        }
        else
        {
            //Cuando pasa de estar quieto a estar andando
            if (!isWalking && cController.velocity.magnitude != 0 && cController.isGrounded)
            {
                isWalking = true;
                soundsManager.ManageWalkSound();
            }
            //Cuando pasa de estar andando a estar completamente quieto
            else if (cController.velocity.magnitude == 0 && isWalking)
            {
                isWalking = false;
                isRunning = false;
                soundsManager.StopSound();
            }
        }
    }

    int checkTypeOfActiveWeapon()
    {int res = 0;

        if (transform.GetComponentInChildren<Weapon>()) res = 1;
        else if(transform.GetComponentInChildren<ParticleWeapon>()) res = 2;
        return res;
    }

    public bool CanRunAgain() { return this.canRunAgain; }
}
