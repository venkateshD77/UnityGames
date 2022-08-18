using UnityEngine;

public class TouchMoveStrikers : MonoBehaviour
{
    Transform _strikerTransform;
    //Vector2 _offset;
    bool _canDrag = false;
    bool _oppoCanDrag = false;
    string _tag;

    void Update()
    {
        Vector2 _position;
        Vector2 _oppoPosiion;
        if (Input.touchCount > 0)
        {
            for (int i = 0; i < Input.touchCount; i++)
            {
                Touch _touch = Input.touches[i];
                Vector2 _touchPosition = _touch.position;

                if (_touch.phase == TouchPhase.Began)
                {
                    Ray _ray = Camera.main.ScreenPointToRay(_touchPosition);
                    RaycastHit2D _raycastHit = Physics2D.Raycast(_ray.origin, -Vector2.up);
                    if (_touchPosition.y < Screen.height / 2 && _raycastHit.collider.tag == "Strikers")
                    {
                        _tag = _raycastHit.collider.tag;
                        _strikerTransform = _raycastHit.transform;
                       // _position = new Vector2(_touchPosition.x, _touchPosition.y);
                        //_position = Camera.main.ScreenToWorldPoint(_position);
                        //_offset.x = _strikerTransform.position.x - _position.x;
                        //_offset.y = _strikerTransform.position.y - _position.y;
                        _canDrag = true;
                    }
                    else if(_touchPosition.y > Screen.height / 2 && _raycastHit.collider.tag == "OppoStrikers")
                    {
                        _tag = _raycastHit.collider.tag;
                        _strikerTransform = _raycastHit.transform;
                        //_oppoPosiion = new Vector2(_touchPosition.x, _touchPosition.y);
                        //_oppoPosiion = Camera.main.ScreenToWorldPoint(_oppoPosiion);
                       // _offset.x = _strikerTransform.position.x - _oppoPosiion.x;
                        //_offset.y = _strikerTransform.position.y - _oppoPosiion.y;
                        _oppoCanDrag = true;
                    }
                }

                if (_touch.phase == TouchPhase.Moved)
                {
                    if (_tag == "Strikers" && _canDrag )
                    {
                        ///Debug.Log("striker......... "+ _strikerTransform.position.y);
                        _position = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
                        _position = Camera.main.ScreenToWorldPoint(_position);
                        _strikerTransform.position = new Vector2(Mathf.Clamp(_position.x,-1.78f,1.78f),
                                                    Mathf.Clamp(_position.y,-4f,-0.6f)) ;
                    }
                    else if (_tag == "OppoStrikers" && _oppoCanDrag && _strikerTransform.position.y > 0.4)
                    {
                        _oppoPosiion = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
                        _oppoPosiion = Camera.main.ScreenToWorldPoint(_oppoPosiion);
                        _strikerTransform.position = new Vector2(Mathf.Clamp(_oppoPosiion.x, -1.78f, 1.78f),
                                                     Mathf.Clamp(_oppoPosiion.y, 0.6f, 4f));
                    }
                }
                if (_touch.phase == TouchPhase.Ended || _touch.phase == TouchPhase.Canceled)
                {
                    if (_canDrag && _tag == "Strikers")
                    {
                        _canDrag = false;
                        _tag = "";
                    }
                    else if (_oppoCanDrag && _tag == "OppoStrikers")
                    {
                        _oppoCanDrag = false;
                        _tag = "";
                    }                    
                }
            }
        }
    }

}