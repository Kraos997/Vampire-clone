using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectedStatue : MonoBehaviour
{

    [SerializeField] private BaseStatue baseStatue;
    [SerializeField] private GameObject visualGameObject;

    private void Start()
    {
        Player.Instance.OnSelectedStatueChanged += Player_OnSelectedStatueChanged;
    }

    private void Player_OnSelectedStatueChanged(object sender, Player.OnSelectedStatueChangedEventArgs e)
    {
        if(e.SelectedStatue == baseStatue)
        {
            Show();
        }
        else
        {
            Hide();
        }
    }
    private void Show()
    {
        visualGameObject.SetActive(true);
    }

    private void Hide()
    {
        visualGameObject.SetActive(false);
    }
}
