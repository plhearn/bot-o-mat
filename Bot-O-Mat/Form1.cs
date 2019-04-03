using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using System.Diagnostics;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Reflection;

namespace Bot_O_Mat
{

    public partial class Form1 : Form
    {
        string strRobotTasks = @"[
          {
            description: 'do the dishes',
            eta: 1000,
          },{
            description: 'sweep the house',
            eta: 3000,
          },{
            description: 'do the laundry',
            eta: 10000,
          },{
            description: 'take out the recycling',
            eta: 4000,
          },{
            description: 'make a sammich',
            eta: 7000,
          },{
            description: 'mow the lawn',
            eta: 20000,
          },{
            description: 'rake the leaves',
            eta: 18000,
          },{
            description: 'give the dog a bath',
            eta: 14500,
          },{
            description: 'bake some cookies',
            eta: 8000,
          },{
            description: 'wash the car',
            eta: 20000,
          },
        ]";

        string strRobotTypes = @"{ 
          UNIPEDAL: 'Unipedal',
          BIPEDAL: 'Bipedal',
          QUADRUPEDAL: 'Quadrupedal',
          ARACHNID: 'Arachnid',
          RADIAL: 'Radial',
          AERONAUTICAL: 'Aeronautical'
        }";

        public struct RobotTask
        {
            [JsonProperty("description")]
            public string description;
            [JsonProperty("eta")]
            public int eta;
        }

        public struct RobotType
        {
            public string name;
            public string description;
        }

        public class Robot
        {
            public string name;
            public RobotType type;
        }

        public class TaskRecord
        {
            public Robot robot;
            public RobotTask task;
            public int completionTime;

            public TaskRecord(Robot robot, RobotTask task)
            {
                this.robot = robot;
                this.task = task;
                completionTime = -1;
            }
        }

        List<Robot> robots = new List<Robot>();
        List<RobotType> robotTypes = new List<RobotType>();
        List<RobotTask> robotTasks = new List<RobotTask>();
        List<TaskRecord> taskHistory = new List<TaskRecord>();

        const int timerInterval = 100;


        public Form1()
        {
            InitializeComponent();

            cbTask.DropDownStyle = ComboBoxStyle.DropDownList;  //make read only
            cbRobot.DropDownStyle = ComboBoxStyle.DropDownList;
            cbRobotType.DropDownStyle = ComboBoxStyle.DropDownList;
            lvTasks.DoubleBuffered(true);   //reduces flicker from updating
            lvLeaderBoard.DoubleBuffered(true);

            loadJson();

            loadAppData();

            loadDropDowns();
        }

        private void loadJson()
        {
            robotTasks = JsonConvert.DeserializeObject<List<RobotTask>>(strRobotTasks);

            JObject robotTypesJson = Newtonsoft.Json.JsonConvert.DeserializeObject<JObject>(strRobotTypes);

            foreach (var x in robotTypesJson)
            {
                RobotType type = new RobotType();
                type.name = x.Key;
                type.description = x.Value.ToString();
                robotTypes.Add(type);
            }
        }

        private void loadAppData()
        {
            robots = ReadFromJsonFile<List<Robot>>("robots.txt");

            if (robots == null)
                robots = new List<Robot>();


            taskHistory = ReadFromJsonFile<List<TaskRecord>>("taskHistory.txt");

            if (taskHistory == null)
                taskHistory = new List<TaskRecord>();
            
            int taskCount = taskHistory.Count;

            for (int i = 0; i < taskCount; i++)
            {
                TaskRecord record = new TaskRecord(taskHistory[i].robot, taskHistory[i].task);
                taskHistory.Add(record);
                

                ListViewItem lvi = new ListViewItem();

                int modifiedETA = getModifiedETA(taskHistory[i].robot.type, taskHistory[i].task);

                lvi.SubItems[0].Text = taskHistory[i].robot.name;
                lvi.SubItems.Add(taskHistory[i].task.description);
                lvi.SubItems.Add(modifiedETA + " / " + modifiedETA);
                lvi.SubItems.Add("");

                lvTasks.Items.Add(lvi);
            }


            List<dynamic> robotScores = ReadFromJsonFile<List<dynamic>>("leaderBoard.txt");

            if (robotScores == null)
                robotScores = new List<dynamic>();

            if (robotScores != null)
            {
                for (int i = 0; i < robotScores.Count; i++)
                {
                    string[] row = { (i + 1).ToString(), robotScores[i].name, ((int)robotScores[i].avgTime).ToString() };
                    var listViewItem = new ListViewItem(row);
                    lvLeaderBoard.Items.Add(listViewItem);
                }
            }

        }


        private void loadDropDowns()
        {
            loadRobotDropDown();

            foreach (RobotTask task in robotTasks)
            {
                cbTask.DisplayMember = "Text";
                cbTask.ValueMember = "Value";
                cbTask.Items.Add(new { Text = task.description, Value = task });
            }

            foreach (RobotType type in robotTypes)
            {
                cbRobotType.DisplayMember = "Text";
                cbRobotType.ValueMember = "Value";
                cbRobotType.Items.Add(new { Text = type.description, Value = type });
            }
        }

        private void loadRobotDropDown()
        {
            cbRobot.Items.Clear();

            foreach (Robot robot in robots)
            {
                cbRobot.DisplayMember = "Text";
                cbRobot.ValueMember = "Value";
                cbRobot.Items.Add(new { Text = robot.name, Value = robot });
            }
        }

        private void btnAddRobot_Click(object sender, EventArgs e)
        {
            if (txtRobotName.Text == "")
            {
                MessageBox.Show("You must enter a name.");
                return;
            }

            if (cbRobotType.SelectedIndex < 0)
            {
                MessageBox.Show("You must select a type");
                return;
            }

            Robot robot = new Robot();
            robot.name = txtRobotName.Text;
            robot.type = robotTypes[cbRobotType.SelectedIndex];

            robots.Add(robot);

            loadRobotDropDown();

            WriteToJsonFile<List<Robot>>("robots.txt", robots);
        }

        private void btnStartTask_Click(object sender, EventArgs e)
        {

            if (cbRobot.SelectedIndex < 0)
            {
                MessageBox.Show("You must select a robot");
                return;
            }

            if (cbTask.SelectedIndex < 0)
            {
                MessageBox.Show("You must select a task");
                return;
            }

            Robot robot = robots[cbRobot.SelectedIndex];
            RobotTask task = robotTasks[cbTask.SelectedIndex];

            addTask(robot, task);
        }


        private void addTask(Robot robot, RobotTask task)
        {

            ListViewItem lvi = new ListViewItem();

            int modifiedETA = getModifiedETA(robot.type, task);

            lvi.SubItems[0].Text = robot.name;          //name
            lvi.SubItems.Add(task.description);         //task
            lvi.SubItems.Add("0 / " + modifiedETA);     //progress
            lvi.SubItems.Add("");                       //bar
            lvi.SubItems.Add("0");                      //current time 
            lvi.SubItems.Add(modifiedETA.ToString());   //eta

            lvTasks.Items.Add(lvi);

            TaskRecord record = new TaskRecord(robot, task);
            taskHistory.Add(record);

            System.Timers.Timer timer = new System.Timers.Timer();
            timer.Elapsed += (obj, evnt) => SetProgress(lvi, timer, record);
            timer.Interval = timerInterval;
            timer.Enabled = true;

        }


        private int getModifiedETA(RobotType robotType, RobotTask task)
        {
            float eta = task.eta;

            if (robotType.name == "UNIPEDAL")  //unipedals take longer to move around the house
            {
                if (task.description == "sweep the house")
                    eta *= 1.5f;

                if (task.description == "take out the recycling")
                    eta *= 1.5f;
            }

            if (robotType.name == "QUADRUPEDAL")  //four legs makes some tasks faster
            {
                if (task.description == "do the dishes")
                    eta *= 0.5f;

                if (task.description == "do the laundry")
                    eta *= 0.5f;
            }

            if (robotType.name == "ARACHNID")  //spider robots are superior at everything
                eta *= 0.75f;

            if (robotType.name == "RADIAL") //radial robots are slow and not well suited for housekeeping tasks
                eta *= 1.25f;

            return (int)eta;
        }

        private int updateProgressBar(ListViewItem lvi, int amount, TaskRecord record)
        {
            int currentProgress = 0;

            int.TryParse(lvi.SubItems[4].Text, out currentProgress);

            int max = 0;

            int.TryParse(lvi.SubItems[5].Text, out max);

            if (currentProgress < max)
            {
                int newValue = currentProgress + amount;

                if (newValue > max)
                    newValue = max;
                
                lvi.SubItems[2].Text = newValue.ToString() + " / " + max.ToString();

                float percentComplete = newValue / (float)max;

                int numBars = (int)(100 * percentComplete);

                string strBars = "";

                for (int i = 0; i < numBars; i++)
                    strBars += "|";

                lvi.SubItems[3].Text = strBars;

                lvi.SubItems[4].Text = newValue.ToString();
                    
                return 1;
            }
            else
            {
                record.completionTime = currentProgress;

                WriteToJsonFile<List<TaskRecord>>("taskHistory.txt", taskHistory);

                updateLeaderBoard();

                return 0;
            }

            return 1;
        }

        delegate void SetProgressCallback(ListViewItem lvi, System.Timers.Timer timer, TaskRecord record);

        private void SetProgress(ListViewItem lvi, System.Timers.Timer timer, TaskRecord record)
        {
            if (this.lvTasks.InvokeRequired)
            {
                SetProgressCallback d = new SetProgressCallback(SetProgress);
                this.BeginInvoke(d, new object[] { lvi, timer ?? null, record });
            }
            else
            {
                int updateResult = updateProgressBar(lvi, timerInterval, record);

                if (updateResult == 0)
                {
                    timer.Stop();
                    timer.Dispose();
                }
            }
        }

        private void btnClearHistory_Click(object sender, EventArgs e)
        {
            taskHistory.Clear();
            lvTasks.Items.Clear();
            lvTasks.Controls.Clear();
            updateLeaderBoard();

            WriteToJsonFile<List<TaskRecord>>("taskHistory.txt", null);
        }

        private void btnSimulate_Click(object sender, EventArgs e)
        {
            Random random = new Random();

            //create up to 10 robots
            if (robots.Count < 10)
            {
                int robotCount = 10 - robots.Count;

                for (int i = 0; i < robotCount; i++)
                {
                    Robot robot = new Robot();
                    RobotType type = robotTypes[random.Next(robotTypes.Count)];

                    robot.name = "Robot_" + i.ToString() + "_" + type.description;
                    robot.type = type;

                    robots.Add(robot);
                }

                loadRobotDropDown();

                WriteToJsonFile<List<Robot>>("robots.txt", robots);
            }


            //assign first 10 robots to random tasks
            for (int i = 0; i < robots.Count; i++)
            {
                Robot robot = robots[i];
                RobotTask task = robotTasks[random.Next(robotTasks.Count)];
                
                addTask(robot, task);
            }

        }


        private void updateLeaderBoard()
        {
            List<dynamic> robotScores = new List<dynamic>();

            foreach(Robot robot in robots)
            {
                List <TaskRecord> completedRecords = taskHistory.FindAll(r => r.robot == robot && r.completionTime >= 0);

                if (completedRecords.Count > 0)
                    robotScores.Add(new { name = robot.name, avgTime = completedRecords.Average(r => r.completionTime) });
            }
            
            robotScores.Sort((x, y) => x.avgTime.CompareTo(y.avgTime));


            lvLeaderBoard.Items.Clear();

            for (int i = 0; i < robotScores.Count; i++)
            {
                string[] row = { (i+1).ToString(), robotScores[i].name, ((int)robotScores[i].avgTime).ToString() };
                var listViewItem = new ListViewItem(row);
                lvLeaderBoard.Items.Add(listViewItem);
            }

            WriteToJsonFile<List<dynamic>>("leaderBoard.txt", robotScores);
        }

        public void WriteToJsonFile<T>(string filePath, T objectToWrite, bool append = false) where T : new()
        {
            TextWriter writer = null;
            try
            {
                var contentsToWriteToFile = JsonConvert.SerializeObject(objectToWrite);
                writer = new StreamWriter(filePath, append);
                writer.Write(contentsToWriteToFile);
            }
            finally
            {
                if (writer != null)
                    writer.Close();
            }
        }

        public T ReadFromJsonFile<T>(string filePath) where T : new()
        {
            TextReader reader = null;
            try
            {
                if(File.Exists(filePath))
                {
                    reader = new StreamReader(filePath);
                    var fileContents = reader.ReadToEnd();
                    return JsonConvert.DeserializeObject<T>(fileContents);
                }

                return JsonConvert.DeserializeObject<T>("");
            }
            finally
            {
                if (reader != null)
                    reader.Close();
            }
        }

    }

    //this makes the listview use a double buffer.  Without it the listview will flicker a lot due to 10+ timer threads all updating it every 0.1 seconds.  (I used stack overflow to figure this out)
    public static class ControlExtensions
    {
        public static void DoubleBuffered(this Control control, bool enable)
        {
            var doubleBufferPropertyInfo = control.GetType().GetProperty("DoubleBuffered", BindingFlags.Instance | BindingFlags.NonPublic);
            doubleBufferPropertyInfo.SetValue(control, enable, null);
        }
    }
}
