using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using FMODUnity;
using TMPro;

public class MovementOverrideLogic : MonoBehaviour
{
    private bool triggered;
    private bool teleported;
    public FirstPersonController player;
    public GameObject staticScreen;
    public Light flashlight;
    public GameObject teleportMarker;
    public GameObject structure;
    public float moveSpeed;
    public GameObject tvWithEmitter;
    public GameObject warningText;
    public Animator animator;

    private BotanicalWingInit[] inits;
    // Start is called before the first frame update
    void Start()
    {
        triggered = false;
        teleported = false;
        inits = GameObject.FindObjectsOfType<BotanicalWingInit>();
        animator = warningText.GetComponent<Animator>();
    }


    void Update(){
        StartCoroutine(handleForcedMovement());
    }

    IEnumerator handleForcedMovement(){
        if (triggered)
        {
            flashlight.GetComponent<FlashlightTimeout>().losingBattery = false;
            flashlight.intensity = flashlight.GetComponent<FlashlightTimeout>().flashingIntensity;
            tvWithEmitter.GetComponent<StudioEventEmitter>().enabled = true;
        }
        if (triggered && !teleported){
            if (player.transform.position.z < staticScreen.transform.position.z){
                player.transform.position += new Vector3(0, 0, moveSpeed * Time.deltaTime);
            }
            else if (player.transform.position.x < staticScreen.transform.position.x - 1){
                player.transform.position += new Vector3(moveSpeed * Time.deltaTime, 0, 0);
            }
            else{
                yield return new WaitForSeconds(4);
                player.transform.position = teleportMarker.transform.position;
                structure.SetActive(false);
                staticScreen.GetComponent<MeshRenderer>().enabled = false;
                teleported = true;
            }
        }
        else if (triggered && teleported){
            if (GameObject.Find("Final Sequence Tele Location").transform.position.x - player.transform.position.x < 10){
                player.transform.position -= new Vector3(moveSpeed * Time.deltaTime, 0, 0);
            }
            else{
                yield return new WaitForSeconds(3);
                PauseMenu.GamePaused = true;
                player.playerCanMove = true;
                Cursor.lockState = CursorLockMode.None;
                foreach (BotanicalWingInit init in inits)
                    init.Stop();
                FMODUnity.StudioEventEmitter[] emitters = GameObject.FindObjectsOfType<FMODUnity.StudioEventEmitter>();
                foreach (FMODUnity.StudioEventEmitter em in emitters)
                    em.GetComponent<FMODUnity.StudioEventEmitter>().Stop();
                animator.ResetTrigger("Warned");
                animator.ResetTrigger("Faded");
                animator.SetTrigger("Reset");
                SceneManager.LoadScene("MainMenu_sketch");
            }
        }
    }


    void OnTriggerEnter(Collider collider)
    {
        triggered = true;
        player.playerCanMove = false;
        player.isWalking = false;
        player.enableHeadBob = false;
        animator.ResetTrigger("Warned");
        animator.ResetTrigger("Faded");
        animator.SetTrigger("Reset");
        // change warning text
        warningText.GetComponent<TextMeshProUGUI>().text = "Warning:\u0009\u0009\u0009";
        animator.SetTrigger("Warned");
        animator.ResetTrigger("Reset");
    }
}
