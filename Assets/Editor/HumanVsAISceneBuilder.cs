using System.Linq;
using Unity.MLAgents.Policies;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public static class HumanVsAISceneBuilder
{
    private const float BoardOffset = 13.75f;

    [MenuItem("Tools/Breakout/Rebuild Human vs AI Scene")]
    public static void Build()
    {
        Scene target = EditorSceneManager.OpenScene("Assets/Scenes/Game.unity", OpenSceneMode.Single);

        GameObject cameraObject = FindRoot(target, "Main Camera");
        Camera camera = cameraObject.GetComponent<Camera>();
        camera.orthographicSize = 15.5f;

        GameObject humanRoot = new GameObject("Human Board (Left)");
        SceneManager.MoveGameObjectToScene(humanRoot, target);
        BoardContext humanBoard = humanRoot.AddComponent<BoardContext>();

        GameObject humanSpawnerObject = FindRoot(target, "BrickSpawn");
        GameObject humanPaddle = FindRoot(target, "Paddle");
        GameObject humanBorders = FindRoot(target, "Borders");
        GameObject humanBallObject = FindRoot(target, "Ball");
        GameObject humanUi = FindRoot(target, "GameUI");

        ParentToBoard(humanRoot, humanSpawnerObject, humanPaddle, humanBorders, humanBallObject, humanUi);
        humanRoot.transform.position = Vector3.left * BoardOffset;
        humanUi.name = "Human Score UI (Existing)";
        humanUi.AddComponent<SideBySideHudLayout>().rightSide = false;

        BallScript humanBall = humanBallObject.GetComponent<BallScript>();
        BrickSpawnScript humanSpawner = humanSpawnerObject.GetComponent<BrickSpawnScript>();
        ShiftSpawnArea(humanBall, humanSpawner, -BoardOffset);

        humanBoard.ball = humanBall;
        humanBoard.brickSpawner = humanSpawner;
        humanBoard.score = humanUi.GetComponent<ScoreScript>();

        Scene training = EditorSceneManager.OpenScene("Assets/Scenes/Game_Training.unity", OpenSceneMode.Additive);
        GameObject aiRoot = new GameObject("AI Board (Right)");
        SceneManager.MoveGameObjectToScene(aiRoot, target);
        BoardContext aiBoard = aiRoot.AddComponent<BoardContext>();

        GameObject aiSpawnerObject = CloneIntoTarget(training, target, "BrickSpawn");
        GameObject aiPaddle = CloneIntoTarget(training, target, "Paddle");
        GameObject aiBorders = CloneIntoTarget(training, target, "Borders");
        GameObject aiBallObject = CloneIntoTarget(training, target, "Ball");
        GameObject aiUi = CloneIntoTarget(training, target, "GameUI");
        aiUi.name = "AI Score UI (Existing)";
        aiUi.AddComponent<SideBySideHudLayout>().rightSide = true;
        ParentToBoard(aiRoot, aiSpawnerObject, aiPaddle, aiBorders, aiBallObject, aiUi);
        aiRoot.transform.position = Vector3.right * BoardOffset;

        BallScript aiBall = aiBallObject.GetComponent<BallScript>();
        BrickSpawnScript aiSpawner = aiSpawnerObject.GetComponent<BrickSpawnScript>();
        RewardScript aiRewards = aiSpawnerObject.GetComponent<RewardScript>();
        PaddleAgent aiAgent = aiPaddle.GetComponent<PaddleAgent>();
        ShiftSpawnArea(aiBall, aiSpawner, BoardOffset);

        aiAgent.isTrainingSession = false;
        SetAgentBallReferences(aiAgent, aiBallObject);
        aiAgent.GetComponent<BehaviorParameters>().BehaviorType = BehaviorType.InferenceOnly;

        SerializedObject agentData = new SerializedObject(aiAgent);
        agentData.FindProperty("minX").floatValue += BoardOffset;
        agentData.FindProperty("maxX").floatValue += BoardOffset;
        agentData.ApplyModifiedPropertiesWithoutUndo();

        aiBoard.ball = aiBall;
        aiBoard.brickSpawner = aiSpawner;
        aiBoard.agent = aiAgent;
        aiBoard.rewards = aiRewards;
        aiBoard.score = aiUi.GetComponent<ScoreScript>();

        AddHeadingOverlay(target);
        EditorSceneManager.CloseScene(training, true);
        EditorSceneManager.MarkSceneDirty(target);
        EditorSceneManager.SaveScene(target, "Assets/Scenes/humanVsAI.unity");
        AssetDatabase.SaveAssets();
        Debug.Log("Built Assets/Scenes/humanVsAI.unity");
    }

    private static GameObject FindRoot(Scene scene, string objectName)
    {
        return scene.GetRootGameObjects().First(gameObject => gameObject.name == objectName);
    }

    private static GameObject CloneIntoTarget(Scene source, Scene target, string objectName)
    {
        GameObject clone = Object.Instantiate(FindRoot(source, objectName));
        clone.name = objectName;
        SceneManager.MoveGameObjectToScene(clone, target);
        return clone;
    }

    private static void ParentToBoard(GameObject board, params GameObject[] objects)
    {
        foreach (GameObject gameObject in objects)
        {
            gameObject.transform.SetParent(board.transform, true);
        }
    }

    private static void ShiftSpawnArea(BallScript ball, BrickSpawnScript spawner, float offset)
    {
        ball.minSpawnX += offset;
        ball.maxSpawnX += offset;
        spawner.startPosition = new Vector2(spawner.startPosition.x + offset, spawner.startPosition.y);
    }

    private static void SetAgentBallReferences(PaddleAgent agent, GameObject ballObject)
    {
        SerializedObject agentData = new SerializedObject(agent);
        agentData.FindProperty("ball").objectReferenceValue = ballObject.transform;
        agentData.FindProperty("ballRigidbody").objectReferenceValue = ballObject.GetComponent<Rigidbody2D>();
        agentData.ApplyModifiedPropertiesWithoutUndo();
    }

    private static void AddHeadingOverlay(Scene target)
    {
        GameObject overlay = new GameObject("Human vs AI Labels");
        SceneManager.MoveGameObjectToScene(overlay, target);
        UIDocument document = overlay.AddComponent<UIDocument>();
        document.panelSettings = AssetDatabase.LoadAssetAtPath<PanelSettings>(
            "Assets/UI Toolkit/PanelSettings.asset");
        document.visualTreeAsset = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>(
            "Assets/UI/HumanVsAI.uxml");
        document.sortingOrder = 10;
    }
}
