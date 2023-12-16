using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSessions : MonoBehaviour
{
    private static GameSessions instance;

    public static GameSessions Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new GameObject("GameSessions").AddComponent<GameSessions>();
                DontDestroyOnLoad(instance.gameObject);
            }
            return instance;
        }
    }

    // Adicione outras variáveis que você deseja manter entre cenas.
    private int pontuacaoFinal;

    public int PontuacaoFinal
    {
        get { return pontuacaoFinal; }
        set { pontuacaoFinal = value; }
    }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
