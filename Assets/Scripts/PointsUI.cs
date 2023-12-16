using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PointsUI : MonoBehaviour
{
    // Start is called before the first frame update
    TextMeshProUGUI textPontuacao;

    public int ptns; 

    void Start()
    {
        textPontuacao = GetComponent<TextMeshProUGUI>();
    }

    public void AtualizaPontuacao(int pontuacao = 1){
        int pontuacaoAtual = int.Parse(textPontuacao.text);
        pontuacaoAtual += pontuacao;
        textPontuacao.text = pontuacaoAtual.ToString();
    }


    // public void Pontuacao(){
    //     GameSessions.Instance.PontuacaoFinal = pontuacaoAtual;
    // }

    // AtualizaPontuacao();

    // Update is called once per frame
}
