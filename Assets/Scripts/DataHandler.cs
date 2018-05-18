using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ReadWriteCSV;
using System.IO;

/// <summary>
/// Writes a line of data after every trial, giving information on the trial.
/// </summary>
public class DataHandler : MonoBehaviour
{

    // stores the data for writing to file at end of task
    List<TrialData> trialData = new List<TrialData>();

    private string pid = GlobalControl.Instance.participantID;
    private string tryNum = GlobalControl.Instance.tryNumber;

    /// <summary>
    /// Write all data to a file
    /// </summary>
    void OnDisable()
    {
        WriteTrialFile();
    }

    // Records trial data into the data list
    public void recordTrial(float score, int numPickups, int numBadPickups, int numFalls, float timeRemaining, bool won)
    {
        trialData.Add(new TrialData(score, numPickups, numBadPickups, numFalls, timeRemaining, won));
    }

    /// <summary>
    /// A class that stores info on each trial relevant to data recording. Every field is
    /// public readonly, so can always be accessed, but can only be assigned once in the
    /// constructor.
    /// </summary>
    class TrialData
    {
        public readonly float score;
        public readonly int numPickups;
        public readonly int numBadPickups;
        public readonly int numFalls;
        public readonly float timeRemaining;
        public readonly bool won;

        public TrialData(float score, int numPickups, int numBadPickups, int numFalls, float timeRemaining, bool won)
        {
            this.score = score;
            this.numPickups = numPickups;
            this.numBadPickups = numBadPickups;
            this.numFalls = numFalls;
            this.timeRemaining = timeRemaining;
            this.won = won;
        }
    }

    /// <summary>
    /// Writes the Trial File to a CSV
    /// </summary>
    private void WriteTrialFile()
    {

        // Write all entries in data list to file
        Directory.CreateDirectory(@"Data/" + pid);
        using (CsvFileWriter writer = new CsvFileWriter(@"Data/" + pid + "/" + pid + "Try" + tryNum + ".csv"))
        {
            Debug.Log("Writing trial data to file");

            // write header
            CsvRow header = new CsvRow();
            header.Add("Participant ID");
            header.Add("Trial Number");
            header.Add("Score");
            header.Add("Number of Pickups Collected");
            header.Add("Number of Bad Pickups Collected");
            header.Add("Number of Falls");
            header.Add("Time remaining (seconds)");
            header.Add("Total time (seconds)");
            header.Add("Level Difficulty");
            header.Add("Game won?");

            writer.WriteRow(header);

            // write each line of data
            foreach (TrialData d in trialData)
            {
                CsvRow row = new CsvRow();

                row.Add(pid);
                row.Add(tryNum);
                row.Add(d.score.ToString());
                row.Add(d.numPickups.ToString());
                row.Add(d.numBadPickups.ToString());
                row.Add(d.numFalls.ToString());
                row.Add(d.timeRemaining.ToString());
                row.Add(GlobalControl.Instance.timeLimit.ToString());
                row.Add(GlobalControl.Instance.levelNumber.ToString());
                if (d.won)
                {
                    row.Add("YES");
                }
                else
                {
                    row.Add("NO");
                }

                writer.WriteRow(row);
            }
        }
    }
}
