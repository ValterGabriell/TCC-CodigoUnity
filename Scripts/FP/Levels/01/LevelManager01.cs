using UnityEngine;

public class LevelManager01 : GenericLevel
{
    private int currentDiceBlueOneFace = 1;
    private int currentDiceBlueTwoFace = 1;
    private int currentDiceRedOneFace = 1;
    private int currentDiceRedTwoFace = 1;
    public bool canNormalizePlataforms = false;
   




    private void FixedUpdate()
    {
        if (canNormalizePlataforms)
        {
            this.isLevelCompleted = true;
            this.hasSuccess = true;
        }
    }

    public void CheckIfCanOpenDoor()
    {

        //se a soma for igual
        if ((currentDiceBlueOneFace + currentDiceBlueTwoFace) == (currentDiceRedOneFace + currentDiceRedTwoFace))
        {
            //se os valores de azul 1 for diferente dos vermelhos
            if (currentDiceBlueOneFace != currentDiceRedTwoFace && currentDiceBlueOneFace != currentDiceRedOneFace)
            {
                //se os valroes de azul 2 for diferente dos vermelhos
                if (currentDiceBlueTwoFace != currentDiceRedTwoFace && currentDiceBlueTwoFace != currentDiceRedOneFace)
                {
                    canNormalizePlataforms = true;
                }
                else
                {
                    HUDInteraction.instance.EnableInteractionText("OS VALORES DOS DADOS PRECISAM SER DIFERENTES", "RELEIA AS INSTRU��ES SE NECESS�RIO APERTANDO P");
                }
            }
            else
            {
                HUDInteraction.instance.EnableInteractionText("OS VALORES DOS DADOS PRECISAM SER DIFERENTES", "RELEIA AS INSTRU��ES SE NECESS�RIO APERTANDO P");
            }
        }
        else
        {
            HUDInteraction.instance.EnableInteractionText("O PESO N�O EST� IGUAL", "RELEIA AS INSTRU��ES SE NECESS�RIO APERTANDO P");
        }
    }

    public void IncrementDiceBlueOneFace()
    {
        currentDiceBlueOneFace++;
        if (currentDiceBlueOneFace > 6)
        {
            currentDiceBlueOneFace = 1;
        }
    }

    public void IncrementDiceBlueTwoFace()
    {
        currentDiceBlueTwoFace++;
        if (currentDiceBlueTwoFace > 6)
        {
            currentDiceBlueTwoFace = 1;
        }
    }

    public void IncrementDiceRedOneFace()
    {
        currentDiceRedOneFace++;
        if (currentDiceRedOneFace > 6)
        {
            currentDiceRedOneFace = 1;
        }
    }


    public void IncrementDiceRedTwoFace()
    {
        currentDiceRedTwoFace++;
        if (currentDiceRedTwoFace > 6)
        {
            currentDiceRedTwoFace = 1;
        }
    }


    //return
    public int ReturnDiceBlueOneFace()
    {
        return currentDiceBlueOneFace;
    }

    public int ReturnDiceBlueTwoFace()
    {
        return currentDiceBlueTwoFace;
    }

    public int ReturnDiceRedOneFace()
    {
        return currentDiceRedOneFace;
    }

    public int ReturnDiceRedTwoFace()
    {
        return currentDiceRedTwoFace;
    }
}
