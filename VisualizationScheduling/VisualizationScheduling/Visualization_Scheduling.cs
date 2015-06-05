using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.VisualBasic;

namespace VisualizationScheduling
{
    public partial class Visualization_Scheduling : Form
    {
        private bool ReadedFile = false;
        public List<Process> pList, pView;
        public List<Result> resultList;
        string[] StrData;
        public int TimeQuntam = 5;
        public string path;
        public int a;
        public Boolean[] Schedul = new Boolean[6];
        public bool none = true;
        public Visualization_Scheduling()
        {   
            InitializeComponent();

        }
        public void Main_From_Load(object sender, EventArgs e)
        {
            pList = new List<Process>();
            pView = new List<Process>();

        }
        private string SelectFilePath()
        {
            openFileDialog1.Filter = "텍스트파일|*.txt";
            return (openFileDialog1.ShowDialog() == DialogResult.OK) ? openFileDialog1.FileName : null;
        }
        private void newToolStripMenuItem_Click(object sender, EventArgs e) //New
        {
            pView.Clear();
            pList.Clear();
            dataGridView1.Rows.Clear();
            Visualization_Scheduling vi = new Visualization_Scheduling();
            vi.Show();
        }
        private void openToolStripMenuItem_Click(object sender, EventArgs e) //Open
        {
            path = SelectFilePath();
            if (path == null) return;
            StrData = File.ReadAllLines(path);

            for (int i = 0; i < StrData.Length; i++)
            {
                string[] token = StrData[i].Split(' ');
                Process p = new Process(int.Parse(token[1]), int.Parse(token[2]), int.Parse(token[3]), int.Parse(token[4]));
                pList.Add(p);
            }

            ReadedFile = true;
            dataGridView1.Rows.Clear();
            string[] row = { "", "", "", "" };
            foreach (Process p in pList)
            {
                row[0] = p.ProcessID.ToString();
                row[1] = p.ArriveTime.ToString();
                row[2] = p.BurstTime.ToString();
                row[3] = p.Priority.ToString();
                dataGridView1.Rows.Add(row);
            }
            pList.Sort(delegate(Process x, Process y)
            {
                return x.ArriveTime.CompareTo(y.ArriveTime);
            });
            
        }

        private void addToolStripMenuItem_Click(object sender, EventArgs e) //Add
        {
            //파일 오픈
            path = SelectFilePath();
            if (path == null) return;

            StrData = File.ReadAllLines(path);

            for (int i = 0; i < StrData.Length; i++)
            {
                string[] token = StrData[i].Split(' ');
                Process p = new Process(int.Parse(token[1]), int.Parse(token[2]), int.Parse(token[3]), int.Parse(token[4]));
                pList.Add(p);
            }
            /////////////////////////////Data Gridview에 값넣음/////////////////////////
            ReadedFile = true;
            dataGridView1.Rows.Clear();
            string[] row = { "", "", "", "" };
            foreach (Process p in pList)
            {
                row[0] = p.ProcessID.ToString();
                row[1] = p.ArriveTime.ToString();
                row[2] = p.BurstTime.ToString();
                row[3] = p.Priority.ToString();

                dataGridView1.Rows.Add(row);
            }
            pList.Sort(delegate(Process x, Process y)
            {
                return x.ArriveTime.CompareTo(y.ArriveTime);
            });

            //arriveTime으로 정렬
        }
        private void openToolStripMenuItem1_Click(object sender, EventArgs e)
        {
          openToolStripMenuItem_Click(sender, e);
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("종료하시겠습니까?", "종료창", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                if (System.Windows.Forms.Application.MessageLoop)
                {
                    System.Windows.Forms.Application.Exit();
                }
                else
                {
                    System.Environment.Exit(1);
                }
            }
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Team : 김상욱, 이근열, 이언우\nO/S Term Project(Scheduling)","About");
        }

