using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ViewScore : MonoBehaviour
{

    [SerializeField] private string startString;

    public void ShowScore(float value) => GetComponent<Text>().text = startString + value.ToString();
}
