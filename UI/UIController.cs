using UnityEngine;
using UnityEngine.UIElements;
public class UIController : MonoBehaviour 
{
    public PlayerMove playerMove;
    public GameManager gameManager;




    private void OnEnable()
    {

        VisualElement root = GetComponent<UIDocument>().rootVisualElement;
        Button left = root.Q<Button>("left");
        Button right = root.Q<Button>("right");
        Button forward = root.Q<Button>("forward");
        Button backward = root.Q<Button>("backward");


        Button _while = root.Q<Button>("while");
        Button freeWay = root.Q<Button>("freeWay");
        Button obstacle = root.Q<Button>("obstacle");


        Button start = root.Q<Button>("start");

        Button _if = root.Q<Button>("if");
        Button _else = root.Q<Button>("else");

       
        /* DISABLE BTN OF CONDITIONS*/
        freeWay.visible = false;
        obstacle.visible = false;

        /* ACTIONS */
        left.clicked += () => playerMove.MoveLeft();
        right.clicked += () => playerMove.MoveRight();
        forward.clicked += () => playerMove.MoveForward();
        backward.clicked += () => playerMove.MoveBackward();


        /* WHILE CLICKED*/
        _while.clicked += () => WannaCreateCondition(freeWay);

        /*IF CLICKED*/
        _if.clicked += () => WannaCreateIF(obstacle);

        /* CONDITIONS */
        freeWay.clicked += () => playerMove.WhileCondition(GameManager.MovementConditions.FREE_WAY);
        obstacle.clicked += () => playerMove.SetIfCondition(GameManager.IFConditions.OBSTACLE);

        /* START */
        start.clicked += () => StartCoroutine(playerMove.StartMovements());
    }


    private void WannaCreateCondition(Button conditionBtn)
    {
        conditionBtn.visible = true;
    }

    private void WannaCreateIF(Button conditionBtn)
    {
        conditionBtn.visible = true;
    }
}
