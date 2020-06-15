using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class UI_Exp : MonoBehaviour
{
    public Player player_;
    [SerializeField]
    private UIProgressBar expBar = null;
    [SerializeField]
    private UILabel expLabel = null;



    public void ExpUIReflash()
    {
        expBar.value = (float)player_.exp / (float)player_.maxExp;
        expLabel.text = string.Format("Exp {0} / {1}",player_.exp , player_.maxExp);
    }
}
