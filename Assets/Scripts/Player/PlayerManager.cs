using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    // �ܺο��� Instance�� ���� �����ϸ� _instance�� ��ȯ
    private static PlayerManager _instance;
    public static PlayerManager Instance
    {
        get // ��ȯ
        {
            // ���Ӿ��� ������� �������ִ� ����ڵ�
            if (_instance == null)
            {
                // ������Ʈ ���� �� PlayerManager ������Ʈ �߰�
                _instance = new GameObject("PlayerManager").AddComponent<PlayerManager>();
            }
            return _instance;
        }
    }

    public Player _player;
    public Player Player
    {
        get { return _player; }
        set { _player = value; }
    }

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            if(_instance != this)
            {
                Destroy(gameObject);
            }
        }
    }
}
