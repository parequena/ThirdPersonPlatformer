using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attachable : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    private bool _isAttachable;

    private bool _isAttached;
    public bool IsAttachable{
        get { return _isAttachable; }
        set { _isAttachable = value; }}

    public bool IsAttached{
        get{ return _isAttached; }
    set{ _isAttached = value; }
    }
}
