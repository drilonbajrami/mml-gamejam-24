using UnityEngine;

public class FinishLine : MonoBehaviour
{
    public PlayerController playerController;
    public Camera menuCamera;
    public GameObject gameFinishPanel;

    public void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Player"))
            FinishGame();
    }

    private void FinishGame()
    {
        playerController.gameObject.SetActive(false);
        menuCamera.gameObject.SetActive(true);
        gameFinishPanel.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
}