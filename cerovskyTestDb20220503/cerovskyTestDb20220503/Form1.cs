using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace cerovskyTestDb20220503
{
    public partial class Form1 : Form
    {
        private void Form1_Load(object sender, EventArgs e)
        {

        }



        SqlRepository sQLRepository = new SqlRepository();
        List<Operacione> operation;
        public Form1()
        {
            InitializeComponent();
            operation = sQLRepository.GetHodnotyFromDb();
            RefreshGUI();
        }

        public void RefreshGUI()
        {
            tabulka.Items.Clear();
            foreach (var h in operation)
            {
                var cenaSDPH = (h.DPH * 0.01 * h.PriceForOnePiece) + h.PriceForOnePiece;
                var cenaCelkem = cenaSDPH * h.Count;
                ListViewItem listView = new ListViewItem(new string[]
                {
                    h.Name,
                    Convert.ToString(h.PriceForOnePiece),
                    Convert.ToString(h.DPH)+" %",
                    Convert.ToString(cenaSDPH),
                    Convert.ToString(h.Count),
                    Convert.ToString(cenaCelkem),
                    Convert.ToString(h.Bill.dateTime),
                    Convert.ToString(h.Bill.BillNumber),
                    Convert.ToString(h.Bill.Subscriber)
                });
                tabulka.Items.Add(listView);
            }
        }
    }
}
