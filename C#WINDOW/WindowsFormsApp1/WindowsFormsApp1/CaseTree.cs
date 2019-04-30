using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    class CaseTree
    {
        private CaseTreeNode NodeTop;
        private string filePath;
        public void InitializeTree()
        {
            NodeTop = new CaseTreeNode("开始", "开始演示", 0, "无");
            getPath();
            readData();
        }
        /// <summary>
        /// 读取数据路径
        /// </summary>
        private void getPath()
        {
            filePath = Environment.CurrentDirectory + @"\data.csv";
        }
        private void readData()
        {
            var data = GetCSVData();
            foreach(var i in data)
            {
                addData(i, NodeTop);
            }
        }
        private bool addData(CaseTreeNode data,CaseTreeNode TreeTop)
        {
            if(data.parentTitle==TreeTop.title)
            {
                TreeTop.childCaseList.Add(data);
                return true;
            }
            foreach (var i in TreeTop.childCaseList)
            {
                if(data.layer >i.layer)
                {
                    bool result=addData(data, i);
                    if (result == true)
                        return true;
                }
            }
            return false;
        }
        /// <summary>
        /// 将CSV文件的数据读取到DataTable中
        /// </summary>
        /// <param name="fileName">CSV文件路径</param>
        /// <returns>返回读取了CSV数据的DataTable</returns>
        private List<CaseTreeNode> GetCSVData()
        {
            //Encoding encoding = Common.GetType(filePath); //Encoding.ASCII;//
            List<CaseTreeNode> listData = new List<CaseTreeNode>();
            FileStream fs = new FileStream(filePath, System.IO.FileMode.Open, System.IO.FileAccess.Read);

            //StreamReader sr = new StreamReader(fs, Encoding.UTF8);
            StreamReader sr = new StreamReader(fs, Encoding.UTF8);
            //string fileContent = sr.ReadToEnd();
            //encoding = sr.CurrentEncoding;
            //记录每次读取的一行记录
            string strLine = "";
            //记录每行记录中的各字段内容
            string[] aryLine = null;
            string[] tableHead = null;
            //标示列数
            int columnCount = 0;
            //标示是否是读取的第一行
            bool IsFirst = true;
            //逐行读取CSV中的数据
            while ((strLine = sr.ReadLine()) != null)
            {
                //strLine = Common.ConvertStringUTF8(strLine, encoding);
                //strLine = Common.ConvertStringUTF8(strLine);

                if (IsFirst == true)
                {
                    tableHead = strLine.Split(',');
                    IsFirst = false;
                    columnCount = tableHead.Length;                    
                }
                if(columnCount!=4)
                {
                    MessageBox.Show("读取配置表头出错！");
                    Environment.Exit(0);
                }
                else
                {
                    aryLine = strLine.Split(',');
                    if(aryLine.Length!=4)
                    {
                        MessageBox.Show("读取配置内容出错！");
                        Environment.Exit(0);
                    }
                    CaseTreeNode newData = new CaseTreeNode(aryLine[0], aryLine[1], int.Parse(aryLine[2]), aryLine[3]);
                    listData.Add(newData);                    
                }
            }
            sr.Close();
            fs.Close();
            return listData;
        }
    }
}
