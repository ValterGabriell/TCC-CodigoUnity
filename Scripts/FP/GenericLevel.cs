

using System.Runtime.CompilerServices;
using UnityEngine;

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
    

    public InstructionModel Level_O1()
    {
        return new InstructionModel(
            "SE os valores dos dados no lado AZUL e no lado VERMELHO forem iguais E os valores dos dados NÃO formarem o mesmo par, as plataformas se alinharão.",
            "EX: Dados AZUL formam 2+3 =6; Os dados VERMELHOS tem que somar 6, mas não poderão ser 2+3, podendo ser 5+1",
            "Este conceito de balanceamento é usado em física, quando estudamos o equilíbrio de forças, como na construção de pontes ou no funcionamento de balanças. Também é usado em economia para equilibrar orçamentos e recursos."
            );
    }
}
