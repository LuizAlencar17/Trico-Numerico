using UnityEngine;
using UnityEngine.EventSystems;

public class FloatingJoystick : Joystick
{
    Vector2 joystickCenter = Vector2.zero;
	public static bool Ativado;

    void Start()
    {
        background.gameObject.SetActive(false);
    }

    public override void OnDrag(PointerEventData eventData)
    {
		Ativado = true;

        Vector2 direction = eventData.position - joystickCenter;
        inputVector = (direction.magnitude > background.sizeDelta.x / 2f) ? direction.normalized : direction / (background.sizeDelta.x / 2f);
        ClampJoystick();
        handle.anchoredPosition = (inputVector * background.sizeDelta.x / 2f) * handleLimit;

		if(handle.anchoredPosition.x>0){
			Jogador.move = 1;
		}
		if(handle.anchoredPosition.x<0){
			Jogador.move = -1;
		}
		if(handle.anchoredPosition.x==0){
			Jogador.move = 0;
		}

    }

    public override void OnPointerDown(PointerEventData eventData)
    {
        background.gameObject.SetActive(true);
        background.position = eventData.position;
        handle.anchoredPosition = Vector2.zero;
        joystickCenter = eventData.position;


    }

    public override void OnPointerUp(PointerEventData eventData)
    {
        background.gameObject.SetActive(false);
        inputVector = Vector2.zero;
		Jogador.move = 0;

    }
}