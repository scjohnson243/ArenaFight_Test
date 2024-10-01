using UnityEngine;
using System.Collections.Generic;
using Sirenix.OdinInspector;

public class Spawner : MonoBehaviour
{
    public GameObject meleePrefab; // Single unit prefab
    public GameObject rangedPrefab;  // Ranged unit prefab

    public Transform[] redSpawnPoints;  // Spawn points for Red team
    public Transform[] blueSpawnPoints; // Spawn points for Blue team

    public int unitsPerTeam; // Number of units to spawn per team

    private List<string> firstNames;  // List of first names
    private List<string> lastNames;   // List of last names

    private void Start()
    {
        LoadNames();  // Load names from the text files
    }

    [Button]
    public void SpawnTeams()
    {
        SpawnUnits(unitsPerTeam, redSpawnPoints, Team.Red);
        SpawnUnits(unitsPerTeam, blueSpawnPoints, Team.Blue);
    }

    private void SpawnUnits(int numberOfUnits, Transform[] spawnPoints, Team team)
    {
        for (int i = 0; i < numberOfUnits; i++)
        {
            int randomSpawnPoint = Random.Range(0, spawnPoints.Length);
            
            GameObject unitPrefab = Random.Range(0, 2) == 0 ? meleePrefab : rangedPrefab;

            // Instantiate the unitPrefab at a random spawn point
            GameObject unitObj = Instantiate(unitPrefab, spawnPoints[randomSpawnPoint].position, Quaternion.identity);
            Unit unit = unitObj.GetComponent<Unit>();

            // Assign the team to the unit
            unit.team = team;

            // Assign a random name to the unit
            string randomName = GenerateRandomName();
            unitObj.name = randomName; // Set the GameObject's name in Unity (for debugging)
            
            // (Optional) If the unit has a name display (like a UI element), you can set it here.
            // unitObj.GetComponent<Unit>().SetName(randomName);

            CustomizeUnitAppearance(unitObj, team);
        }
    }

    // Load the first and last names from text files in the Resources folder
    private void LoadNames()
    {
        // Load first names
        TextAsset firstNameText = Resources.Load<TextAsset>("firstNames");
        firstNames = new List<string>(firstNameText.text.Split(new[] { '\n', '\r' }, System.StringSplitOptions.RemoveEmptyEntries));
            
        //firstNames = new List<string>(firstNames.text.Split(new[] { '\n', '\r' }, System.StringSplitOptions.RemoveEmptyEntries));

        // Load last names
        TextAsset lastNameText = Resources.Load<TextAsset>("lastNames");
        lastNames = new List<string>(lastNameText.text.Split(new[] { '\n', '\r' }, System.StringSplitOptions.RemoveEmptyEntries));
    }

    // Generate a random full name by combining a random first and last name
    private string GenerateRandomName()
    {
        int randomFirstNameIndex = Random.Range(0, firstNames.Count);
        int randomLastNameIndex = Random.Range(0, lastNames.Count);

        string firstName = firstNames[randomFirstNameIndex];
        string lastName = lastNames[randomLastNameIndex];

        return $"{firstName} {lastName}";
    }

    // Customize unit appearance (e.g., color) based on team
    private void CustomizeUnitAppearance(GameObject unitObj, Team team)
    {
        Renderer renderer = unitObj.GetComponent<Renderer>();

        if (team == Team.Red)
        {
            renderer.material.color = Color.red;
        }
        else if (team == Team.Blue)
        {
            renderer.material.color = Color.blue;
        }
    }
}
