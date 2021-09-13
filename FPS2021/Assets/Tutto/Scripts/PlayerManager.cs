using UnityEngine;
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

    int m_hitPoint = 0;

    public bool interact = false;

    Rigidbody m_rigidbody;
    inputManager m_inputManager;

    public bool death = false;



    void Awake()
    {
        m_inputManager = GameObject.Find("inputManager").GetComponent<inputManager>();

        m_rigidbody = GetComponent<Rigidbody>();
    }


    void Update()
    {
        switch (m_state)
        {
            //Move
            case PlayerManager.State.Move:

                //Moviment
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
                            m_rigidbody.velocity = new Vector3(speed * Time.fixedDeltaTime, m_rigidbody.velocity.y, 0.0f);
                        }

                        //Left
                        else if (m_inputManager.runLeft)
                        {
                            m_rigidbody.velocity = new Vector3(-speed * Time.fixedDeltaTime, m_rigidbody.velocity.y, 0.0f);
                        }

                        //Up
                        else if (m_inputManager.runUp)
                        {
                            m_rigidbody.velocity = new Vector3(0.0f, m_rigidbody.velocity.y, speed * Time.fixedDeltaTime);
                        }

                        //Down
                        else if (m_inputManager.runDown)
                        {
                            m_rigidbody.velocity = new Vector3(0.0f, m_rigidbody.velocity.y, -speed * Time.fixedDeltaTime);
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
                            m_rigidbody.velocity = new Vector3(speed * Time.fixedDeltaTime, m_rigidbody.velocity.y, 0.0f);
                        }

                        //Left
                        if (m_inputManager.runLeft)
                        {
                            m_rigidbody.velocity = new Vector3(-speed * Time.fixedDeltaTime, m_rigidbody.velocity.y, 0.0f);
                        }

                        //Up
                        if (m_inputManager.runUp)
                        {
                            m_rigidbody.velocity = new Vector3(0.0f, m_rigidbody.velocity.y, speed * Time.fixedDeltaTime);
                        }

                        //Down
                        if (m_inputManager.runDown)
                        {
                            m_rigidbody.velocity = new Vector3(0.0f, m_rigidbody.velocity.y, -speed * Time.fixedDeltaTime);
                        }
                    }
                    #endregion
                }
                else
                {
                    m_rigidbody.velocity = new Vector3(0.0f, 0.0f, 0.0f);
                }
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
