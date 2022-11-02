using UnityEngine;
using Bomberman.Grid;

namespace Bomberman.Player
{
    /// <summary> класс отчвечает за движение персонажа и положения камеры отностильно него </summary>
    public class PlayerControler: MonoBehaviour
    {
        const float SPEED_FOR_PLEAYR = 50;
        public GameObject objectCamera;
        Rigidbody2D _rigidbodyPlayer;
        Vector3 _vectorLeft;
        Vector3 _vectorRigth;
        Vector3 _vectorUp;
        Vector3 _vectorDown;
        Camera _componentCamera;
        GenericGrid _gridGeneric;

        void Start()
        {
            _rigidbodyPlayer = GetComponent<Rigidbody2D>();
            _vectorLeft = new Vector3(-SPEED_FOR_PLEAYR, 0);
            _vectorRigth = new Vector3(SPEED_FOR_PLEAYR, 0);
            _vectorUp = new Vector3(0, SPEED_FOR_PLEAYR);
            _vectorDown = new Vector3(0,-SPEED_FOR_PLEAYR);
            _componentCamera = objectCamera.GetComponent<Camera>();
            _gridGeneric = objectCamera.GetComponent<GenericGrid>();
            StatrPositionPleayr();
            StatrPositionCamera();
        }

        void Update() => CorrectPositionCamera();

        void FixedUpdate() => InputControler();

        void InputControler()
        {
            if (Input.GetKey(KeyCode.D))
                _rigidbodyPlayer.AddForce(_vectorRigth);   
            if (Input.GetKey(KeyCode.A))
                _rigidbodyPlayer.AddForce(_vectorLeft);
            if (Input.GetKey(KeyCode.W))
                _rigidbodyPlayer.AddForce(_vectorUp);
            if (Input.GetKey(KeyCode.S))
                _rigidbodyPlayer.AddForce(_vectorDown);
        }
  
        void StatrPositionPleayr() 
        {
            transform.position = _gridGeneric.GetPositionCellForStartPleayr();
        } 
        void StatrPositionCamera()
        {
            objectCamera.transform.position = new Vector3
                (_gridGeneric._leftExtremePositionGrid + _componentCamera.orthographicSize * _componentCamera.aspect, 0, objectCamera.transform.position.z);
        }

        void CorrectPositionCamera()
        {
            if(_componentCamera.orthographicSize * _componentCamera.aspect <  _gridGeneric.GetDistanceToNearestCurbHorisontal(transform.position.x))
                objectCamera.transform.position = new(transform.position.x, 0, objectCamera.transform.position.z);   
        }

    }
}