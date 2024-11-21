

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
    public bool hasSuccess = false;
    public bool hasPassedThrougTheFinalArea = false;

    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public InstructionModel Level_O1()
    {
        return new InstructionModel(
            "SE os valores dos dados no lado AZUL e no lado VERMELHO forem iguais E os valores dos dados NÃO formarem o mesmo par, as plataformas se alinharão.",
            "EX: Dados AZUL formam 2+3 =6; Os dados VERMELHOS tem que somar 6, mas não poderão ser 2+3, podendo ser 5+1",
            "Este conceito de balanceamento é usado em física, quando estudamos o equilíbrio de forças, como na construção de pontes ou no funcionamento de balanças. Também é usado em economia para equilibrar orçamentos e recursos."
            );
    }

    public InstructionModel Level_O2()
    {
        return new InstructionModel(
            "Ache a CHAVE correta",
            "SE o jogador encontrar e posicionar a chave correta na porta, a porta se abrirá.",
            "Na vida real, sistemas de segurança usam esse tipo de lógica, como em cofres que só abrem com o código certo ou em aplicativos que verificam tokens de acesso para permitir login. Esse mesmo conceito é usado em biologia, como quando um receptor na célula só ativa uma resposta se a molécula certa se ligar a ele (chave e fechadura biológica). Na física, sensores condicionais podem ativar máquinas apenas se uma determinada força for aplicada."
            );
    }
}
