using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public enum WeaponType
{
    Primary,
    Special,
    Body
}


public class LoadoutManager : MonoBehaviour
{
    public static LoadoutManager Instance;
    public SceneLoader sceneLoader;

    // Assuming you have set these via the inspector
    public Button[] primaryWeaponButtons;
    public Button[] specialWeaponButtons;
    public Button[] BodyButtons;
    public WeaponButton DefaultPrimary;
    public WeaponButton DefaultSpecial;
    public WeaponButton DefaultBody;

    public Button launchButton;
    public Button escapeButton;


    private WeaponButton selectedPrimaryWeapon;
    private WeaponButton selectedSpecialWeapon;
    private WeaponButton selectedBody;

    // [SerializeField] private int? selectedPrimaryWeapon;
    // [SerializeField] private int? selectedSpecialWeapon;

    void OnEnable()
    {
        GameManager gm = GameManager.instance;
        launchButton.onClick.AddListener(ConfirmSelections);
        launchButton.onClick.AddListener(gm.LoadNextLevel);
        escapeButton.onClick.AddListener(sceneLoader.LoadPreviousScene);
    }


    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);


        SelectWeapon(DefaultPrimary);
        SelectWeapon(DefaultSpecial);
        SelectWeapon(DefaultBody);

    }

    void Start(){
        if (Cursor.lockState != CursorLockMode.None)
            Cursor.lockState = CursorLockMode.None;
        if (Cursor.visible != true)
            Cursor.visible = true;
    }

    private void Update()
    {
        // When the return key is pressed, click the launch button
        if(Input.GetKeyDown(KeyCode.Return)){
            launchButton.onClick.Invoke();
        }
        if(Input.GetKeyDown(KeyCode.Escape)){
            escapeButton.onClick.Invoke();
        }
    } 

    public void SelectWeapon(WeaponButton weaponButton)
    {
        if (weaponButton.weaponType.ToString().Equals("Primary"))
        {
            if (selectedPrimaryWeapon != null && selectedPrimaryWeapon != weaponButton)
            {
                selectedPrimaryWeapon.Deselect();
            }

            selectedPrimaryWeapon = weaponButton;
        }
        else if (weaponButton.weaponType.ToString().Equals("Special"))
        {
            if (selectedSpecialWeapon != null && selectedSpecialWeapon != weaponButton)
            {
                selectedSpecialWeapon.Deselect();
            }

            selectedSpecialWeapon = weaponButton;
        }
        else if (weaponButton.weaponType.ToString().Equals("Body"))
        {
            if (selectedBody != null && selectedBody != weaponButton)
            {
                selectedBody.Deselect();
            }

            selectedBody = weaponButton;
        }
        /* else if (!isPrimary && selectedSpecialWeapon != null)
         {
             // Update the previously selected special button to unselected state
             UpdateButtonVisual(selectedSpecialWeapon, false);
         }

        // Select the new button by changing its visual state
        UpdateButtonVisual(button, true);

        // Save the selected button
        if (isPrimary)
             selectedPrimaryWeapon = button;
        else
             selectedSpecialWeapon = button;

        // Check if both selections are made
        if (selectedPrimaryWeapon != null && selectedSpecialWeapon != null)
        {
            launchButton.interactable = true; // Enable the launch button if both selections are made
        }*/
    }

    private void UpdateUI()
    {
        // Update UI elements to reflect the current selections
    }

    public void ConfirmSelections()
    {
        GameManager gameManager = GameManager.instance;
        // 
        if (selectedPrimaryWeapon != null && selectedSpecialWeapon != null){
            gameManager.PrimaryWeapon = selectedPrimaryWeapon.weaponPrefab;
            gameManager.SpecialWeapon = selectedSpecialWeapon.weaponPrefab;
            gameManager.planeBody = selectedBody.bodyScriptableObject;

        }
        else {
            Debug.Log("Weapons not Selected");
        }
    }

}