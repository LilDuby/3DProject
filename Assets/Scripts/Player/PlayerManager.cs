using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    // 외부에서 Instance를 통해 접근하면 _instance를 반환
    private static PlayerManager _instance;
    public static PlayerManager Instance
    {
        get // 반환
        {
            // 게임씬에 없을경우 생성해주는 방어코드
            if (_instance == null)
            {
                // 오브젝트 생성 후 PlayerManager 컴포넌트 추가
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
