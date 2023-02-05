//Singleton class for managing input

using UnityEngine;


class InputManager : Singleton<InputManager>{
    

    public Vector3 CursorWorldPosition {get; private set;}
    public RaycastHit hitInfo {get; private set;}
    public static Camera MainCamera
    //get the main camera again if null (for example if the scene is reloaded)
    {
        get
        {
            if (_mainCamera == null)
                _mainCamera = Camera.main;
            return _mainCamera;
        }
    }
    static Camera _mainCamera;

    public void Update(){

        //Raycast from camera to terrain
        var hitPosition = new Vector3();
        var ray = MainCamera.ScreenPointToRay(Input.mousePosition);
        //raycast and ignore the alyer called ignore raycast


        if (Physics.Raycast(ray, out RaycastHit hit, 3000f, ~LayerMask.GetMask("Ignore Raycast"))){
            hitInfo = hit;
            hitPosition = hitInfo.point;
        }else{
            hitInfo = new RaycastHit();
            hitPosition = ray.GetPoint(3000f);
        }
        
        CursorWorldPosition = hitPosition;
    }

#if UNITY_EDITOR
    private void OnDrawGizmoSelected (){
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(CursorWorldPosition, 0.1f);
        Gizmos.DrawLine(MainCamera.transform.position, CursorWorldPosition);

        //draw the name of the game object under the cursor next to cursor world position
        UnityEditor.Handles.Label(CursorWorldPosition, hitInfo.collider.gameObject.name);

    }
#endif
}