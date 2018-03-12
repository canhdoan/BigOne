﻿using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using System;
using Convertitore;
using System.IO;


[Serializable]
[CreateAssetMenu(menuName = "Prototype/Profile")]
public class Profile : ScriptableObject
{

    public string SavedScene = "";
    public bool newGame = false;
    public int LastScene;

    public bool Continue=false;
    static Converter C = new Converter();

    //public struct customDateTime
    //{
    //    public int year; public int month; public int day;

    //    public int hour; public int minute; public int second;

    //}
    //public customDateTime dateTime;

    public bool[] completedLevel;

    public void Save()
    {

        if (SceneManager.GetActiveScene().buildIndex != 0 || SceneManager.GetActiveScene().name != "Menu")
        {
            LastScene = SceneManager.GetActiveScene().buildIndex;
            SavedScene = SceneManager.GetActiveScene().name;
            switch(SceneManager.GetActiveScene().buildIndex)
            {

                case 2:
                    completedLevel = new bool[5] { true,false,false,false,false};
                    break;
                case 3:
                    completedLevel = new bool[5] { true, true, false, false, false };
                    break;
                case 5:
                    completedLevel = new bool[5] { true, true, true, false, false };
                    break;
                case 6:
                    completedLevel = new bool[5] { true, true, true, true, false }; break;
                case 7:
                    completedLevel = new bool[5] { true, true, true, true, true }; break;
                default:
                    completedLevel = new bool[5] { true, false, false, false, false }; break;
            }
        }
    }
    public static void SaveProfile(string path, Profile profile)
    {

        string json = JsonUtility.ToJson(profile);
        string save = "";
        StreamWriter sw = File.CreateText(path); 
        sw.Close();
     
        File.WriteAllText(path, json);
    }
    public static Profile LoadProfile(string path)
    {
        string json = File.ReadAllText(path);
       
        //string[] savedData;
        //string save = "";
        //savedData = json.Split(' ');
        //for (int i = 0; i < savedData.Length; i++)
        //{
        //    save += (char)Convert.ToInt32(C.FromTo(16, 10, savedData[i]));
        //}
        return JsonUtility.FromJson<Profile>(json);


    }

}
