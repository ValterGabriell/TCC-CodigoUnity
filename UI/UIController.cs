using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class UIController : MonoBehaviour
{
    public PlayerMove playerMove;
    public GameManager gameManager;

    private VisualElement root;

    private void OnEnable()
    {
        root = GetComponent<UIDocument>().rootVisualElement;

        //BOTOES DE MOVIMENTO
        Button left = root.Q<Button>("left");
        Button right = root.Q<Button>("right");
        Button forward = root.Q<Button>("forward");
        Button backward = root.Q<Button>("backward");
     


        //BOTOES CONDICIONAIS ESCOLHAS
        Button CONTIDIONBTN_freeWay = root.Q<Button>("freeWay");
        Button CONTIDIONBTN_obstacle = root.Q<Button>("obstacle");

        //BOTOES CONDICOES
        Button _if = root.Q<Button>("if");
        Button _else = root.Q<Button>("else");
        Button _endIf = root.Q<Button>("end_if");
        Button _endWhile = root.Q<Button>("end_while");
        Button _while = root.Q<Button>("while");

        //LABEL TEXTOS
        Label textAlgorithm = root.Q<Label>("stepsContent");

        //START
        Button start = root.Q<Button>("start");
        Button avance = root.Q<Button>("avance");

        

        /* DISABLE BTN OF CONDITIONS*/
        CONTIDIONBTN_freeWay.visible = false;
        CONTIDIONBTN_obstacle.visible = false;
        _else.visible = false;
        _endIf.visible = false;
        _endWhile.visible = false;

        /* ACTIONS */
        left.clicked += () =>
        {
            playerMove.MoveLeft();
            textAlgorithm.text += "; INDO PARA ESQUERDA";
        };

        right.clicked += () => {
            playerMove.MoveRight();
            textAlgorithm.text += "; INDO PARA DIREITA";
        };


        forward.clicked += () => {
            playerMove.MoveForward(); textAlgorithm.text += "; INDO PARA FRENTE";
        };
        backward.clicked += () => {
            playerMove.MoveBackward(); textAlgorithm.text += "; INDO PARA TRÁS";
        };



        avance.clicked += () =>
        {
            gameManager.CompleteLevel();
        };


        /* WHILE CLICKED*/
        _while.clicked += () =>
        {
            _while.visible = false;
            _if.visible = false;            
            _endWhile.visible = true;
            CONTIDIONBTN_freeWay.visible = true;
            CONTIDIONBTN_obstacle.visible = true;

            textAlgorithm.text += "; ENQUANTO";
        
            gameManager.AllMovements.Enqueue(GameManager.MoveComplete.WHILE);
        };

        /*IF CLICKED*/
        _if.clicked += () => {
            CONTIDIONBTN_freeWay.visible = true;
            CONTIDIONBTN_obstacle.visible = true;
            _endIf.visible = true;

          
            textAlgorithm.text += "; INICIO BLOCO SE";
            gameManager.AllMovements.Enqueue(GameManager.MoveComplete.IF);
        };

        _endIf.clicked += () => {
            _endIf.visible = false;
            CONTIDIONBTN_freeWay.visible = false;
            CONTIDIONBTN_obstacle.visible = false;

         
            textAlgorithm.text += "; FIM BLOCO SE";
            gameManager.AllMovements.Enqueue(GameManager.MoveComplete.END_IF);

           
        };

        _endWhile.clicked += () => {
            _while.visible = true;
            _if.visible = true;
            _endWhile.visible = false;
            CONTIDIONBTN_freeWay.visible = false;
            CONTIDIONBTN_obstacle.visible = false;

          
            textAlgorithm.text += "; FIM BLOCO ENQUANTO";
            gameManager.AllMovements.Enqueue(GameManager.MoveComplete.END_WHILE);
        };



        /* CONDITIONS */
        CONTIDIONBTN_freeWay.clicked += () => {
          
            textAlgorithm.text += "; CAMINHHO LIVRE";
            gameManager.AllMovements.Enqueue(GameManager.MoveComplete.FREE_WAY);

        };
        CONTIDIONBTN_obstacle.clicked += () =>{
           
            textAlgorithm.text += "; TIVER OBSTACULO";
            gameManager.AllMovements.Enqueue(GameManager.MoveComplete.HAS_COLLIDE_WITH_OBSTACLE);
        };

        /* START */
        start.clicked += () => StartCoroutine(playerMove.StartMovements());
    }
}
