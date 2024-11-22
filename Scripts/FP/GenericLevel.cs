

using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InstructionModel
{
    public string MSG = "";
    public string EX = "";
    public string ED = "";

    public InstructionModel(string mSG, string eX, string eD)
    {
        MSG = mSG;
        EX = eX;
        ED = eD;
    }
}

public class GenericLevel : MonoBehaviour
{
    public bool isLevelCompleted = false;
    public bool hasEndedLevel = false;

    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void EndLevel(bool success)
    {
        if (success)
        {
            isLevelCompleted = true;
            hasEndedLevel = true;
        }
    }

    public InstructionModel Level_O1()
    {
        return new InstructionModel(
            "SE os valores dos dados no lado AZUL e no lado VERMELHO forem iguais E os valores dos dados N�O formarem o mesmo par, as plataformas se alinhar�o.",
            "EX: Dados AZUL formam 2+3 =6; Os dados VERMELHOS tem que somar 6, mas n�o poder�o ser 2+3, podendo ser 5+1",
            "Este conceito de balanceamento � usado em f�sica, quando estudamos o equil�brio de for�as, como na constru��o de pontes ou no funcionamento de balan�as. Tamb�m � usado em economia para equilibrar or�amentos e recursos."
            );
    }

    public InstructionModel Level_O2()
    {
        return new InstructionModel(
            "Ache a CHAVE correta",
            "SE o jogador encontrar e posicionar a chave correta na porta, a porta se abrir�.",
            "Na vida real, sistemas de seguran�a usam esse tipo de l�gica, como em cofres que s� abrem com o c�digo certo ou em aplicativos que verificam tokens de acesso para permitir login. Esse mesmo conceito � usado em biologia, como quando um receptor na c�lula s� ativa uma resposta se a mol�cula certa se ligar a ele (chave e fechadura biol�gica). Na f�sica, sensores condicionais podem ativar m�quinas apenas se uma determinada for�a for aplicada."
            );
    }
}
