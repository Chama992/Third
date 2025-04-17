using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;


public class ProgressController : MonoBehaviour
{
    private enum FireType
    {
        Blue,
        Red,
        Orange
    }
    public readonly Color blue = new Color(165 / 255f, 242 / 255f, 223 / 255f, 1);
    public readonly Color red = new Color(235/255f,121/255f,80/255f,1);
    public readonly Color orange = new Color(245/255f,169/255f,63/255f,1);
    public readonly Color gameorange = new Color(255 / 255f, 236 / 255f, 97 / 255f, 1);
    private Animator fireAnimator;
    public Slider gameProgressSlider;
    public Slider firePressSlider;
    public Image fireProgressImage;
    public Image gameProgressImage;
    [SerializeField]
    private float RedTimer;
    [SerializeField]
    private float shotTimer;
    private FireType currentFireType;
    private bool isShooting;
    public Action ShotFinished;
    private float winGameTime;

    private void Awake()
    {
        fireAnimator = GetComponentInChildren<Animator>();
        ProgressInit();
    }

    private void OnEnable()
    {
        ProgressInit();
    }

    private void OnDisable()
    {
        // ShotFinished = null; //TODO:后续改进
    }
    private void Update()
    {
        gameProgressSlider.value = RedTimer / winGameTime;
        firePressSlider.value = shotTimer / 8f;
        if (isShooting)
        {
            shotTimer += Time.deltaTime;
            FireTypeCheck();
        }
        else
        {
            
            if (shotTimer > 0)
            {
                shotTimer -= Time.deltaTime;
                FireTypeCheck();
            }
            else
            {
                ChangeFireType(FireType.Blue);
            }
        }
    }
    private void FireTypeCheck()
    {
        if (shotTimer >= 8f)
        {
            ChangeFireType(FireType.Orange);
        }
        else if (shotTimer >= 3f)
        {
            ChangeFireType(FireType.Red);
        }
        else
        {
            ChangeFireType(FireType.Blue);
        }
    }



    public void StartShot()
    {
        if (shotTimer < 0)
        {
            shotTimer = 0;
        }
        isShooting = true;
    }
    public void EndShot()
    {
        isShooting = false;
    }

    private void ChangeFireType(FireType _fireType)
    {
        if (currentFireType == _fireType)
        {
            return;
        }
        currentFireType = _fireType;
        switch (currentFireType)
        {
            case FireType.Blue:
                fireAnimator.SetBool("Red",false);
                fireAnimator.SetBool("Orange",false);
                fireProgressImage.color = blue;
                break;
            case FireType.Red:
                fireAnimator.SetBool("Orange",false);
                fireAnimator.SetBool("Red",true);
                fireProgressImage.color = red;
                StartCoroutine(AddRedTime());
                break;
            case FireType.Orange:
                fireProgressImage.color = orange;
                fireAnimator.SetBool("Orange",true);
                break;
        }
    }

    IEnumerator AddRedTime()
    {
        while (true)
        {
            if (currentFireType == FireType.Red)
            {
                RedTimer += Time.deltaTime;
                if (RedTimer >= winGameTime)
                {
                    ShotFinished?.Invoke();
                    yield break;
                }
                yield return null;
            }
            else
            {
                RedTimer = 0;
                yield break;
            }
        }
    }

    private void ProgressInit()
    {
        shotTimer = 0f;
        RedTimer = 0f;
        gameProgressSlider.value = 0;
        firePressSlider.value = 0;
        currentFireType = FireType.Blue;
        fireProgressImage.color = blue;
        gameProgressImage.color = gameorange;
    }

    public void SetFinishTime(float _winGameTime)
    {
        winGameTime = _winGameTime;
    }
}
