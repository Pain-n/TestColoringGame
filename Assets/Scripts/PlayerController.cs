using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerController : MonoBehaviour
{
    public Color SelectedColor;
    private Vector3 camStartMovePos;

    public float CameraSpeed;
    private bool CamCanMove;
    private void Awake()
    {
        BrushDetector.SetBrushColor += ChangeBrushColor;
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if(Physics.Raycast(ray, out hit, 1000))
            {
                if(hit.transform.tag == "PaintPart")
                {
                    hit.transform.GetComponent<PaintPart>().ChangeColor(SelectedColor); 
                }
            }
            camStartMovePos = GetWorldPos(CameraSpeed);

            //отдельно стрел€ю вторым рейкастом дл€ проверки на стукание в UI, нужно дл€ корректного движени€ камеры
            var results = new List<RaycastResult>();
            EventSystem.current.RaycastAll(new PointerEventData(EventSystem.current) { position = Input.mousePosition }, results);

            if (results.Count > 1)
            {
                if (results[1].gameObject.layer != 5)
                {
                    CamCanMove = true;
                }
                else
                {
                    CamCanMove = false;
                }
            }
            else
            {
                CamCanMove = true;
            }
        }

        if (Input.GetKey(KeyCode.Mouse0) && CamCanMove == true)
        {
            Vector3 direction = camStartMovePos - GetWorldPos(CameraSpeed);
            Vector3 newPos = Camera.main.transform.position + direction;
            if (newPos.x >= -2 && newPos.x <= 2 &&
                newPos.y >= 2 && newPos.y <= 3)
            {
                Camera.main.transform.position = newPos;
            }
        }
    }

    private Vector3 GetWorldPos(float camSpeed)
    {
        Ray mousePos = Camera.main.ScreenPointToRay(Input.mousePosition);
        Plane ground = new Plane(Vector3.forward, new Vector3(0,0, camSpeed));
        float distance;
        ground.Raycast(mousePos, out distance);
        return mousePos.GetPoint(distance);
    }

    private void ChangeBrushColor(Color newColor)
    {
        SelectedColor = newColor;
    }

    private void OnDestroy()
    {
        BrushDetector.SetBrushColor -= ChangeBrushColor;
    }
}
