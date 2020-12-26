using System;
using System.Collections.Generic;
using UnityEngine;

public class HexagonalView {

    private float sqrt_3 = (float) Math.Sqrt(3);
    private float horizConst;
    private float verticalConst;

    public GameObject hexBoard;

    private World world;
    private Dictionary<int, Texture2D> hexNumberImages;

    public HexagonalView(World world, Texture2D[] hexNumberImages)
    {
        horizConst = 4.5f;
        verticalConst = 3.0f * sqrt_3;
        this.world = world;
        this.hexNumberImages = numberImagestoHash(hexNumberImages);
        hexBoard = new GameObject("hexBoard");
        buildHexNumbers(world.hexagonalNumbers);
    }

    private void buildHexNumbers(int[,] hexagonalNumbers)
    {
        for (int i = 0; i < world.hexagonalNumbers.GetLength(0); i++)
        {
            for (int j = 0; j < world.hexagonalNumbers.GetLength(1); j++)
            {
                // so we don't need to display the number on the even x's on the last row
                if (!(j == world.hexagonalNumbers.GetLength(1) - 1 && (i % 2) == 0))
                {
                    addHexNumber(world.hexagonalNumbers[i, j], i, j);
                }
            }
        }
    }

    private void addHexNumber(int number, int i, int j)
    {
        GameObject currentTile = new GameObject(i + ", " + j + " - " + number);
        currentTile.transform.parent = hexBoard.transform;
        SpriteRenderer sr = currentTile.AddComponent<SpriteRenderer>();
        sr.sprite = GetHexNumberSprite(number);
        sr.sortingOrder = 2;
        setHexNumPosition(currentTile, i, j);
    }

    private void setHexNumPosition(GameObject currentTile, int i, int j)
    {
        Vector3 position = new Vector3(horizConst * i + horizConst / 3f, j * verticalConst + .75f * sqrt_3 - ((i % 2) * 1.5f * sqrt_3), 0f);
        Quaternion rotation = Quaternion.Euler(new Vector3(0, 0, 0));
        Vector3 scale = new Vector3(1f, 1f, 1f);
        setPosition(currentTile, position, rotation, scale);
    }

    private void setPosition(GameObject currentTile, Vector3 position, Quaternion rotation, Vector3 scale)
    {
        currentTile.transform.localScale = scale;
        currentTile.transform.localPosition = position;
        currentTile.transform.localRotation = rotation;
    }

    private Sprite GetHexNumberSprite(int number)
    {
        return Sprite.Create(hexNumberImages[number], new Rect(0, 0, hexNumberImages[number].width, hexNumberImages[number].height), new Vector2(.5f, .5f));
    }

    private Dictionary<int, Texture2D> numberImagestoHash(Texture2D[] hexNumberImages)
    {
        Dictionary<int, Texture2D> hexNumberImagesHash = new Dictionary<int, Texture2D>();
        hexNumberImagesHash.Add(2, hexNumberImages[0]);
        hexNumberImagesHash.Add(3, hexNumberImages[1]);
        hexNumberImagesHash.Add(4, hexNumberImages[2]);
        hexNumberImagesHash.Add(5, hexNumberImages[3]);
        hexNumberImagesHash.Add(6, hexNumberImages[4]);
        hexNumberImagesHash.Add(8, hexNumberImages[5]);
        hexNumberImagesHash.Add(9, hexNumberImages[6]);
        hexNumberImagesHash.Add(10, hexNumberImages[7]);
        hexNumberImagesHash.Add(11, hexNumberImages[8]);
        hexNumberImagesHash.Add(12, hexNumberImages[9]);

        return hexNumberImagesHash;
    }

}