        private void runToolStripMenuItem_Click(object sender, EventArgs e)
        {
                if (pList.Count == 0)
                {
                    MessageBox.Show("Data Nothing", "error");
                    return;
                }
            if (Schedul[0] == true || Schedul[1] == true || Schedul[2] == true || Schedul[4] == true || Schedul[3] == true || Schedul[5] == true)
            {
                if (Schedul[3] == true)
                {
                    string value = "5";
                    if (InputBox("TimeQuantam", "입력", ref value) == DialogResult.OK)
                    {
                        TimeQuntam = Convert.ToInt32(value.ToString());
                    }
                    else
                    {
                        TimeQuntam = 5;
                    }
                }
                Sub_From frm = new Sub_From(this);
                frm.Show();
            }
            if (Schedul[0] == false && Schedul[1] == false && Schedul[2] == false && Schedul[4] == false && Schedul[3] == false && Schedul[5] == false)
            {
                MessageBox.Show("체크된 것이 없습니다. 체크해 주세요.");
                return;
            }
        }

        private void runToolStripMenuItem1_Click(object sender, EventArgs e) //Run버튼
        {
            runToolStripMenuItem_Click(sender, e);
        }
       
        /// <summary>
        /// ////////////////////////////////////////////////이부분 부터 손대지 마시오..........................
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dataGridView1_CellEndEdit(object sender, DataGridViewCellEventArgs e)// datagridview에 동적수정이 일어나면 색변화
        {
            string str;
            int temp;
            string temp2;
            int[] random = new int[4];
            if (pList.Count <= e.RowIndex)
            {
                Random rm = new Random();
                random[0] = rm.Next(1, 10);
                random[1] = rm.Next(0, 30);
                random[2] = rm.Next(0, 30);
                random[3] = rm.Next(1, 10);
                Process p = new Process(random[0], random[1], random[2], random[3]);
                for (int i = 0; i < 4; i++)
                {
                    if(i != e.ColumnIndex)
                    {
                        dataGridView1.Rows[e.RowIndex].Cells[i].Value = random[i].ToString();
                    }
                    else
                    {
                        if (i == 1)
                        {
                            temp2 = dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString();
                            temp = Convert.ToInt32(temp2);
                            p.ArriveTime = temp;
                        }
                        if (i == 2)
                        {
                            temp2 = dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString();
                            temp = Convert.ToInt32(temp2);
                            p.BurstTime = temp;
                        }
                        if (i == 3)
                        {
                            temp2 = dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString();
                            temp = Convert.ToInt32(temp2);
                            p.Priority = temp;
                        }
                        if (i == 0)
                        {
                            temp2 = dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString();
                            temp = Convert.ToInt32(temp2);
                            p.ProcessID = temp;
                        }
                        
                    }
                }
                    pList.Add(p);
            }

            if (e.ColumnIndex == 0)
            {
                str = dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString();
                temp = Convert.ToInt32(str);
                if (temp > 100)
                {
                    temp = temp % 100;
                }
                pList.ElementAt(e.RowIndex).ProcessID = temp;
                dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = temp.ToString();
            }
            else if (e.ColumnIndex == 1)
            {
                str = dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString();
                temp = Convert.ToInt32(str);
                if (temp > 100)
                {
                    temp = temp % 100;
                }
                pList.ElementAt(e.RowIndex).ArriveTime = temp;
                dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = temp.ToString();
            }
            else if (e.ColumnIndex == 2)
            {
                str = dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString();
                temp = Convert.ToInt32(str);
                if (temp > 100)
                {
                    temp = temp % 100;
                }
                pList.ElementAt(e.RowIndex).BurstTime = temp;
                dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = temp.ToString();
            }
            else if (e.ColumnIndex == 3)
            {
                str = dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString();
                temp = Convert.ToInt32(str);
                if (temp > 100)
                {
                    temp = temp % 100;
                }
                pList.ElementAt(e.RowIndex).Priority = temp;
                dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = temp.ToString();
            }
            dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Style.BackColor = System.Drawing.Color.Plum;
        }
        /// ////////////////////////////////////////////////이부분 까지//////////////////////////////////////////////////
        private void 추가_Click(object sender, EventArgs e) //datagridview에 랜덤값 넣기
        {
            dataGridView1.Rows.Clear();
            Random rm = new Random();
            int[] array = new int[10];
            int burst;
            int priorty;
            for (int i = 0; i < 10; i++)
            {
                array[i] = rm.Next(0, 30);
                burst = rm.Next(1, 30);
                priorty = rm.Next(1, 10);
                Process p = new Process(i+1, array[i], burst, priorty);
                pList.Add(p);
            }
            string[] row = { "", "", "", "" };
            foreach (Process p in pList)
            {
                row[0] = p.ProcessID.ToString();
                row[1] = p.ArriveTime.ToString();
                row[2] = p.BurstTime.ToString();
                row[3] = p.Priority.ToString();
                dataGridView1.Rows.Add(row);
            }
            for (int i = 0; i < pList.Count; i++)
            {
                pList.ElementAt(i).ProcessID = i + 1;
                dataGridView1.Rows[i].Cells[0].Value = (i + 1).ToString();
            }
            pList.Sort(delegate(Process x, Process y)
            {
                return x.ArriveTime.CompareTo(y.ArriveTime);
            });
        }
        private void checkedListBox1_SelectedIndexChanged(object sender, EventArgs e) // 체크리스트에서 선택된것만
        {
                for (int i = 0; i <= (checkedListBox1.Items.Count - 1); i++)
                {
                    if (checkedListBox1.GetItemChecked(i) == true)
                    {
                        Schedul[i] = true;
                        none = false;
                    }
                }
                for (int i = 0; i <= (checkedListBox1.Items.Count - 1); i++)
                {
                    if (checkedListBox1.GetItemChecked(i) == false)
                    {
                        Schedul[i] = false;
                    }
                }
        }

