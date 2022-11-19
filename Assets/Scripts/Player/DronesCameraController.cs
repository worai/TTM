using UnityEngine;
using UnityEngine.InputSystem;

public class DronesCameraController : MonoBehaviour
{
  [SerializeField] Transform playerTrans;
  [SerializeField] GameObject droneTemplate;
  [SerializeField] private Camera cam;
  [SerializeField] private float stdSize = 5;

  private DronesCameraInput input;
  private int _currentNumDrones;
  private int _maximumNumDrones = 4;
  private int? _last_dir;

  private void Start()
  {
    Debug.Log("Drones camera controller started");
    cam.orthographicSize = stdSize;
    input = new DronesCameraInput();
    input.Enable();

    input.Player.AddDrone.started += AddDrone_started;
    input.Player.RemoveDrone.started += RemoveDrone_started; ;
    input.Player.RemoveAllDrones.started += RemoveAllDrones_started;

    input.Player.DownLeft.started += DownLeft_started;
    input.Player.Down.started += Down_started;
    input.Player.DownRight.started += DownRight_started;
    input.Player.Left.started += Left_started;
    input.Player.MiddleCentre.started += MiddleCentre_started;
    input.Player.Right.started += Right_started;
    input.Player.UpLeft.started += UpLeft_started;
    input.Player.Up.started += Up_started;
    input.Player.UpRight.started += UpRight_started;
  }

  #region view direction handling
  private void UpRight_started(InputAction.CallbackContext obj)
  {
    MoveView(8);
  }

  private void Up_started(InputAction.CallbackContext obj)
  {
    MoveView(7);
  }

  private void UpLeft_started(InputAction.CallbackContext obj)
  {
    MoveView(6);
  }

  private void Right_started(InputAction.CallbackContext obj)
  {
    MoveView(5);
  }

  private void MiddleCentre_started(InputAction.CallbackContext obj)
  {
    MoveView(4);
  }

  private void Left_started(InputAction.CallbackContext obj)
  {
    MoveView(3);
  }

  private void DownRight_started(InputAction.CallbackContext obj)
  {
    MoveView(2);
  }

  private void Down_started(InputAction.CallbackContext obj)
  {
    MoveView(1);
  }

  private void DownLeft_started(InputAction.CallbackContext obj)
  {
    MoveView(0);
  }

  /// <summary>
  ///  678
  ///  345
  ///  012
  ///  
  /// called by input action cpt on camera object
  /// </summary>
  /// <param name="dir">zero based</param>
  public void MoveView(int dir)
  {
    int i = (dir) % 3 - 1;
    int j = (int)((dir) / 3) - 1;

    if(dir != 4)
    { 
      if (_currentNumDrones == 0  || 
        (_last_dir.HasValue && _last_dir == dir)
        ) TryAddDrone((new Vector3(i, j)).normalized);
    }
    if (dir != 4) _last_dir = dir;
    //float shift = stdSize * 0.5f * GetZoomOutFactor();
    float shift = stdSize * GetZoomOutFactor() - 1f;
    cam.transform.localPosition = new Vector3(i * shift, j * shift, -10f);

  }

  #endregion

  private void RemoveAllDrones_started(InputAction.CallbackContext context)
  {
    int difference = _currentNumDrones;
    _currentNumDrones = 0;
    for (int i = 0; i < difference; i++)
    {
      //TODO a tiny stagger in spawning of drones
      DespawnDrone();
    }
    AdjustCamerSize();
    //move to middle of screen
    MoveView(4);
    _last_dir = -1;
  }

  private void AddDrone_started(UnityEngine.InputSystem.InputAction.CallbackContext obj)
  {
    TryAddDrone();
  }

  private void TryAddDrone(Vector3? direction = null)
  {
    if (_currentNumDrones < _maximumNumDrones)
    {
      _currentNumDrones++;
      SpawnDrone(direction);
      AdjustCamerSize();
    }
  }

  private void RemoveDrone_started(UnityEngine.InputSystem.InputAction.CallbackContext context)
  {
    if (_currentNumDrones > 0)
    {
      _currentNumDrones--;
      DespawnDrone();
      AdjustCamerSize();
      if(_last_dir.HasValue && _last_dir != -1) MoveView(_last_dir.Value);
    }
    if (_currentNumDrones == 0)
    {
      //move to middle of screen
      MoveView(4);
      _last_dir = -1;
    }
  }

  private void SpawnDrone(Vector3? direction = null, DroneController.DroneMode mode = DroneController.DroneMode.Spawning)
  {
    GameObject newGO = Instantiate(droneTemplate); 
    newGO.transform.position = playerTrans.position;
    newGO.SetActive(true);
    DroneController controller = newGO.GetComponent<DroneController>();
    if(direction != null) controller.SetDirection(direction.Value);
    controller.Instantiate(DroneController.DroneMode.Spawning);
  }

  private void DespawnDrone()
  {
    GameObject newGO = Instantiate(droneTemplate);
    newGO.transform.position = playerTrans.position + new Vector3(100f, 0f);
    newGO.SetActive(true);
    newGO.GetComponent<DroneController>().Instantiate(DroneController.DroneMode.Despawning);
  }

  private void AdjustCamerSize()
  {
    float factor = GetZoomOutFactor();
    cam.orthographicSize = stdSize * factor;
  }

  private float GetZoomOutFactor()
  {
    float result = 1f;
    if (_currentNumDrones == 1)      result = 1.2f;
    else if (_currentNumDrones == 2) result = 1.6f;
    else if (_currentNumDrones == 3) result = 2.0f;
    else if (_currentNumDrones == 4) result = 2.6f;
    return result;
  }
}
