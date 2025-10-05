using System.Collections.Generic;
using UnityEngine;

public class WorkManager : MonoBehaviour
{
    public static WorkManager Instance;

    public int Energy = 100;
    public int MaxEnergy = 100;
    public int CurrentMonth = 0;

    [System.Serializable]
    public class Job
    {
        public string Name;
        public int Salary;
        public int RequiredEducationLevel; // liczba od 1 do 5
        public int RequiredExperience;     // w miesi¹cach
        public int GainedExperience;
        
        public Job(string name, int salary, int requiredEducationLevel, int requiredExperience)
        {
            Name = name;
            Salary = salary;
            RequiredEducationLevel = requiredEducationLevel;
            RequiredExperience = requiredExperience;
            GainedExperience = 0;
        }

        public void WorkMonth()
        {
            GainedExperience += 1;
        }

        public bool CanAdvance(int playerEducationLevel, int experience)
        {
            return experience >= RequiredExperience && playerEducationLevel >= RequiredEducationLevel;
        }
    }

    public Dictionary<string, Job> Jobs = new Dictionary<string, Job>();
    public Job CurrentJob;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            InitializeJobs();

            // Startowa praca od razu
            CurrentJob = Jobs["Intern"];
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void InitializeJobs()
    {
        Jobs.Add("Intern", new Job("Intern", 2000, 1, 0));
        Jobs.Add("Junior", new Job("Junior", 3000, 1, 3));
        Jobs.Add("Worker", new Job("Worker", 4000, 2, 12));
        Jobs.Add("Specialist", new Job("Specialist", 5000, 4, 24));
        Jobs.Add("Engineer", new Job("Engineer", 6000, 4, 36));
        Jobs.Add("Manager", new Job("Manager", 8000, 5, 48));
        Jobs.Add("Director", new Job("Director", 10000, 5, 60));
    }
}
