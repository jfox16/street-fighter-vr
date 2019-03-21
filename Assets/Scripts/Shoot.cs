using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour
{
    private Camera _camera;
    private Ray _ray;
    private Vector3 pointOffset, endPoint;
    private float timeToShoot = 30.0f;
    private float currentTime;

    private GameObject _ball;
    [SerializeField] private GameObject _ballprefab;

    // Start is called before the first frame update

    void Start()

    {

        // Access other components attached to the same object

        getCamera();
        currentTime = 0.0f;



        // hides the default mouse cursor so the ingame cursor can be used

        //Cursor.lockState = CursorLockMode.Locked;

        //Cursor.visible = false;

    }



    // Update is called once per frame

    void Update()

    {

        // Respond to the mouse button and check that gui isn't being used

        if (Input.GetKey("space") && currentTime >= timeToShoot)

        {

            // the middle of screen is half its width and height

            Vector3 point = new Vector3(_camera.pixelWidth / 2, _camera.pixelHeight / 2, 0);

            // create a ray from that position
            _ray = _camera.ScreenPointToRay(point);

            RaycastHit hit;

            // check if the ray hit an object that is visible to the camera

            if (Physics.Raycast(_ray, out hit))

            {
                if(_ball == null)
                {
                    // shoot the ball
                    _ball = Instantiate(_ballprefab) as GameObject;
                    // can probably change this since i wanted to shoot from the center of the character
                    _ball.transform.position = transform.TransformPoint(new Vector3(0,1f,1f) * 1.5f);
                    _ball.transform.rotation = transform.rotation;
                    // add force to the shooting object
                    _ball.GetComponent<Rigidbody>().velocity = _camera.transform.forward * 40;
                }

            }
            currentTime = 0.0f;
            endPoint = hit.point;

        }
        currentTime++;

    }



    // gets called automatically for MonoBehavior object (for cursor)

    void OnGUI()

    {

        // the text style is used to change the color of the gui label

        var TextStyle = new GUIStyle();

        TextStyle.normal.textColor = Color.red;



        int size = 24;

        float posX = _camera.pixelWidth / 2 - size / 4;

        float posY = _camera.pixelHeight / 2 - size / 2;



        // the command GUI.Label displays text on the screen.

        // parameters are (rectangle, string, GUIStyle)

        GUI.Label(new Rect(posX, posY, size, size), "o", TextStyle);

    }

    void getCamera()
    {
        GameObject controller = GameObject.Find("Game Controller");
        GameController g = controller.GetComponent<GameController>();
        _camera = g.getCamera();

    }

    public Vector3 getStartPoint()
    {
        return _ray.origin;
    }

    public Vector3 getEndPoint()
    {
        return endPoint;
    }


}
