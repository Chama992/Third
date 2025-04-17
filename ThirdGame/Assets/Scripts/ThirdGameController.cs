using System;
using UnityEditor;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;


public class ThirdGameController : MonoBehaviour
{
    public float WinGameTime;
    public Canvas GameCanvas;
    public Canvas StartGameCanvas;
    public Image ConvexLens;
    public Transform initPoint;
    public Image bg;
    public Button closeBtn;
    public Button StartBtn;
    public Button QuitBtn;
    public Image Fire;
    public LineRenderer line;
    public ProgressController progressController;
    private void Start()
    {
        ConvexlensInit();
        BtnInit();
        SliderInit();
        StartGameCanvas.gameObject.SetActive(true);
        GameCanvas.gameObject.SetActive(false);
    }
    private void ConvexlensInit()
    {
        ConvexLens convexLens = ConvexLens.GetComponent<ConvexLens>();
        convexLens.SetDragScope(new Vector3(bg.rectTransform.rect.xMax  * (Screen.width / 1920f) + Screen.width * 0.5f,bg.rectTransform.rect.yMax  * (Screen.height / 1080f) + Screen.height * 0.5f),
            new Vector3((-bg.rectTransform.rect.xMax  * (Screen.width / 1920f)) + Screen.width * 0.5f,(-bg.rectTransform.rect.yMax  * (Screen.height / 1080f))+ Screen.height * 0.5f));
        convexLens.SetLine(line);
        convexLens.SetFireTarget(Fire.rectTransform);
        convexLens.SetProgress(progressController);
        convexLens.SetInitPos(initPoint.localPosition);
    }
    private void SliderInit()
    {
        progressController.ShotFinished += WinGame;
        progressController.SetFinishTime(WinGameTime);
    }

    private void BtnInit()
    {
        closeBtn.onClick.AddListener(CloseGame);
        StartBtn.onClick.AddListener(StartGame);
#if UNITY_EDITOR
        QuitBtn.onClick.AddListener(() => { UnityEditor.EditorApplication.isPlaying = false; });
#else
        QuitBtn.onClick.AddListener( () => { Application.Quit(); });
#endif
    }
    private void WinGame()
    {
        GameCanvas.gameObject.SetActive(false);
        StartGameCanvas.gameObject.SetActive(true);
        Debug.Log("WinGame");
    }
    private void CloseGame()
    {
        StartGameCanvas.gameObject.SetActive(true);
        GameCanvas.gameObject.SetActive(false);
    }
    private void StartGame()
    {
        GameCanvas.gameObject.SetActive(true);
        StartGameCanvas.gameObject.SetActive(false);
    }
}
