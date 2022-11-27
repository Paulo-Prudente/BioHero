using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class Energy : MonoBehaviour
{
    public GameObject menuFlutuante;
    public GameObject selecaoFase;
    [SerializeField]
    private Text textEnergy;

    [SerializeField]
    private Text textTimer;

    [SerializeField]
    private int maxEnergy;

    private int totalEnergy = 0;

    private DateTime nextEnergyTime;

    private DateTime lastAddedTime;

    private int restoreDuration = 61;

    private bool restoring = false;
    // Start is called before the first frame update
    void Start()
    {
        Load();
        StartCoroutine(RestoreRoutine());
    }

    public void UseEnergy() 
    {
        if (totalEnergy < 5)
        {
            menuFlutuante.SetActive(true);
            selecaoFase.SetActive(false);
            //mostrar tela de compra de energia
            return;
        }
        totalEnergy = totalEnergy - 5;
        UpdateEnergy();
        if (!restoring)
        {
            if (totalEnergy +1 ==maxEnergy)
            {
                nextEnergyTime = AddDuration(DateTime.Now, restoreDuration);
            }
            StartCoroutine(RestoreRoutine());
        }
    }

    private IEnumerator RestoreRoutine() 
    {
        UpdateTimer();
        UpdateEnergy();
        restoring = true;
        while (totalEnergy < maxEnergy)
        {
            DateTime currentTime = DateTime.Now;
            DateTime counter = nextEnergyTime;
            bool isAdding = false;
            while (currentTime > counter)
            {
                if (totalEnergy < maxEnergy)
                {
                    isAdding = true;
                    totalEnergy++;
                    DateTime timeToAdd = lastAddedTime > counter ? lastAddedTime : counter;
                    counter = AddDuration(timeToAdd, restoreDuration);
                }
                else
                {
                    break;
                }
            }
            if (isAdding) 
            {
                lastAddedTime = DateTime.Now;
                nextEnergyTime = counter;
            }
            
            UpdateTimer();
            UpdateEnergy();
            Save();
            yield return null;
        }
        restoring = false;
    }
    private void UpdateTimer() 
    {
        if (totalEnergy >= maxEnergy)
        {
            textTimer.text = "Full";
            return;
        }

        TimeSpan t = nextEnergyTime - DateTime.Now;
        string value = String.Format("{0:D2}:{1:D2}", t.Minutes, t.Seconds);
        textTimer.text = value;
    }
    private void UpdateEnergy() 
    {
        textEnergy.text = totalEnergy.ToString();
    }

    private DateTime AddDuration(DateTime time, int duration) 
    {
        return time.AddSeconds(duration);
    }
    public void Save() 
    {
        PlayerPrefs.SetInt("totalEnergy", totalEnergy);
        PlayerPrefs.SetString("nextEnergyTime", nextEnergyTime.ToString());
        PlayerPrefs.SetString("lastAddedTime", lastAddedTime.ToString());
    }

    public void Load() 
    {
        totalEnergy = PlayerPrefs.GetInt("totalEnergy");
        nextEnergyTime = StringToDate(PlayerPrefs.GetString("nextEnergyTime"));
        lastAddedTime = StringToDate(PlayerPrefs.GetString("lastAddedTime"));
    }

    private DateTime StringToDate(string Date) 
    {
        if (String.IsNullOrEmpty(Date))
        {
            return DateTime.Now;
        }
        return DateTime.Parse(Date);
    }
}
