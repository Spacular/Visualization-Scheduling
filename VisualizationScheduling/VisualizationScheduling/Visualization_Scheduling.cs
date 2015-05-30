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

namespace VisualizationScheduling
{
    public partial class Visualization_Scheduling : Form
    {
        private bool ReadedFile = false;
        public List<Process> pList, pView;
        public List<Result> resultList;
        string[] StrData;
        public string path;
        public int a;
        public Boolean[] Schedul = new Boolean[5];
        
        
        public Visualization_Scheduling()
        {
            InitializeComponent();
        }
        public void Main_From_Load(object sender, EventArgs e)
        {
            pList = new List<Process>();
            pView = new List<Process>();
            // 리스트 뷰 관련 : http://blog.naver.com/ehdrua0305/130183774413
            //리스트 박스 내용을 Txt파일로 저장 : http://kindtis.tistory.com/168
            //새 문서 생성 : http://kin.naver.com/qna/detail.nhn?d1id=1&dirId=1040102&docId=68218792&qb=QyMg66mU64m0IO2BtOumrQ==&enc=utf8&section=kin&rank=1&search_sort=0&spq=1&pid=SSy1GspySoCssv%2BHndossssssts-099782&sid=q38TllHh1OYueU0Rz63cJQ%3D%3D
        }

        private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {
           
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
            pView.Clear();
            pList.Clear();

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
            textBox1.Text = path;
            
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
            /*
            foreach (DataGridViewRow rows in dataGridView1.Rows)
            {
                foreach (DataGridViewColumn cols in dataGridView1.Columns)
                {
                    //row.Cells[col.Index].Style.BackColor = Color.Green; //doesn't work
                    //col.Cells[row.Index].Style.BackColor = Color.Green; //doesn't work
                    //dataGridView1[cols.Index, rows.Index].Style.BackColor = Color.Violet; //doesn't work
                    dataGridView1[cols.Index,rows.Index].Style.ForeColor = Color.Violet;
                }
            } *//*
            foreach (DataGridViewRow rows in dataGridView1.Rows)
            {
                foreach (DataGridViewColumn cols in dataGridView1.Columns)
                {
                    if(String.Compare(cols.Index.ToString(), rows.Index.ToString(), true) == 0)
                        dataGridView1[0, rows.Index].Style.ForeColor = Color.Red;
                }
            } */

            //arriveTime으로 정렬
        }
        private void openToolStripMenuItem1_Click(object sender, EventArgs e)
        {
          openToolStripMenuItem_Click(sender, e);
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //reference : http://stackoverflow.com/questions/12977924/how-to-properly-exit-a-c-sharp-application
            if (MessageBox.Show("종료하시겠습니까?", "종료창", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                if (System.Windows.Forms.Application.MessageLoop)
                {
                    // WinForms app
                    System.Windows.Forms.Application.Exit();
                }
                else
                {
                    // Console app
                    System.Environment.Exit(1);
                }
            }
            else
            {

            }
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Team : 김상욱, 이근열, 이언우\nO/S Term Project(Scheduling)","About");
        }

        private void runToolStripMenuItem_Click(object sender, EventArgs e) //Run버튼//서브폼 호출
        {
            if (pList.Count == 0)
            {
                MessageBox.Show("Data Nothing", "error");
                return;
            }
            Sub_From frm = new Sub_From(this);
            frm.ShowDialog();
            
        }

        private void runToolStripMenuItem1_Click(object sender, EventArgs e) //Run버튼
        {
            runToolStripMenuItem_Click(sender, e);
        }

        private void dataGridView1_CellEndEdit(object sender, DataGridViewCellEventArgs e)// datagridview에 수정일어나면 색변화
        {
            dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Style.BackColor = System.Drawing.Color.Plum;
        }

        private void 추가_Click(object sender, EventArgs e) //datagridview에 랜덤값 넣기
        {
            dataGridView1.Rows.Clear();
            Random rm = new Random();
            int[] array = new int[10];
            int burst;
            int priorty;
            for (int i = pList.Count; i < 10; i++)
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
            pList.Sort(delegate(Process x, Process y)
            {
                return x.ArriveTime.CompareTo(y.ArriveTime);
            });
        }
        private void checkedListBox1_SelectedIndexChanged(object sender, EventArgs e) // 체크리스트에서 선택된것만 (미완)
        {

                for (int i = 0; i <= (checkedListBox1.Items.Count - 1); i++)
                {
                    if (checkedListBox1.GetItemChecked(i) == true)
                    {
                        Schedul[i] = true;
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

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
