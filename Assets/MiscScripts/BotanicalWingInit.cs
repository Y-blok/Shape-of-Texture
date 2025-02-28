using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using FMODUnity;

public class BotanicalWingInit : MonoBehaviour
{
    public GameObject movingDoor;
    public string audioLogPath;
    public GameObject textBox;
    public Light flashlight;
    private FMODUnity.StudioEventEmitter[] emitters;
    private Occlusion[] occlusions;

    private FMOD.Studio.EventInstance instance;
    private bool isPlaying;

    public IEnumerator cr;
    // Start is called before the first frame update
    void Start()
    {
        instance = FMODUnity.RuntimeManager.CreateInstance(audioLogPath);
        isPlaying = false;
        emitters = GetComponentsInChildren<StudioEventEmitter>();
        occlusions = GetComponentsInChildren<Occlusion>();
    }

    public void handleObjPress(GameObject pressedObj){
        instance.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
        instance.start();
        isPlaying = true;

        foreach (Occlusion occ in occlusions)
        {
            occ.Stop();
        }

        if (audioLogPath == "event:/Botanical Wing Scene/LOG_The Light is Out") {
            if (cr != null){
                StopCoroutine(cr);
            }
            cr = SubtitleSequence001();
            StartCoroutine(cr);
        }
        else if (audioLogPath == "event:/Botanical Wing Scene/SFX_BotanicalIntroLog") {
            if (cr != null){
                StopCoroutine(cr);
            }
            cr = SubtitleSequence002();
            StartCoroutine(cr);
        }
        else if (audioLogPath == "event:/Botanical Wing Scene/SFX_BotanicalIntroLog 2") {
            if (cr != null){
                StopCoroutine(cr);
            }
            cr = SubtitleSequence003();
            StartCoroutine(cr);
        }
        else if (audioLogPath == "event:/Botanical Wing Scene/SFX_BotanicalIntroLog 3") {
            if (cr != null){
                StopCoroutine(cr);
            }
            cr = SubtitleSequence004();
            StartCoroutine(cr);
        }
        

        if (!(movingDoor == null)){
            //Move door
            pressedObj.GetComponentInChildren<Light>().intensity = 0;
            Destroy(movingDoor);
            flashlight.GetComponent<FlashlightTimeout>().losingBattery = true;
        }
        else{
            pressedObj.GetComponentInChildren<Light>().color = Color.green;
        }
    }

    void Update()
    {
        if (isPlaying && PauseMenu.GamePaused){
            instance.setPaused(true);
        }
        else if (isPlaying && !PauseMenu.GamePaused){
            instance.setPaused(false);
        }
    }

    public void Play()
    {
        foreach (Occlusion occ in occlusions)
        {
            occ.Play();
        }
    }

    public void Stop()
    {
        if (cr != null)
            StopCoroutine(cr);
        instance.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
        foreach (StudioEventEmitter emitter in emitters)
        {
            emitter.Stop();
        }
    }

    IEnumerator SubtitleSequence001() {
        // yield return new WaitForSeconds(1);
        textBox.GetComponent<TextMeshProUGUI>().text = "Your flashlight is low on power… Find a battery…";
        yield return new WaitForSeconds(6);
        textBox.GetComponent<TextMeshProUGUI>().text = "Don’t let it run out again, or else…";
        yield return new WaitForSeconds(5);
        if (textBox.GetComponent<TextMeshProUGUI>().text == "Don’t let it run out again, or else…")
            textBox.GetComponent<TextMeshProUGUI>().text = "";
        isPlaying = false;
    }
    IEnumerator SubtitleSequence002() {
        yield return new WaitForSeconds(1);
        textBox.GetComponent<TextMeshProUGUI>().text = "Okay, so before we send this station to its orbit,";
        yield return new WaitForSeconds(3.5f);
        textBox.GetComponent<TextMeshProUGUI>().text = "I'm making a simple guide for the crew.";
        yield return new WaitForSeconds(2.5f);
        textBox.GetComponent<TextMeshProUGUI>().text = "This is the biolab, now, I'm sure you're all familiar with the concept of plants,";
        yield return new WaitForSeconds(4);
        textBox.GetComponent<TextMeshProUGUI>().text = "but for those of you who come from colonies where they weren't on the curriculum,";
        yield return new WaitForSeconds(4);
        textBox.GetComponent<TextMeshProUGUI>().text = "plants produce food and oxygen";
        yield return new WaitForSeconds(3);
        textBox.GetComponent<TextMeshProUGUI>().text = "Without them, the station does not produce food or oxygen.";
        yield return new WaitForSeconds(3);
        textBox.GetComponent<TextMeshProUGUI>().text = "Generally the plants in the biolab should never be left unattended for more than four hours.";
        yield return new WaitForSeconds(6);
        if (textBox.GetComponent<TextMeshProUGUI>().text == "Generally the plants in the biolab should never be left unattended for more than four hours.")
            textBox.GetComponent<TextMeshProUGUI>().text = "";
        isPlaying = false;
    }

    IEnumerator SubtitleSequence003() {
        // yield return new WaitForSeconds(1);
        textBox.GetComponent<TextMeshProUGUI>().text = "Alright, with the simple stuff out of the way, let's move on to the technical elements;";
        yield return new WaitForSeconds(6);
        textBox.GetComponent<TextMeshProUGUI>().text = "The plants are being exposed to radiation-emitting lights at all times.";
        yield return new WaitForSeconds(5);
        textBox.GetComponent<TextMeshProUGUI>().text = "Do not stick your hands or any other part of your body";
        yield return new WaitForSeconds(3);
        textBox.GetComponent<TextMeshProUGUI>().text = "directly under the lights unless you're wearing the appropriate protective gear,";
        yield return new WaitForSeconds(5);
        textBox.GetComponent<TextMeshProUGUI>().text = "which should be in the closets in the back.";
        yield return new WaitForSeconds(2);
        if (textBox.GetComponent<TextMeshProUGUI>().text == "which should be in the closets in the back.")
            textBox.GetComponent<TextMeshProUGUI>().text = "";
        isPlaying = false;
    }

    IEnumerator SubtitleSequence004() {
        // yield return new WaitForSeconds(1);
        textBox.GetComponent<TextMeshProUGUI>().text = "Additionally, the radiation should cause the plants to mutate;";
        yield return new WaitForSeconds(4);
        textBox.GetComponent<TextMeshProUGUI>().text = "this is intentional, but do not eat anything";
        yield return new WaitForSeconds(4);
        textBox.GetComponent<TextMeshProUGUI>().text = "without putting it in the testing devices near the bow-side of the room.";
        yield return new WaitForSeconds(3);
        textBox.GetComponent<TextMeshProUGUI>().text = "Thank you for your attention, this has been Dr. Franks,";
        yield return new WaitForSeconds(4);
        textBox.GetComponent<TextMeshProUGUI>().text = "take care aboard the station.";
        yield return new WaitForSeconds(2);
        if (textBox.GetComponent<TextMeshProUGUI>().text == "take care aboard the station.")
            textBox.GetComponent<TextMeshProUGUI>().text = "";
        isPlaying = false;
    }
}
