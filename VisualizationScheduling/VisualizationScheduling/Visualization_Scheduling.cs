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
        List<Process> pList, pView;
        string[] StrData;

        public Visualization_Scheduling()
        {
            InitializeComponent();
        }

        private void Main_From_Load(object sender, EventArgs e)
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

        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e) //Open
        {
            pView.Clear();
            pList.Clear();

            //파일 오픈
            string path = SelectFilePath();
            if (path == null) return;

            StrData = File.ReadAllLines(path);

            //토큰 분리
            for (int i = 0; i < StrData.Length; i++)
            {
                string[] token = StrData[i].Split(' ');
                Process p = new Process(int.Parse(token[1]), int.Parse(token[2]), int.Parse(token[3]), int.Parse(token[4]));
                pList.Add(p);
            }

            //Grid에 process 출력
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

            //arriveTime으로 정렬
            pList.Sort(delegate(Process x, Process y)
            {
                return x.ArriveTime.CompareTo(y.ArriveTime);
            });

            ReadedFile = true;
        }

        private void addToolStripMenuItem_Click(object sender, EventArgs e) //Add
        {
            //파일 오픈
            string path = SelectFilePath();
            if (path == null) return;

            StrData = File.ReadAllLines(path);

            //토큰 분리
            for (int i = 0; i < StrData.Length; i++)
            {
                string[] token = StrData[i].Split(' ');
                Process p = new Process(int.Parse(token[1]), int.Parse(token[2]), int.Parse(token[3]), int.Parse(token[4]));
                pList.Add(p);
            }

            //Grid에 process 출력
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
            pList.Sort(delegate(Process x, Process y)
            {
                return x.ArriveTime.CompareTo(y.ArriveTime);
            });

            ReadedFile = true;
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e) //Save
        {

        }

        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e) //Save As
        {

        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e) //Exit
        {
            //reference : http://stackoverflow.com/questions/12977924/how-to-properly-exit-a-c-sharp-application
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


    }
}
