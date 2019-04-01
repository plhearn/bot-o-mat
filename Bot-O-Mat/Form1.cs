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
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

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
            public string key;

            public TaskRecord(Robot robot, RobotTask task, string key)
            {
                this.robot = robot;
                this.task = task;
                this.key = key;
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

            loadJson();
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

            loadDropDowns();
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
            string key = Guid.NewGuid().ToString();     //unique identifier used for updating the progress bars

            TaskRecord record = new TaskRecord(robot, task, key);
            taskHistory.Add(record);

            System.Timers.Timer timer = new System.Timers.Timer();
            timer.Elapsed += (obj, evnt) => SetProgress(key, timer);
            timer.Interval = timerInterval;
            timer.Enabled = true;

            ListViewItem lvi = new ListViewItem();

            int modifiedETA = getModifiedETA(robot.type, task);

            lvi.SubItems[0].Text = robot.name;
            lvi.SubItems.Add(task.description);
            lvi.SubItems.Add("0 / " + modifiedETA);
            lvi.SubItems.Add(key);

            lvTasks.Items.Add(lvi);

            ProgressBar pb = new ProgressBar();

            pb.Minimum = 0;
            pb.Maximum = modifiedETA;
            pb.Value = 0;
            pb.Name = key;
            lvTasks.Controls.Add(pb);

            updateProgressBar(key, 0);
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

        private int updateProgressBar(string key, int amount)
        {
            ProgressBar pb = lvTasks.Controls.OfType<ProgressBar>().FirstOrDefault(q => q.Name == key);

            if (pb != null)
            {
                // find the ListViewItem based on the key
                ListViewItem lvi = lvTasks.Items.Cast<ListViewItem>().FirstOrDefault(q => q.SubItems[3].Text == key);

                //set the position of the progress bars.  doesn't work too well with scrolling.
                Rectangle r = lvi.SubItems[3].Bounds;
                pb.SetBounds(r.X, r.Y, r.Width, r.Height);

                if (pb.Value < pb.Maximum)
                {
                    int newValue = pb.Value + amount;

                    if (newValue > pb.Maximum)
                        newValue = pb.Maximum;

                    if (lvi != null)
                        lvi.SubItems[2].Text = newValue.ToString() + " / " + pb.Maximum.ToString();

                    pb.Value = newValue;
                    
                    return 1;
                }
                else
                {
                    TaskRecord record = taskHistory.First(i => i.key == key);
                    record.completionTime = pb.Value;

                    updateLeaderBoard();

                    return 0;
                }
            }

            return 1;
        }

        delegate void SetProgressCallback(string key, System.Timers.Timer timer);

        private void SetProgress(string key, System.Timers.Timer timer)
        {
            if (this.lvTasks.InvokeRequired)
            {
                SetProgressCallback d = new SetProgressCallback(SetProgress);
                this.BeginInvoke(d, new object[] { key, timer ?? null });
            }
            else
            {
                int updateResult = updateProgressBar(key, timerInterval);

                if (updateResult == 0)
                {
                    //timer.Stop();
                    //timer.Dispose();
                }
            }
        }

        private void btnClearHistory_Click(object sender, EventArgs e)
        {
            taskHistory.Clear();
            lvTasks.Items.Clear();
            lvTasks.Controls.Clear();
            updateLeaderBoard();
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
            string strLeaderBoard = "";

            foreach(Robot robot in robots)
            {
                strLeaderBoard += robot.name + "   ";

                string strAvgTime = "N/A";

                List <TaskRecord> completedRecords = taskHistory.FindAll(r => r.robot == robot && r.completionTime >= 0);

                if (completedRecords.Count > 0)
                {
                    strAvgTime = completedRecords.Average(r => r.completionTime).ToString();
                    strLeaderBoard += strAvgTime;
                }

                strLeaderBoard +=  "\n";
            }

            lblLeaderBoard.Text = strLeaderBoard;
        }

    }
}
