using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlaneCreator : MonoBehaviour
{
    private GameObject drawnObject; //Global drawing object

    [Range(0.1f, 10f)] [SerializeField] private float destroyTimer = 2f; //Float used to set how long until the object gets destroyed
 

    [Range(0f, 1f)] [SerializeField] private float red = 1f, green = 1f, blue = 1f;    //Float to set the red, rreen, and blue hue on objects


    [Range(0.3f, 5f)] [SerializeField] private float size = 1f; //Float to set the size on objects


    public Text mousePosition;      //Used to update the Mouse Position text in the UI
    public Text destroyTimerText;   //Used to update the label of the Destroy timer Slider
    public Text sizeLabelText;      //Used to update the label of the size Slider

    private bool timedDestroy = true;    //Boolean used for destroy toggle
    private bool randColor = true;       //Boolean used for random color toggle
    private bool randSize = true;        //Boolean used for random size toggle

    private int shapeIndex = 0;          //Int used to set the object shape using the Dropdown menu

    private Vector3 clickPosition = -Vector3.one;           //Vector to determine where user clicks on screen
    private Plane canvas = new Plane(Vector3.forward, 0f);  //Plane to draw on

    //Variables added for Project 2

    private int menuIndex = 0;          //Int to Change which menus appear on screen
    public GameObject colorToggle, redSlider, greenSlider, blueSlider,
        shapeToggle, shapeMenu, sizeToggle, sizeSlider, 
        rotReset, rotToggle, rotXSlider, rotYSlider, rotZSlider, rotNote;     // Objects so that I can link all of the UI elements

    private bool randShape = false;     //Boolean for Random Shape toggle

    private bool randRot = false;       //Boolean for Random Rotation toggle
    private float totalX = 0f, totalY = 0f, totalZ = 0f;    //Total Rotation amount of objects
    [Range(0f, 179f)] [SerializeField] private float rotX = 0f, rotY = 0f, rotZ = 0f;   //Floats to affect rotation amount each time an object is made

    public Text XLabelText;     //Used to update the label of the X Slider
    public Text YLabelText;     //Used to update the label of the Y Slider
    public Text ZLabelText;     //Used to update the label of the Z Slider

    //Variables added for Project 3

    public GameObject snowman, snowFlake, animationToggle, randAniToggle, animationMenu, cube, sphere, cylinder, capsule;

    private bool animationTog, randAnimationTog;
    private int animationIndex;
   


    // Update is called once per frame
    void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition); // Creates the Ray to spawn objects
        RaycastHit hit;

        if (Input.GetMouseButtonDown(0) || Input.GetMouseButton(0)) // Checks for left click/hold
        {          
            if (canvas.Raycast(ray, out float distanceToPlane))  //Finds the click position to spawn objects
            {
                clickPosition = ray.GetPoint(distanceToPlane);
            }

            if (Physics.Raycast(ray, out hit) && hit.transform.gameObject.layer == 9)   //Checks if you're clicking on the clock and changes the color if you are
            {
                hit.transform.gameObject.GetComponent<Renderer>().material.color = new Vector4(red, green, blue, 1f);
            }

            int chosenShape = Random.Range(0, 6);   //Random number chosen to give a shape.
            if (!randShape)
            {
                switch (shapeIndex) //Creates the object of a certain shape based on the dropdown
                {
                    case 0:
                        drawnObject = Instantiate(sphere);
                        break;

                    case 1:
                        drawnObject = Instantiate(cube, clickPosition, Quaternion.identity, this.transform.parent) as GameObject;
                        break;

                    case 2:
                        drawnObject = Instantiate(cylinder, clickPosition, Quaternion.identity, this.transform.parent) as GameObject;
                        break;

                    case 3:
                        drawnObject = Instantiate(capsule, clickPosition, Quaternion.identity, this.transform.parent) as GameObject;
                        break;

                    case 4:
                        drawnObject = Instantiate(snowman, clickPosition, Quaternion.identity, this.transform.parent) as GameObject;
                        break;

                    case 5:
                        drawnObject = Instantiate(snowFlake, clickPosition, Quaternion.identity, this.transform.parent) as GameObject;
                        break;

                    default:
                        drawnObject = Instantiate(sphere, clickPosition, Quaternion.identity, this.transform.parent) as GameObject;
                        break;
                }
            }
            else
            {
                switch (chosenShape) //Creates the object of a certain shape based on a random number and if the toggle is selected
                {
                    case 0:
                        drawnObject = Instantiate(sphere, clickPosition,Quaternion.identity, this.transform.parent) as GameObject;
                        break;

                    case 1:
                        drawnObject = Instantiate(cube, clickPosition, Quaternion.identity, this.transform.parent) as GameObject;
                        break;

                    case 2:
                        drawnObject = Instantiate(cylinder, clickPosition, Quaternion.identity, this.transform.parent) as GameObject;
                        break;

                    case 3:
                        drawnObject = Instantiate(capsule, clickPosition, Quaternion.identity, this.transform.parent) as GameObject;
                        break;

                    case 4:
                        drawnObject = Instantiate(snowman, clickPosition, Quaternion.identity, this.transform.parent) as GameObject;
                        break;

                    case 5:
                        drawnObject = Instantiate(snowFlake, clickPosition, Quaternion.identity, this.transform.parent) as GameObject;
                        break;

                    default:
                        drawnObject = Instantiate(sphere, clickPosition, Quaternion.identity, this.transform.parent) as GameObject;
                        break;
                }
            }

            if (randSize) //Makes the drawnObject a random or set size depending on the size slider and toggle
            {
                drawnObject.transform.localScale = new Vector3(Random.Range(0.3f, size), Random.Range(0.3f, size), Random.Range(0.3f, size));
            }
            else
            {
                drawnObject.transform.localScale = new Vector3(size, size, size); //Changes object size without randomizing
            }

            if (randColor)   //Makes the drawnObject a random or set color depending on the color slider and toggle
            {
                drawnObject.GetComponent<Renderer>().material.color = new Vector4(Random.Range(0f, red), Random.Range(0f, green), Random.Range(0f, blue), 1f);
            }
            else
            {
                drawnObject.GetComponent<Renderer>().material.color = new Vector4(red, green, blue, 1f); //Sets the objects color without randomizing
            }

            if (randRot)    //Gives the drawnObject a random or set rotation depending on the sliders and toggle
            {
                drawnObject.transform.eulerAngles = new Vector3(Random.Range(0f, 179f), Random.Range(0f, 179f), Random.Range(0f, 179f));
            }
            else
            {
                drawnObject.transform.eulerAngles = new Vector3(totalX, totalY, totalZ);
                totalX += rotX;
                totalY += rotY;
                totalZ += rotZ;
            }

            drawnObject.transform.position = clickPosition; //Puts object at mouse postion

            drawnObject.transform.parent = this.transform; //Makes the objects children of objectCreator so that they can be deleted at the press of a button.

            if (timedDestroy) //Destroys the object based on the time slider and toggle
            {
                Destroy(drawnObject, destroyTimer); 
            }
        }

        if (Input.GetMouseButtonDown(1) || Input.GetMouseButton(1)) //Checks for right click or hold
        {
            if (Physics.Raycast(ray, out RaycastHit planeFound) && planeFound.transform.gameObject.layer != 9)
            {
                Destroy(planeFound.transform.gameObject);
            }
        }

        mousePosition.text = "Your mouse is at X: " + Input.mousePosition.x.ToString("F0") + ", Y: " + Input.mousePosition.y.ToString("F0"); //Writes text to mouse position label

        destroyTimerText.text = destroyTimer + "s"; // Writes text to the destroy timer label, if it's toggled on
        if (!timedDestroy)
        {
            destroyTimerText.text = " ";
        }

        sizeLabelText.text = size.ToString("0.0"); //Writes text to the size label


        XLabelText.text = "X: " + rotX.ToString("0.0"); //Writes text to the X label
        YLabelText.text = "Y: " + rotY.ToString("0.0"); //Writes text to the Y label
        ZLabelText.text = "Z: " + rotZ.ToString("0.0"); //Writes text to the Z label

        if(randRot) //Makes the lables blank when Random rotation is selected
        {
            XLabelText.text = " "; 
            YLabelText.text = " "; 
            ZLabelText.text = " "; 
        }

        switch (menuIndex)      //Switch Statement to set viewable menues
        {
            case 0: //Color
                colorToggle.SetActive(true);
                redSlider.SetActive(true);
                greenSlider.SetActive(true);
                blueSlider.SetActive(true);
                shapeToggle.SetActive(false);
                shapeMenu.SetActive(false);
                sizeToggle.SetActive(false);
                sizeSlider.SetActive(false);
                rotReset.SetActive(false);
                rotToggle.SetActive(false);
                rotXSlider.SetActive(false);
                rotYSlider.SetActive(false);
                rotZSlider.SetActive(false);
                rotNote.SetActive(false);
                animationToggle.SetActive(false);
                randAniToggle.SetActive(false);
                animationMenu.SetActive(false);
                break;
            case 1: //Shape
                colorToggle.SetActive(false);
                redSlider.SetActive(false);
                greenSlider.SetActive(false);
                blueSlider.SetActive(false);
                shapeToggle.SetActive(true);
                shapeMenu.SetActive(true);
                sizeToggle.SetActive(false);
                sizeSlider.SetActive(false);
                rotReset.SetActive(false);
                rotToggle.SetActive(false);
                rotXSlider.SetActive(false);
                rotYSlider.SetActive(false);
                rotZSlider.SetActive(false);
                rotNote.SetActive(false);
                animationToggle.SetActive(false);
                randAniToggle.SetActive(false);
                animationMenu.SetActive(false);
                break;
            case 2: //Size
                colorToggle.SetActive(false);
                redSlider.SetActive(false);
                greenSlider.SetActive(false);
                blueSlider.SetActive(false);
                shapeToggle.SetActive(false);
                shapeMenu.SetActive(false);
                sizeToggle.SetActive(true);
                sizeSlider.SetActive(true);
                rotReset.SetActive(false);
                rotToggle.SetActive(false);
                rotXSlider.SetActive(false);
                rotYSlider.SetActive(false);
                rotZSlider.SetActive(false);
                rotNote.SetActive(false);
                animationToggle.SetActive(false);
                randAniToggle.SetActive(false);
                animationMenu.SetActive(false);
                break;
            case 3: //Rotation
                colorToggle.SetActive(false);
                redSlider.SetActive(false);
                greenSlider.SetActive(false);
                blueSlider.SetActive(false);
                shapeToggle.SetActive(false);
                shapeMenu.SetActive(false);
                sizeToggle.SetActive(false);
                sizeSlider.SetActive(false);
                rotReset.SetActive(true);
                rotToggle.SetActive(true);
                rotXSlider.SetActive(true);
                rotYSlider.SetActive(true);
                rotZSlider.SetActive(true);
                rotNote.SetActive(true);
                animationToggle.SetActive(false);
                randAniToggle.SetActive(false);
                animationMenu.SetActive(false);
                break;
            case 4: //Animation
                colorToggle.SetActive(false);
                redSlider.SetActive(false);
                greenSlider.SetActive(false);
                blueSlider.SetActive(false);
                shapeToggle.SetActive(false);
                shapeMenu.SetActive(false);
                sizeToggle.SetActive(false);
                sizeSlider.SetActive(false);
                rotReset.SetActive(false);
                rotToggle.SetActive(false);
                rotXSlider.SetActive(false);
                rotYSlider.SetActive(false);
                rotZSlider.SetActive(false);
                rotNote.SetActive(false);
                animationToggle.SetActive(true);
                randAniToggle.SetActive(true);
                animationMenu.SetActive(true);
                break;
            case 5: //Hide All
                colorToggle.SetActive(false);
                redSlider.SetActive(false);
                greenSlider.SetActive(false);
                blueSlider.SetActive(false);
                shapeToggle.SetActive(false);
                shapeMenu.SetActive(false);
                sizeToggle.SetActive(false);
                sizeSlider.SetActive(false);
                rotReset.SetActive(false);
                rotToggle.SetActive(false);
                rotXSlider.SetActive(false);
                rotYSlider.SetActive(false);
                rotZSlider.SetActive(false);
                rotNote.SetActive(false);
                animationToggle.SetActive(false);
                randAniToggle.SetActive(false);
                animationMenu.SetActive(false);
                break;
        }

        if (animationTog)
        {
            if (drawnObject != null)
            {
                drawnObject.GetComponent<Animator>().SetBool("AnimationTog", animationTog);

                if (randAnimationTog)
                {
                    int chosenAnimation = Random.Range(0, 3);

                    switch (chosenAnimation)
                    {
                        case 0:
                            drawnObject.GetComponent<Animator>().SetInteger("MenuIndex", chosenAnimation);
                            break;
                        case 1:
                            drawnObject.GetComponent<Animator>().SetInteger("MenuIndex", chosenAnimation);
                            break;
                        case 2:
                            drawnObject.GetComponent<Animator>().SetInteger("MenuIndex", chosenAnimation);
                            break;
                        default:
                            drawnObject.GetComponent<Animator>().SetInteger("MenuIndex", 0);
                            break;
                    }
                }

                else
                {
                    switch (animationIndex)
                    {
                        case 0:
                            drawnObject.GetComponent<Animator>().SetInteger("MenuIndex", animationIndex);
                            break;
                        case 1:
                            drawnObject.GetComponent<Animator>().SetInteger("MenuIndex", animationIndex);
                            break;
                        case 2:
                            drawnObject.GetComponent<Animator>().SetInteger("MenuIndex", animationIndex);
                            break;
                        default:
                            drawnObject.GetComponent<Animator>().SetInteger("MenuIndex", 0);
                            break;
                    }
                }
            }
        }

        if (Input.GetMouseButton(0))    //This block is used to check if you hit the clock and to set the color according to the sliders
        {
            if(Physics.Raycast(ray, out hit))
            {
                if(hit.transform.gameObject.layer == 9)
                {
                    hit.transform.gameObject.GetComponent<Renderer>().material.color = new Vector4(red, green, blue, 1f);
                }
            }
        }
    }
    //Methods to interact with the UI

    //Methods from Project 1
    //Interacts with Timer Slider
    public void ChangeDestroyTimer(float time) => destroyTimer = time;

    //Interacts with Red Slider
    public void ChangeRedCap(float cap) => red = cap;

    //Interacts with Green Slider
    public void ChangeGreenCap(float cap) => green = cap;

    //Interacts with Blue Slider
    public void ChangeBlueCap(float cap) => blue = cap;

    //Interacts with Size Slider
    public void ChangeSizeCap(float cap) => size = cap;

    //Interacts with Shape Dropdown
    public void ShapeIndexChange(int index) => shapeIndex = index;

    //Interacts with Destroy Toggle
    public void TimedDestroyToggle(bool tog) => timedDestroy = tog;

    //Interects with Random Colors Toggle
    public void RandColorToggle(bool tog) => randColor = tog;

    //Interacts with Random Size Toggle
    public void RandSizeToggle(bool tog) => randSize = tog;

    //Interacts with Clear Screen Button
    public void ClearScreen()
    {
        foreach(Transform child in transform)
        {
            Destroy(child.gameObject);
        }
    }


    //Methods for Project 2

    //Interacts with Menu Dropdown
    public void menuIndexChange(int index) =>  menuIndex = index;

    //Interacts with Random Shape Toggle
    public void RandShapeToggle(bool tog) => randShape = tog;

    //Interacts with Random Rotation Toggle
    public void RandRotToggle(bool tog) => randRot = tog;

    //Interacts with Reset Rotation Button
    public void ResetRotaion()
    {
        totalX = 0;

        totalY = 0;

        totalZ = 0;
    }

    //Interacts with X Slider
    public void ChangeX(float x) => rotX = x;

    //Interacts with Y Slider
    public void ChangeY(float y) => rotY = y;

    //Interacts with Z Slider
    public void ChangeZ(float z) => rotZ = z;

    //Methods for Project 3

    //Interacts with the Animation Dropdown
    public void animationIndexChange(int index) => animationIndex = index;

    //Interacts with Animation Toggle
    public void animationsOnOff(bool tog) => animationTog = tog;

    //Interacts with Random Animation Toggle
    public void randomAnimationsToggle(bool tog) => randAnimationTog = tog;

}