        private void button1_Click(object sender, EventArgs e)// 전체 체크리스트 해제
        {

            CheckedListBox list;
            list = checkedListBox1;
            for (int i = 0; i < list.Items.Count; i++)
            {
                list.SetItemChecked(i, false);
                Schedul[i] = false;
            }
        }

        private void button2_Click(object sender, EventArgs e) //전체 체크리스트 선택
        {
            CheckedListBox list;
            list = checkedListBox1;
            for (int i = 0; i < list.Items.Count; i++)
            {
                list.SetItemChecked(i, true);
                Schedul[i] = true; 
            }
        }

        private void button3_Click(object sender, EventArgs e)//입력된 모든것을 지운다
        {
            this.pList.Clear();
            this.dataGridView1.Rows.Clear();
            this.button1_Click(sender, e);
        }
        private void button4_Click(object sender, EventArgs e)//processID 정렬
        {
            pList.Sort(delegate(Process x, Process y)
            {
                return x.ArriveTime.CompareTo(y.ArriveTime);
            });
            for (int i = 0; i < pList.Count; i++)
            {
                pList.ElementAt(i).ProcessID = i+1;
                dataGridView1.Rows[i].Cells[0].Value = (i+1).ToString();
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            runToolStripMenuItem_Click(sender, e);
        }

        private void button6_Click(object sender, EventArgs e)
        {
            openToolStripMenuItem_Click(sender, e);
        }

        private void Visualization_Scheduling_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (MessageBox.Show("종료하시겠습니까?", "Visualization_Scheduling",MessageBoxButtons.YesNo) == DialogResult.No)
            {   
                e.Cancel = true;
            }

        }
        public static DialogResult InputBox(string title, string promptText, ref string value)
        {
            Form form = new Form();
            Label label = new Label();
            TextBox textBox = new TextBox();
            Button buttonOk = new Button();
            Button buttonCancel = new Button();

            form.Text = title;
            label.Text = promptText;
            textBox.Text = value;

            buttonOk.Text = "OK";
            buttonCancel.Text = "Cancel";
            buttonOk.DialogResult = DialogResult.OK;
            buttonCancel.DialogResult = DialogResult.Cancel;

            label.SetBounds(9, 20, 372, 13);
            textBox.SetBounds(12, 36, 372, 20);
            buttonOk.SetBounds(228, 72, 75, 23);
            buttonCancel.SetBounds(309, 72, 75, 23);

            label.AutoSize = true;
            textBox.Anchor = textBox.Anchor | AnchorStyles.Right;
            buttonOk.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            buttonCancel.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;

            form.ClientSize = new Size(396, 107);
            form.Controls.AddRange(new Control[] { label, textBox, buttonOk, buttonCancel });
            form.ClientSize = new Size(Math.Max(300, label.Right + 10), form.ClientSize.Height);
            form.FormBorderStyle = FormBorderStyle.FixedDialog;
            form.StartPosition = FormStartPosition.CenterScreen;
            form.MinimizeBox = false;
            form.MaximizeBox = false;
            form.AcceptButton = buttonOk;
            form.CancelButton = buttonCancel;

            DialogResult dialogResult = form.ShowDialog();
            value = textBox.Text;
            return dialogResult;
        }
    }
}
