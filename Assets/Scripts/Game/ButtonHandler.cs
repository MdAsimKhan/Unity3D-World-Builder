using UnityEngine;

public class ButtonHandler : MonoBehaviour
{
    [SerializeField] private GameObject player;
    public void QuitGame()
    {
        Application.Quit();
    }

    public void ResetPlayerPosition()
    {
        Debug.Log(player.transform.position);
        var characterController = player.GetComponent<CharacterController>();
        characterController.enabled = false;
        characterController.transform.position = new Vector3(0, 0, 0);
        characterController.enabled = true;
        Debug.Log(player.transform.position);
    }
}
