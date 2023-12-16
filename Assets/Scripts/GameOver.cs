using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameOver : MonoBehaviour
{

    TextMeshProUGUI textPontuacao;

    void Start()
    {
        textPontuacao = GetComponent<TextMeshProUGUI>();
        FinalScore();
    }

    // void FinalScore(){
    //     int pontuacaoFinal = gameObject.GetComponent<Enemy>().ptnsFinal;
    //     textPontuacao.text =  "" + pontuacaoFinal;
    //     print(pontuacaoFinal);
    // }

    void FinalScore()
    {
        int pontuacaoFinal = GameSessions.Instance.PontuacaoFinal;
        textPontuacao.text = pontuacaoFinal.ToString();
        print(pontuacaoFinal);
    }



    // public void AtualizaPontuacao(int pontuacao = 1){
    //     int pontuacaoAtual = int.Parse(textPontuacao.text);
    //     pontuacaoAtual += pontuacao;
    //     textPontuacao.text = pontuacaoAtual.ToString();
    // }
    // public Text pointsText;
    // public void Setup(int score){
    //     int pontuacaoFinal = int.Parse(textPontuacao.text);

    //     gameObject.SetActive(true);
    //     pointsText.text = score.ToString() + " POINTS";
    // }
}
