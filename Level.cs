using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : GameHandler
{
    public const float pipeWidth = 25f;
    public const float levelSpeed = 30f;
    public float leftEdgePosition = -250f;
    public float rightEdgePosition = +250f;
    public float timerDuration = 4f;
    public float height2 = 0f;

    private List<Pipe> pipeList;
    private float pipeSpawnTimer;
    

    private void Awake() {
        pipeList = new List<Pipe>();

    }

    private void Start() {
        /* CreateGapPipes(50f, 80f, 55f);
        CreateGapPipes(150f, 100f, 55f); */
    }

    private void Update() {
        HandlePipeMovement();
        HandlePipeSpawning();
    }

    private void HandlePipeMovement() {
        for (int i=0; i<pipeList.Count; i++) {
            Pipe pipe = pipeList[i];
            pipe.Move();
            if (pipe.GetXPosition() < leftEdgePosition) {
                pipe.DestroySelf();
                pipeList.Remove(pipe);
                i--;
            }
        }
    }

    private void HandlePipeSpawning() {
        pipeSpawnTimer -= Time.deltaTime;
        if(pipeSpawnTimer < 0) {
            height2 = Random.Range(40f, 200f);
            pipeSpawnTimer = timerDuration;
            CreateGapPipes(rightEdgePosition, height2, 50f);
        }
    }

    private void CreateGapPipes (float xPosition, float gapY, float gapSize) {
        CreatePipe(xPosition, gapY - gapSize/2, true);
        CreatePipe(xPosition, mainCamera.orthographicSize * 2 - gapY - gapSize/2, false);
    }

    private void CreatePipe (float xPosition, float height, bool createBottom) {

        // Set Pipebody
        Transform pipebody = Instantiate(GameAssets.GetInstance().pfPipebody);
        float pipebodyYposition;
        if (createBottom) {
            pipebodyYposition = -mainCamera.orthographicSize;
        } else {
            pipebodyYposition = mainCamera.orthographicSize;
            pipebody.localScale = new Vector3(1, -1, 1);
        }
        pipebody.position = new Vector3(xPosition, pipebodyYposition);
        
        // Set Pipehead
        Transform pipehead = Instantiate(GameAssets.GetInstance().pfPipehead);
        float pipeheadYposition;
        if (createBottom) {
            pipeheadYposition = -mainCamera.orthographicSize + height;
        } else {
            pipeheadYposition = mainCamera.orthographicSize - height;
        }
        pipehead.position = new Vector3(xPosition, pipeheadYposition);

        SpriteRenderer pipeBodySpriteRenderer = pipebody.GetComponent<SpriteRenderer>();
        pipeBodySpriteRenderer.size = new Vector2(pipeWidth, height);

        BoxCollider2D pBodyBoxCollider = pipebody.GetComponent<BoxCollider2D>();
        pBodyBoxCollider.offset = new Vector2(0, height/2);
        pBodyBoxCollider.size = new Vector2(pipeWidth, height);

        Pipe pipe = new Pipe(pipehead, pipebody);
        pipeList.Add(pipe);
    }

    // represents a single Pipe
    private class Pipe {

        private Transform pipeheadTransform;
        private Transform pipebodyTransform;

        public Pipe(Transform pipeheadTransform, Transform pipebodyTransform) {
            this.pipeheadTransform = pipeheadTransform;
            this.pipebodyTransform = pipebodyTransform;
        }

        public void Move() {
            pipeheadTransform.position += new Vector3(-1, 0, 0) * levelSpeed * Time.deltaTime;
            pipebodyTransform.position += new Vector3(-1, 0, 0) * levelSpeed * Time.deltaTime;
        }

        public float GetXPosition() {
            return pipeheadTransform.position.x;
        }

        public void DestroySelf() {
            Destroy(pipeheadTransform.gameObject);
            Destroy(pipebodyTransform.gameObject);
        }
    }
}
