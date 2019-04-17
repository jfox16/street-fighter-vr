using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FighterInput : MonoBehaviour
{
    // Start is called before the first frame update
    KeyCode LightPunch;
    KeyCode HeavyPunch;
    KeyCode Kick;
    KeyCode Special;
    KeyCode Dodge;
    KeyCode Block;
    KeyCode Crouch;

    [SerializeField] Animator _animator;

    //[SerializeField] GameObject lightPunchPrefab;
    //[SerializeField] GameObject HeavyPunchPrefab;
    [SerializeField] GameObject kickPrefab;
    [SerializeField] GameObject specialPrefab;

    [SerializeField] Transform lightPunchTransform;
    [SerializeField] Transform HeavyPunchTransform;
    [SerializeField] Transform SpecialTransform;

    [SerializeField] float cooldown;
    private float timestamp;

    bool isAttacking;
    void Start()
    {
        LightPunch = KeyCode.Mouse0;
        HeavyPunch = KeyCode.Mouse1;
        Kick = KeyCode.LeftControl;
        Special = KeyCode.Space;
        Dodge = KeyCode.LeftShift;
        Block = KeyCode.C;
        Crouch = KeyCode.LeftShift;

    }

    // Update is called once per frame
    void Update() {
        if (!_animator.GetBool("isAttacking"))
        {
            if (Input.GetKeyDown(LightPunch))
            {
                _animator.SetTrigger("Light_Punch");
               // Instantiate(lightPunchPrefab, lightPunchTransform);
            }

            if (Input.GetKeyDown(HeavyPunch))
            {
                _animator.SetTrigger("Heavy_Punch");
            }
            /*else if (Input.GetButtonDown("Punch Left"))
            {
                Instantiate(punchPrefab, punchPointTransform);
                animator.SetTrigger("PunchLeft");
            }*/
            else if (Input.GetKeyDown(Kick))
            {
                //Instantiate(kickPrefab, kickPointTransform);
                _animator.SetTrigger("Kick");
            }
            /*else if (Input.GetButtonDown("Kick Left"))
            {
                Instantiate(kickPrefab, kickPointTransform);
                animator.SetTrigger("KickLeft");
            }*/
            else if (Input.GetKeyDown(Block))
            {
                _animator.SetBool("Block", true);
            }
            else if (Input.GetKeyUp(Block))
            {
                _animator.SetBool("Block", false);
            }
            else if (Input.GetKeyDown(Crouch))
            {
                _animator.SetBool("Crouch", true);
            }
            else if (Input.GetKeyUp(Crouch))
            {
                _animator.SetBool("Crouch", false);
            }
            else if (Input.GetKeyDown(Special) && timestamp < Time.time)
            {
                timestamp = Time.time + cooldown;
                _animator.SetTrigger("Special");

                Camera _camera;
                Ray _ray;
                Vector3 pointOffset, endPoint;
                GameObject controller = GameObject.Find("Game Controller");
                GameController g = controller.GetComponent<GameController>();
                _camera = g.getCamera();
                GameObject _ball;
                timestamp = Time.time + cooldown;
                _animator.SetTrigger("Special");
                {

                    // the middle of screen is half its width and height

                    Vector3 point = new Vector3(_camera.pixelWidth / 2, _camera.pixelHeight / 2, 0);

                    pointOffset = new Vector3(_camera.pixelWidth / 2, _camera.pixelHeight / 2, 1.5f);

                    // create a ray from that position
                    _ray = _camera.ScreenPointToRay(point);

                    RaycastHit hit;

                    // check if the ray hit an object that is visible to the camera

                    if (Physics.Raycast(_ray, out hit))
                    {
                        _ball = Instantiate(specialPrefab) as GameObject;
                        _ball.transform.position = transform.TransformPoint(new Vector3(0, 1f, 1f) * 1.5f);
                        _ball.transform.rotation = transform.rotation;

                    }
                    endPoint = hit.point;
                    Debug.Log(_ray.direction);

                }
            }
        }
    }
        
    void SpawnProjectile()
    {
        //problem: this will instantiate a new projectile point each time.
        Instantiate(specialPrefab, SpecialTransform.position, new Quaternion(0,0,0,0));
    }
}
