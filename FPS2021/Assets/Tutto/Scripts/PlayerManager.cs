using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PlayerManager : MonoBehaviour
{
    enum State
    {
        Move,
        Attack,
        Hit,
        Death
    }

    PlayerManager.State m_state = PlayerManager.State.Move;

    //public EnemyManager enemyManager;
    //public SceneFader sceneFader;

    [Header("Movement")]
    public float walkspeed = 300.0f;
    public float runspeed = 350.0f;
    float speed = 300.0f;

    [Header("Shooting")]
    public float shotRatio = 0.2f;

    [Header("Mouse Settings")]
    public float mouseSensitivity = 10.0f;

    [Header("Camera Settings")]
    public Transform mainCamera;

    int m_hitPoint = 0;

    public bool interact = false;

    Rigidbody m_rigidbody;
    inputManager m_inputManager;

    public bool death = false;

    float m_RotX = 0.0f;

    void Awake()
    {
        Cursor.lockState = CursorLockMode.Locked;

        m_inputManager = GameObject.Find("inputManager").GetComponent<inputManager>();

        m_rigidbody = GetComponent<Rigidbody>();
    }


    void Update()
    {
        // Rotazione corpo
        transform.Rotate(0.0f, Input.GetAxis("Mouse X") * mouseSensitivity, 0.0f);
        
        // Rotazione testa
        m_RotX = m_RotX + -Input.GetAxis("Mouse Y") * mouseSensitivity;
        m_RotX = Mathf.Clamp(m_RotX, -50.0f, 50.0f);
        mainCamera.localEulerAngles = new Vector3(m_RotX, 0.0f, 0.0f);

        #region StateMachine
        switch (m_state)
        {
            //Move
            case PlayerManager.State.Move:

                //Moviment
                #region Moviment
                if (m_inputManager.runLeft || m_inputManager.runRight || m_inputManager.runUp || m_inputManager.runDown)
                {
                    #region Run
                    if (!m_inputManager.walk)
                    {
                        //m_animator.Play("Run");
                        speed = runspeed;

                        //Right
                        if (m_inputManager.runRight)
                        {
                            m_rigidbody.velocity += transform.TransformDirection(Vector3.right) * speed * Time.deltaTime;
                        }

                        //Left
                        else if (m_inputManager.runLeft)
                        {
                            m_rigidbody.velocity += transform.TransformDirection(Vector3.left) * speed * Time.deltaTime;
                        }

                        //Forward
                        else if (m_inputManager.runForward)
                        {
                            m_rigidbody.velocity += transform.TransformDirection(Vector3.forward) * speed * Time.deltaTime;
                        }

                        //Back
                        else if (m_inputManager.runBack)
                        {
                            m_rigidbody.velocity += transform.TransformDirection(Vector3.back) * speed * Time.deltaTime;
                        }
                    }
                    #endregion

                    #region Walk
                    else
                    {
                        //m_animator.Play("Walk");
                        speed = walkspeed;

                        //Right
                        if (m_inputManager.runRight)
                        {
                            m_rigidbody.velocity += transform.TransformDirection(Vector3.right) * speed * Time.deltaTime;
                        }

                        //Left
                        if (m_inputManager.runLeft)
                        {
                            m_rigidbody.velocity += transform.TransformDirection(Vector3.left) * speed * Time.deltaTime;
                        }

                        //Forward
                        if (m_inputManager.runForward)
                        {
                            m_rigidbody.velocity += transform.TransformDirection(Vector3.forward) * speed * Time.deltaTime;
                        }

                        //Back
                        if (m_inputManager.runBack)
                        {
                            m_rigidbody.velocity += transform.TransformDirection(Vector3.back) * speed * Time.deltaTime;
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
                if (m_inputManager.attack)
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

                death = true;

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
