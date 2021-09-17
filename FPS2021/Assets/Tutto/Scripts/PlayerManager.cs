using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerManager : MonoBehaviour
{
    #region VarStateMachine
    enum State
    {
        Move,
        Attack,
        Hit,
        Death
    }

    PlayerManager.State m_state = PlayerManager.State.Move;
    #endregion

    //Variable
    #region Variable

    //Header
    #region Header
    [Header("Movement")]
    public float walkspeed = 300.0f;
    public float crouchspeed = 150.0f;
    public float runspeed = 350.0f;
    public int jumpForce = 100;
    float m_speed = 300.0f;

    [Header("Shooting")]
    public float shotRatio = 0.2f;

    [Header("Mouse Settings")]
    public float mouseSensitivity = 10.0f;

    [Header("Camera Settings")]
    public Transform mainCamera;
    float m_RotX = 0.0f;
    #endregion

    //Int
    #region Int
    //int m_hitPoint = 0;
    int layerMask = 1 << 6;
    #endregion

    //Float
    #region Float
    #endregion

    //Bool
    #region Bool
    bool m_death = false;
    bool m_crouch = false;
    #endregion

    //Vector
    #region Vector3

    #endregion


    public float speed;
    public float speedPerSecond;
    public Vector3 oldPosition;

    Rigidbody m_rigidbody;
    CapsuleCollider m_capsuleCollider;
    inputManager m_inputManager;
    //public EnemyManager enemyManager;
    //public SceneFader sceneFader;
    #endregion

    void Awake()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        m_inputManager = GameObject.Find("inputManager").GetComponent<inputManager>();

        m_rigidbody = GetComponent<Rigidbody>();
        m_capsuleCollider = GetComponent<CapsuleCollider>();

        m_speed = runspeed;
    }


    void Update()
    {
        #region SpeedDisplay
        /*speed = Vector3.Distance(oldPosition, transform.position);
        speedPerSecond = Vector3.Distance(oldPosition, transform.position) / Time.deltaTime;
        oldPosition = transform.position;
        Debug.Log("Speed: " + speed.ToString("F2"));*/
        #endregion

        Debug.Log(m_speed);

        #region SpeedRegolation
        Speed();
        #endregion

        #region Rotation
        // Body Rotation
        transform.Rotate(0.0f, Input.GetAxis("Mouse X") * mouseSensitivity, 0.0f);
        
        // Head Rotation
        m_RotX = m_RotX + -Input.GetAxis("Mouse Y") * mouseSensitivity;
        m_RotX = Mathf.Clamp(m_RotX, -50.0f, 50.0f);
        mainCamera.localEulerAngles = new Vector3(m_RotX, 0.0f, 0.0f);
        #endregion

        #region Jump

        if (Physics.Raycast(transform.position, Vector3.down, 2.1f, layerMask) && m_inputManager.jump)
        {
            m_rigidbody.AddForce(transform.up * jumpForce);

            Debug.DrawRay(transform.position, Vector3.down, Color.blue);
            Debug.Log("Ground");
        }
        #endregion

        #region Crouch
        if (!m_crouch && m_inputManager.crouch)
        {
            //m_capsuleCollider =;
            mainCamera.position = new Vector3(mainCamera.position.x, mainCamera.position.y * 0.5f, mainCamera.position.z);

            m_crouch = true;
        }
        else if (m_crouch && !m_inputManager.crouch)
        {
            //
            mainCamera.position = new Vector3(mainCamera.position.x, mainCamera.position.y * 2f, mainCamera.position.z);

            m_crouch = false;
        }
        #endregion

        #region StateMachine
        switch (m_state)
        {
            //Move
            case PlayerManager.State.Move:

            //Moviment
            #region Moviment
            if (m_inputManager.runLeft || m_inputManager.runRight || m_inputManager.runForward || m_inputManager.runBack)
            {
                #region Run
                if (!m_inputManager.walk)
                {
                    //m_animator.Play("Run");

                    //Right
                    if (m_inputManager.runRight)
                    {
                        m_rigidbody.velocity = transform.TransformDirection(Vector3.right) * m_speed * Time.deltaTime;
                    }

                    //Left
                    else if (m_inputManager.runLeft)
                    {
                        m_rigidbody.velocity = transform.TransformDirection(Vector3.left) * m_speed * Time.deltaTime;
                    }

                    //Forward
                    else if (m_inputManager.runForward)
                    {
                        m_rigidbody.velocity = transform.TransformDirection(Vector3.forward) * m_speed * Time.deltaTime;
                    }

                    //Back
                    else if (m_inputManager.runBack)
                    {
                        m_rigidbody.velocity = transform.TransformDirection(Vector3.back) * m_speed * Time.deltaTime;
                    }
                }
                #endregion

                #region Walk
                else
                {
                    //m_animator.Play("Walk");

                    //Right
                    if (m_inputManager.runRight)
                    {
                        m_rigidbody.velocity = transform.TransformDirection(Vector3.right) * m_speed * Time.deltaTime;
                    }

                    //Left
                    if (m_inputManager.runLeft)
                    {
                        m_rigidbody.velocity = transform.TransformDirection(Vector3.left) * m_speed * Time.deltaTime;
                    }

                    //Forward
                    if (m_inputManager.runForward)
                    {
                        m_rigidbody.velocity = transform.TransformDirection(Vector3.forward) * m_speed * Time.deltaTime;
                    }

                    //Back
                    if (m_inputManager.runBack)
                    {
                        m_rigidbody.velocity = transform.TransformDirection(Vector3.back) * m_speed * Time.deltaTime;
                    }
                }
                #endregion
            }
            else
            {
                m_rigidbody.velocity = new Vector3(0.0f, 0.0f, 0.0f);
            }
            #endregion

            //Attack
            #region Attack
            /*if (m_inputManager.attack)
            {
                //m_animator.Play("Attack");
                m_state = PlayerManager.State.Attack;
            }
            break;

           
        //Attack
        case PlayerManager.State.Attack:

            m_rigidbody.velocity = new Vector3(0.0f, 0.0f, 0.0f);

            /*if (m_animator.GetCurrentAnimatorStateInfo(0).IsName("Attack") && m_animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f)
            {
                m_state = PlayerManager.State.Move;
            }*/

            break;
            #endregion

            //Hit
            #region Hit
            case PlayerManager.State.Hit:                 

                /*if(m_animator.GetCurrentAnimatorStateInfo(0).IsName("Hit") && m_animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f)
                {
                    m_state = PlayerManager.State.Move;
                }*/

                break;
            #endregion

            //Death
            #region Death
            case PlayerManager.State.Death:

                m_death = true;

                SceneManager.LoadScene("SampleScene");

                /*if (m_animator.GetCurrentAnimatorStateInfo(0).IsName("Death") && m_animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f)
                {
                    sceneFader.FadeTo(levelToLoad);
                }
                */
                break;
                #endregion
        }
        #endregion
    }

    void FixedUpdate()
    {
        #region Gravity
        m_rigidbody.AddForce(Physics.gravity * (m_rigidbody.mass * m_rigidbody.mass));
        #endregion
    }

    void Speed()
    {
        if(m_inputManager.moviment && !m_inputManager.walk && !m_inputManager.crouch)
        {
            m_speed = runspeed;
        }
        else if (!m_inputManager.moviment && m_inputManager.walk && !m_inputManager.crouch)
        {
            m_speed = walkspeed;
        }
        else if(!m_inputManager.moviment && !m_inputManager.walk && m_inputManager.crouch)
        {
            m_speed = crouchspeed;
        }


    }

    /*void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            if (m_enemyManager != null) { m_hitPoint += m_enemyManager.damage;}            

            if (m_hitPoint < m_healthManager.numOfHearts)
            {
                m_animator.Play("Hit");

                if (healtBar != null) { healtBar.SetHealth(m_healthManager.Health); }                
                m_healthManager.Health -= m_enemyManager.damage;

                m_state = PlayerManager.State.Hit;
            }
            else if (m_hitPoint >= m_healthManager.numOfHearts)
            {
                m_animator.Play("Death");
                m_state = PlayerManager.State.Death;
            }
        }
    }*/
}
