using UnityEngine;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
    public GameObject PlayerPrefab;
    public GameObject player;
    public List<GameObject> Objects = new List<GameObject>();

    public List<GameObject> TerrainPrefabs;
    bool needPlayerStart = true;
    public float playerStartX;
    public float playerStartY;

    void Start(){
        createWorld();

    }

    public void createWorld(){
        CreateTerrain();
        CreatePlayer();
    }

    void Update(){
        if (Input.GetKeyDown(KeyCode.LeftShift)){

        }
    }

    void CreatePlayer(){
        player = Instantiate(PlayerPrefab);
        player.transform.position = new Vector2(playerStartX, playerStartY + 1.0f);
    }

    void CreateTerrain(){
        int forwardMovement = 0;
        //int[,] gen1 = GetRandomGen();
        //int[,] gen2 = GetRandomGen();
        //int[,] gen3 = GetRandomGen();
        List<int[,]> gens = new List<int[,]>();
        //int[,] list1 = {{6, 12, 12, 12}, {1, 13, 13, 13}, {19, 16, 4, 4}};
        //int[,] list2 = {{12,12,12,12,12}, {13,13,13,13,13}, {4,4,14,13,8},{0,0,1,13,2}, {0,0,19,4,20}};
        //int[,] list4 = {{6, 12, 12, 12, 12, 12, 12, 12, 12, 12, 7}, {22, 13, 13, 13, 13, 15, 4, 4, 4, 16, 20}, {4, 14, 13, 13, 13, 2, 0, 0, 0, 0, 0}, {0, 19, 9, 4, 4, 20, 0, 0, 0, 0, 0}};
        //gens.Add(list1);
        //gens.Add(list2);
        //gens.Add(list4);
        int[,] sampleRoom = {{0,0,0,0,0,6,25,7,0,0}, 
                             {0,6,12,12,12,29,8,30,12,7},
                             {6,29,13,13,13,15,24,14,13,2}, 
                             {19,16,14,13,5,21,0,27,28,26},
                             {0,0,19,9,4,20,0,1,13,2},
                             {0,0,6,12,12,12,12,29,13,2},
                             {0,0,19,16,14,13,13,13,15,20},
                             {0,0,0,0,19,9,4,9,20,0}};
        gens.Add(sampleRoom);
        for (int k = 0; k < gens.Count; k++){
            for (int i = 0; i < gens[k].GetLength(0); i++){
                for (int j = 0; j < gens[k].GetLength(1); j++){
                    int room = gens[k][i, j];
                    Generate(room, j + forwardMovement, i);
                }
            }
            forwardMovement = forwardMovement + gens[k].GetLength(1);
        }
    }

    int[,] GetRandomGen(){
        int val = Random.Range(0,5);
        switch (val){
            case 0:
                int[,] list = {{6, 12, 12, 12}, {1, 13, 13, 13}, {19, 16, 4, 4}};
                return list;
                break;
            
            case 1:
                int[,] list2 = {{12,12,12,12,12}, {13,13,13,13,13}, {4,4,14,13,8},{0,0,1,13,2}, {0,0,19,4,20}};
                return list2;
                break;

            case 3:
                int[,] list4 = {{6, 12, 12, 12, 12, 12, 12, 12, 12, 12, 7}, {22, 13, 13, 13, 13, 15, 4, 4, 4, 16, 20}, {4, 14, 13, 13, 13, 2, 0, 0, 0, 0, 0}, {0, 19, 9, 4, 4, 20, 0, 0, 0, 0, 0}};                
                return list4;
            default:
                int[,] list3 = {{6, 12, 12, 12}, {1, 13, 13, 13}, {19, 16, 4, 4}};
                return list3;

        }
    }

    void Generate(int room, int xOffset, int yOffset){
        int offXAmount = xOffset * 5;
        int offYAmount = yOffset * 5;
        GameObject newRoom = null;
        if (room != 0){
            newRoom = Instantiate(TerrainPrefabs[room]);
            newRoom.transform.position = new Vector2(0.0f + offXAmount, 0.0f - offYAmount);
        }
        if (room == 16 && needPlayerStart){
            playerStartX = 0.0f + offXAmount;
            playerStartY = 0.0f - offYAmount;
            needPlayerStart = false;
        }
        else if (room == 16 && needPlayerStart == false){
            newRoom.GetComponent<Elevator>().startElevator = false;
        }
    }

    // World Generation Setups

}

public class Room{
    private int[,] roomGen;
    public int exitType;
}
