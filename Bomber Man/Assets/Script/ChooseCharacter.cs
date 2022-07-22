using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace SelectCharacter
{
    public class ChooseCharacter : MonoBehaviour
    {
        private MyGameManagerData myGameManagerData;
        private GameObject gameStartButton;

        private void Start()
        {
            //�@���E�Ɉ������MyGameManager����MyGameManagerData���擾����
            myGameManagerData = FindObjectOfType<MyGameManager>().GetMyGameManagerData();
            //�@�Q�[���X�^�[�g�{�^�����擾����
            gameStartButton = transform.parent.Find("ButtonPanel/GameStart").gameObject;
            //�@�Q�[���X�^�[�g�{�^���𖳌��ɂ���
            gameStartButton.SetActive(false);
        }
        //�@�L�����N�^�[��I���������Ɏ��s���L�����N�^�[�f�[�^��MyGameManagerData�ɃZ�b�g
        public void OnSelectCharacter(GameObject character)
        {
            //�@MyGameManagerData�ɃL�����N�^�[�f�[�^���Z�b�g����
            myGameManagerData.SetCharacter(character);
            //�@�{�^���̑I����Ԃ��������đI�������{�^���̃n�C���C�g�\�����\�ɂ���ׂɎ��s
            //EventSystem.current.SetSelectedGameObject(null);
            //�@�Q�[���X�^�[�g�{�^����L���ɂ���
            gameStartButton.SetActive(true);
        }
        //�@�L�����N�^�[��I���������ɔw�i���I���ɂ���
        public void SwitchButtonBackground(int buttonNumber)
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                if (i == buttonNumber - 1)
                {
                    transform.GetChild(i).Find("Background").gameObject.SetActive(true);
                }
                else
                {
                    transform.GetChild(i).Find("Background").gameObject.SetActive(false);
                }
            }
        }
        public void StartGame()
        {
            SceneManager.LoadScene("douki");
        }
    }
}