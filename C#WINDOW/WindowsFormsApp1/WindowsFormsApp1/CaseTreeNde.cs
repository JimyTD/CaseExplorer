using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp1
{
    class CaseTreeNode
    {
        public CaseTreeNode(string sTitle,string sContent,int nLayer,string sParent)
        {
            title = sTitle;
            content = sContent;
            layer = nLayer;
            parentTitle = sParent;
        }
        public string title;
        public string content;
        public string parentTitle;
        /// <summary>
        /// 层数，从0开始
        /// </summary>
        public int layer;
        public List<CaseTreeNode> childCaseList = new List<CaseTreeNode>();
    }
}
